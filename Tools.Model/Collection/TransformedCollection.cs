using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace PeletonSoft.Tools.Model.Collection
{
    public class TransformedCollection<TInnerType, TTargetType>
        : INotifyChangedReadOnlyCollection<TTargetType>
    {
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        private readonly INotifyChangedReadOnlyCollection<TInnerType> _innerCollection;
        private readonly List<TTargetType> _transformedValues = new List<TTargetType>();
        private readonly Func<TInnerType, TTargetType> _transformationFunction;

        /// <summary>
        /// Creates a new transformed collection to view another collection.
        /// </summary>
        /// <param name="innerCollection">The collection containing the instances to transform. Must not be null.</param>
        /// <param name="transformationFunction">The function to convert an inner collection instance. Must not be null.
        /// This delegate must not throw any exceptions.</param>
        public TransformedCollection(INotifyChangedReadOnlyCollection<TInnerType> innerCollection,
            Func<TInnerType, TTargetType> transformationFunction)
        {
            _innerCollection = innerCollection;
            _transformationFunction = transformationFunction;

            CreateCollection();

            //Ensure that when the inner collection is changed, the changes propagate to this one.
            innerCollection.CollectionChanged += InnerCollectionChanged;
        }

        /// <summary>
        /// Transforms and adds every element in the inner list
        /// </summary>
        private void CreateCollection()
        {
            foreach (var element in CreateTransformedElements())
            {
                _transformedValues.Add(element);
            }
        }

        /// <summary>
        /// Transforms all elements in the inner list.
        /// </summary>
        /// <returns>An enumerable containing all the transformed elements.</returns>
        private IEnumerable<TTargetType> CreateTransformedElements()
        {
            return _innerCollection
                .Select(inner => _transformationFunction(inner));
        }

        private void InnerCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            List<TTargetType> newItems;
            List<TTargetType> oldItems;

            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    newItems = new List<TTargetType>();

                    for (var i = 0; i < e.NewItems.Count; ++i)
                    {
                        //Transform all newly added items and add them to temporary "newItems" list.

                        var innerElement = (TInnerType) e.NewItems[i];

                        var transformedElement = _transformationFunction(innerElement);

                        newItems.Add(transformedElement);
                    }

                    _transformedValues.InsertRange(e.NewStartingIndex, newItems);

                    RaiseCollectionChanged(this, new NotifyCollectionChangedEventArgs(
                        NotifyCollectionChangedAction.Add,
                        newItems, e.NewStartingIndex));

                    break;
                case NotifyCollectionChangedAction.Remove:
                    oldItems = new List<TTargetType>();

                    for (var i = 0; i < e.OldItems.Count; ++i)
                    {
                        var index = e.OldStartingIndex + i;

                        oldItems.Add(_transformedValues[index]);
                    }

                    _transformedValues.RemoveRange(e.OldStartingIndex, e.OldItems.Count);

                    RaiseCollectionChanged(this, new NotifyCollectionChangedEventArgs(
                        NotifyCollectionChangedAction.Remove,
                        oldItems, e.OldStartingIndex));

                    break;
                case NotifyCollectionChangedAction.Replace:
                    newItems = new List<TTargetType>();
                    oldItems = new List<TTargetType>();

                    for (var i = 0; i < e.NewItems.Count; ++i)
                    {
                        var index = e.NewStartingIndex + i;
                        var innerElement = (TInnerType) e.NewItems[i];

                        var transformedElement = _transformationFunction(innerElement);

                        newItems.Add(transformedElement);
                        oldItems.Add(_transformedValues[index]);

                        _transformedValues[index] = transformedElement;
                    }

                    RaiseCollectionChanged(this, new NotifyCollectionChangedEventArgs(
                        NotifyCollectionChangedAction.Replace,
                        newItems, oldItems, e.NewStartingIndex));

                    break;
                case NotifyCollectionChangedAction.Reset:
                    _transformedValues.Clear();

                    CreateCollection();

                    RaiseCollectionChanged(this, new NotifyCollectionChangedEventArgs(
                        NotifyCollectionChangedAction.Reset));

                    break;
                case NotifyCollectionChangedAction.Move:
                    newItems = new List<TTargetType>();

                    if (e.NewItems.Count != 1)
                    {
                        throw new NotImplementedException("No idea how this is supposed to work");
                    }

                    //The names "at old index"/"at new index" refer to the positions *before* the move method was called.
                    var valueAtOldIndex = _transformedValues[e.OldStartingIndex];
                    var valueAtNewIndex = _transformedValues[e.NewStartingIndex];

                    //Swap positions
                    _transformedValues[e.NewStartingIndex] = valueAtOldIndex;
                    _transformedValues[e.OldStartingIndex] = valueAtNewIndex;

                    //The value at the old index *before* the move method was called is the value that is at the new index *after* it.
                    //We add that value to the list
                    newItems.Add(valueAtOldIndex);

                    RaiseCollectionChanged(this, new NotifyCollectionChangedEventArgs(
                        NotifyCollectionChangedAction.Move,
                        newItems, e.NewStartingIndex, e.OldStartingIndex));

                    break;
                default:
                    throw new InvalidOperationException();
            }
        }

        private void RaiseCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (CollectionChanged != null)
            {
                CollectionChanged(sender, e);
            }
        }

        public IEnumerator<TTargetType> GetEnumerator()
        {
            return _transformedValues.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count
        {
            get { return _transformedValues.Count; }
        }
    }
}
