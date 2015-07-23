using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Sketch.ViewModel.Interface.Element;
using PeletonSoft.Tools.Model.Collection;
using PeletonSoft.Tools.Model.Dragable;
using PeletonSoft.Tools.Model.Memento.Container;
using PeletonSoft.Tools.Model.NotifyChanged;
using PeletonSoft.Tools.Model.NotifyChanged.ElementList;
using PeletonSoft.Tools.Model.NotifyChanged.ElementList.ChangedInfo;
using PeletonSoft.Tools.Model.NotifyChanged.ElementList.ChangedInfo.Primitive;
using PeletonSoft.Tools.Model.NotifyChanged.Render;

namespace PeletonSoft.Sketch.ViewModel.Container
{
    public sealed class ElementListViewModel : IElementListViewModel
    {
        #region implement INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            this.OnPropertyChanged(PropertyChanged, propertyName);
        }

        private bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            Action notificator = () => OnPropertyChanged(propertyName);
            return notificator.SetField(ref field, value);
        }
        private void OnPropertyChanged<T>(Expression<Func<ElementListViewModel, T>> expression)
        {
            expression.OnPropertyChanged(OnPropertyChanged);
        }
        #endregion

        #region implement INotifyElementListChanged
        public event ElementListChangedEventHandler ElementListChanged;

        private void OnElementListChanged(ElementListChangedInfo changedInfo)
        {
            var handler = ElementListChanged;
            if (handler != null)
            {
                handler(this, new ElementListChangedEventArgs(changedInfo));
            }
        }
        #endregion

        #region implement IOriginator
        public void RestoreDefault()
        {

        }
        #endregion

        #region implement IContainer<IElementViewModel>

        public IEnumerable<IContainerRecord<IElementViewModel>> Items
        {
            get
            {
                return List.Select(el => new ContainerRecord<IElementViewModel>(
                        el.GetType().ToString(), el.GetType(), el));
            }
        }

        public IElementViewModel Default
        {
            get { return null; }
        }

        #endregion

        #region implement INotifyOpacityMaskRenderChanged
        public RenderChangedDispatcher<IElementViewModel, IElementViewModel, IEnumerable<Point>> RenderChangedDispatcher { get; private set; }
        #endregion

        #region implement ISelectableList
        private int _selectedIndex;
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set { SetField(ref _selectedIndex, IsValidIndex(value) ? value : -1); }
        }

        public bool IsValidIndex(int index)
        {
            return index >= 0 && index < List.Count;
        }
        public IElementViewModel SelectedItem
        {
            get { return IsValidIndex(SelectedIndex) ? List[SelectedIndex] : null; }
        }
        #endregion

        private readonly NotifyChangedCollection<IElementViewModel> _list;
        public INotifyChangedReadOnlyCollection<IElementViewModel> Collection
        {
            get { return _list; } 
        }

        private IList<IElementViewModel> List
        {
            get { return _list; }
        }

        public bool IsEmpty
        {
            get { return !List.Any(); }
        }

        private void CreateElement(object param)
        {
            var dataTransition = (DataTransition) param;
            var factory = (IElementFactoryViewModel<IElementViewModel>)dataTransition.Source;

            AddElement(factory);
        }

        public IElementViewModel AddElement(IElementFactoryViewModel<IElementViewModel> factory)
        {
            var element = factory.CreateElement(WorkspaceBit);
            element.MoveToElementCommand = MoveToElementCommand;
            List.Add(element);
            SelectedIndex = List.IndexOf(element);
            if (List.Count == 1)
            {
                OnPropertyChanged(l => l.IsEmpty);
            }
            element.AfterInsert();
            return element;
        }

        private readonly Lazy<ICommand> _lazyCreateElementCommand;
        public ICommand CreateElementCommand
        {
            get { return _lazyCreateElementCommand.Value; }
        }

        private readonly Lazy<ICommand> _lazyMoveUpElementCommand;

        public IList<IElementViewModel> GetBelowElements(IElementViewModel element)
        {
            var index = List.IndexOf(element);
            var result = List.Where((el, i) => i < index);
            return result.ToList();    
        }

        public ICommand MoveUpElementCommand
        {
            get { return _lazyMoveUpElementCommand.Value; }
        }

        private readonly Lazy<ICommand> _lazyMoveDownElementCommand;

        public ICommand MoveDownElementCommand
        {
            get { return _lazyMoveDownElementCommand.Value; }
        }

        private readonly Lazy<ICommand> _lazyMoveToElementCommand;

        public ICommand MoveToElementCommand
        {
            get { return _lazyMoveToElementCommand.Value; }
        }

        private readonly Lazy<ICommand> _lazyRemoveElementCommand;

        public ICommand RemoveElementCommand
        {
            get { return _lazyRemoveElementCommand.Value; }
        }
        private readonly Lazy<ICommand> _lazyUnselectElementCommand;

        public ICommand UnselectElementCommand
        {
            get { return _lazyUnselectElementCommand.Value; }
        }

        public ElementListViewModel(WorkspaceBit workspaceBit)
        {
            WorkspaceBit = workspaceBit;
            RenderChangedDispatcher =
                new RenderChangedDispatcher<IElementViewModel, IElementViewModel, IEnumerable<Point>>();

            _list = new NotifyChangedCollection<IElementViewModel>();
            Unselect();

            _lazyCreateElementCommand = new Lazy<ICommand>(
                () => WorkspaceBit.CommandFactory.CreateCommand(CreateElement));

            _lazyMoveUpElementCommand = new Lazy<ICommand>(
                () => WorkspaceBit.CommandFactory.CreateCommand(MoveUp,
                    () => SelectedIndex >= 1 && SelectedIndex < List.Count));

            _lazyMoveDownElementCommand = new Lazy<ICommand>(
                () => WorkspaceBit.CommandFactory.CreateCommand(MoveDown,
                    () => SelectedIndex >= 0 && SelectedIndex < List.Count - 1));

            _lazyRemoveElementCommand = new Lazy<ICommand>(
                () => WorkspaceBit.CommandFactory.CreateCommand(Remove,
                    () => SelectedIndex >= 0 && SelectedIndex < List.Count));

            _lazyMoveToElementCommand = new Lazy<ICommand>(
                () => WorkspaceBit.CommandFactory.CreateCommand(
                    param => MoveTo((DataTransition)param)));

            _lazyUnselectElementCommand = new Lazy<ICommand>(
                () => WorkspaceBit.CommandFactory.CreateCommand(Unselect,
                    () => SelectedIndex >= 0));
            this
                .SetPropertyChanged(el => el.SelectedIndex, () => OnPropertyChanged(el => el.SelectedItem));
        }

        private WorkspaceBit WorkspaceBit { get; set; }

        public IScreenViewModel Screen
        {
            get { return WorkspaceBit.Screen; }
        }

        public IEnumerable<IElementFactoryViewModel<IElementViewModel>> Factories
        {
            get { return WorkspaceBit.Factories; }
        }

        private void MoveTo(DataTransition dataTransition)
        {
            var source = (IElementViewModel) dataTransition.Source;
            var destination = (IElementViewModel)dataTransition.Destination;

            MoveTo(List.IndexOf(source), List.IndexOf(destination));
        }

        private void MoveTo(int sourceIndex, int destinationIndex)
        {
            if (sourceIndex != destinationIndex)
            {
                var source = List[sourceIndex];

                List.Remove(source);
                List.Insert(destinationIndex, source);

                SelectedIndex = destinationIndex;

                var changedInfo = new MoveElementListChangedInfo
                    (sourceIndex, destinationIndex);
                OnElementListChanged(changedInfo);
            }
        }

        private void Remove()
        {
            if (IsValidIndex(SelectedIndex))
            {
                var index = SelectedIndex;
                SelectedItem.BeforeDelete();
                List.RemoveAt(index);
                SelectedIndex = -1;

                var changedInfo = new RemoveElementListChangedInfo(index);
                OnElementListChanged(changedInfo);

                if (!IsEmpty)
                {
                    OnPropertyChanged(l => l.IsEmpty);
                }
            }
        }

        private void MoveDown()
        {
            if (SelectedIndex >= 0 && SelectedIndex + 1 < List.Count)
            {
                MoveTo(SelectedIndex, SelectedIndex + 1);
            }
        }

        private void MoveUp()
        {
            if (SelectedIndex - 1 >= 0 && SelectedIndex < List.Count)
            {
                MoveTo(SelectedIndex, SelectedIndex - 1);
            }
        }

        public void Clear()
        {
            while (!IsEmpty)
            {
                SelectedIndex = List.Count - 1;
                Remove();
            }
        }

        public void Unselect()
        {
            SelectedIndex = -1;
        }
        
    }
}
