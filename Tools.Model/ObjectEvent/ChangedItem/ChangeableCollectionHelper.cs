using System;
using System.Collections.Generic;
using PeletonSoft.Tools.Model.Collection;
using PeletonSoft.Tools.Model.ObjectEvent.ChangedItem.ChangedInfo;

namespace PeletonSoft.Tools.Model.ObjectEvent.ChangedItem
{
    public static class ChangeableCollectionHelper
    {
        public static int Count<T>(this IChangeableCollection<T> collection) => 
            collection.Collection.Count;

        public static bool AllowMoveUp<T>(this IChangeableCollection<T> collection) => 
            collection.SelectedIndex >= 1 && collection.SelectedIndex < collection.Count();

        public static bool AllowMoveDown<T>(this IChangeableCollection<T> collection) => 
            collection.SelectedIndex >= 0 && collection.SelectedIndex < collection.Count() - 1;

        public static bool AllowRemove<T>(this IChangeableCollection<T> collection) => 
            collection.SelectedIndex >= 0 && collection.SelectedIndex < collection.Count();

        public static bool IsEmpty<T>(this IChangeableCollection<T> collection) => 
            collection.Count() == 0;

        public static void DoMoveTo<T>(
            this IChangeableCollection<T> collection, IList<T> list, 
            Action<ItemChangedInfo> onItemChanged, int sourceIndex, int destinationIndex)
        {
            if (sourceIndex != destinationIndex)
            {
                var source = list[sourceIndex];

                list.Remove(source);
                list.Insert(destinationIndex, source);

                collection.SelectedIndex = destinationIndex;

                var changedInfo = new MoveItemChangedInfo
                    (sourceIndex, destinationIndex);
                onItemChanged(changedInfo);
            }
        }

        public static void DoRemove<T>(this IChangeableCollection<T> collection, IList<T> list, 
            Action<ItemChangedInfo> onItemChanged) where T : ICollectionItem
        {
            if (list.IsValidIndex(collection.SelectedIndex))
            {
                var index = collection.SelectedIndex;
                collection.SelectedItem.BeforeDelete();
                list.RemoveAt(index);
                collection.SelectedIndex = -1;

                var changedInfo = new RemoveItemChangedInfo(index);
                onItemChanged(changedInfo);
            }
        }

        public static void DoMoveDown<T>(this IChangeableCollection<T> collection, IList<T> list,
            Action<ItemChangedInfo> onItemChanged)
        {
            if (collection.AllowMoveDown())
            {
                collection.DoMoveTo(list,onItemChanged, collection.SelectedIndex, collection.SelectedIndex + 1);
            }
        }
        public static void DoMoveUp<T>(this IChangeableCollection<T> collection, IList<T> list,
            Action<ItemChangedInfo> onItemChanged)
        {
            if (collection.AllowMoveUp())
            {
                collection.DoMoveTo(list, onItemChanged, collection.SelectedIndex, collection.SelectedIndex - 1);
            }
        }
        public static void DoClear<T>(this IChangeableCollection<T> collection, IList<T> list,
            Action<ItemChangedInfo> onItemChanged) where T : ICollectionItem
        {
            while (!collection.IsEmpty())
            {
                collection.SelectedIndex = list.Count - 1;
                collection.DoRemove(list,onItemChanged);
            }
        }

        public static void DoAppend<T>(this IChangeableCollection<T> collection,
            IList<T> list, Action<ItemChangedInfo> onItemChanged, T element) where T : ICollectionItem
        {
            list.Add(element);
            collection.SelectedIndex = list.IndexOf(element);
            element.AfterInsert();
            var changedInfo = new AppendItemChangedInfo();
            onItemChanged(changedInfo);
        }
    }
}
