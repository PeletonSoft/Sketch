using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Sketch.ViewModel.Interface.Element;
using PeletonSoft.Tools.Model.Dragable;
using PeletonSoft.Tools.Model.NotifyChanged.ElementList;
using PeletonSoft.Tools.Model.NotifyChanged.ElementList.ChangedInfo;
using PeletonSoft.Tools.Model.NotifyChanged.ElementList.ChangedInfo.Primitive;
using PeletonSoft.Tools.Model.NotifyChanged.Render;

namespace PeletonSoft.Sketch.ViewModel.Container
{
    public sealed class ElementListViewModel : IElementListViewModel
    {
        #region INotifyPropertyChanged imlement
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        #region INotifyElementListChanged implement
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

        #region IOriginator implement
        public void RestoreDefault()
        {

        }
        #endregion

        #region IContainer<IElementViewModel> implement
        public IEnumerable<IElementViewModel> Items
        {
            get
            {
                return List;
            }
        }
        #endregion

        private ObservableCollection<IElementViewModel> List { get; set; }

        private int _selectedIndex;
        public int SelectedIndex
        {
            get
            {
                return _selectedIndex;
            }
            set
            {
                if (value != _selectedIndex)
                {
                    _selectedIndex = value;
                    OnPropertyChanged("SelectedIndex");
                    OnPropertyChanged("SelectedItem");
                }
            }
        }
        public IElementViewModel SelectedItem
        {
            get
            {
                var isValid = SelectedIndex >= 0 && 
                    SelectedIndex < Items.Count();
                return isValid ? List[SelectedIndex] : null;
            }
        }

        public bool IsEmpty
        {
            get
            {
                return !List.Any();
            }
        }

        private void DoCreateElement(object param)
        {
            var dataTransition = (DataTrasition) param;
            var factory = (IElementFactoryViewModel<IElementViewModel>)dataTransition.Source;

            AddElement(factory);
        }

        public IElementViewModel AddElement(IElementFactoryViewModel<IElementViewModel> factory)
        {
            var element = factory.CreateElement(WorkspaceBit);
            element.MoveToElementCommand = MoveToElementCommand;
            List.Add(element);
            SelectedIndex = List.IndexOf(element);
            if (List.Count() == 1)
            {
                OnPropertyChanged("IsEmpty");
            }
            element.AfterInsert();
            return element;
        }

        private readonly Lazy<ICommand> _createElementCommandLazy;
        public ICommand CreateElementCommand { get { return _createElementCommandLazy.Value; } }

        private readonly Lazy<ICommand> _moveUpElementCommandLazy;
        public ICommand MoveUpElementCommand { get { return _moveUpElementCommandLazy.Value; } }

        private readonly Lazy<ICommand> _moveDownElementCommandLazy;
        public ICommand MoveDownElementCommand { get { return _moveDownElementCommandLazy.Value; } }

        private readonly Lazy<ICommand> _moveToElementCommandLazy;
        public ICommand MoveToElementCommand { get { return _moveToElementCommandLazy.Value; } }

        private readonly Lazy<ICommand> _removeElementCommandLazy;
        public ICommand RemoveElementCommand { get { return _removeElementCommandLazy.Value; } }

        public ElementListViewModel(WorkspaceBit workspaceBit)
        {
            WorkspaceBit = workspaceBit;
            RenderChangedDispatcher = new RenderChangedDispatcher<IElementViewModel, IElementViewModel, IEnumerable<Point>>();

            List = new ObservableCollection<IElementViewModel>();
            SelectedIndex = -1;

            _createElementCommandLazy = new Lazy<ICommand>(
                () => WorkspaceBit.CommandFactory.CreateCommand(DoCreateElement));

            _moveUpElementCommandLazy = new Lazy<ICommand>(
                () => WorkspaceBit.CommandFactory.CreateCommand(DoMoveUpElement,
                    () => SelectedIndex >= 1 && SelectedIndex < List.Count()));

            _moveDownElementCommandLazy = new Lazy<ICommand>(
                () => WorkspaceBit.CommandFactory.CreateCommand(DoMoveDownElement,
                    () => SelectedIndex >= 0 && SelectedIndex < List.Count() - 1));

            _removeElementCommandLazy = new Lazy<ICommand>(
                () => WorkspaceBit.CommandFactory.CreateCommand(DoRemoveElement,
                    () => SelectedIndex >= 0 && SelectedIndex < List.Count()));

            _moveToElementCommandLazy = new Lazy<ICommand>(
                () => WorkspaceBit.CommandFactory.CreateCommand(DoMoveToElement));
        }

        private WorkspaceBit WorkspaceBit { get; set; }

        public IScreenViewModel Screen
        {
            get
            {
                return WorkspaceBit.Screen; 
            }
        }

        public IEnumerable<IElementFactoryViewModel<IElementViewModel>> Factories
        {
            get
            {
                return WorkspaceBit.Factories;
            }
        }
        private void DoMoveToElement(object param)
        {
            var dataTransition = (DataTrasition)param;
            var source = (IElementViewModel) dataTransition.Source;
            var destination = (IElementViewModel)dataTransition.Destionation;

            Move(List.IndexOf(source), List.IndexOf(destination));
        }

        private void Move(int sourceIndex, int destinationIndex)
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

        private void DoRemoveElement()
        {
            if (Items.Any() && SelectedIndex < Items.Count())
            {
                var index = SelectedIndex;
                List.RemoveAt(index);
                SelectedIndex = -1;

                var changedInfo = new RemoveElementListChangedInfo(index);
                OnElementListChanged(changedInfo);

                if (!List.Any())
                {
                    OnPropertyChanged("IsEmpty");
                }
            }
        }

        private void DoMoveDownElement()
        {
            if (SelectedIndex >= 0 && SelectedIndex + 1 < Items.Count())
            {
                Move(SelectedIndex, SelectedIndex + 1);
            }
        }

        private void DoMoveUpElement()
        {
            if (SelectedIndex - 1 >= 0 && SelectedIndex < Items.Count())
            {
                Move(SelectedIndex, SelectedIndex - 1);
            }
        }

        public void Clear()
        {
            while (!IsEmpty)
            {
                SelectedIndex = 0;
                DoRemoveElement();
            }
        }


        public RenderChangedDispatcher<IElementViewModel, IElementViewModel, IEnumerable<Point>> RenderChangedDispatcher { get; private set; }
    }
}
