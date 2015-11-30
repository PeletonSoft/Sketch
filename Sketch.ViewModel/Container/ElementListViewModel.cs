using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using PeletonSoft.Sketch.ViewModel.DataTransfer.Interface;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Sketch.ViewModel.Interface.Container;
using PeletonSoft.Sketch.ViewModel.Interface.Element;
using PeletonSoft.Tools.Model.Collection;
using PeletonSoft.Tools.Model.Dragable;
using PeletonSoft.Tools.Model.Memento;
using PeletonSoft.Tools.Model.MetaData;
using PeletonSoft.Tools.Model.ObjectEvent.ChangedItem;
using PeletonSoft.Tools.Model.ObjectEvent.ChangedItem.ChangedInfo;
using PeletonSoft.Tools.Model.ObjectEvent.NotifyChanged;
using PeletonSoft.Tools.Model.ObjectEvent.Render;
using static PeletonSoft.Tools.Model.ObjectEvent.EventAction;
using static PeletonSoft.Tools.Model.ObjectEvent.NotifyChanged.NotifyPropertyChangedHelper;

namespace PeletonSoft.Sketch.ViewModel.Container
{
    public sealed class ElementListViewModel : IElementListViewModel
    {
        #region implement INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName) =>
            this.OnPropertyChanged(PropertyChanged, propertyName);

        private void SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null) =>
            SetFieldValue(() => OnPropertyChanged(propertyName), ref field, value);

        #endregion

        #region implement INotifyItemChanged

        public event ItemChangedEventHandler ItemChanged;

        private void OnItemChanged(ItemChangedInfo changedInfo)
        {
            ItemChanged?.Invoke(this, new ItemChangedEventArgs(changedInfo));
        }

        #endregion

        #region implement IOriginator

        public void RestoreDefault() => DoNothing();

        #endregion

        #region implement IContainer

        public IEnumerable<IContainerRecord<IElementViewModel>> Items
        {
            get
            {
                return List.Select(el => new ContainerRecord<IElementViewModel>(
                    el.GetType().ToString(), el.GetType(), el));
            }
        }

        public IElementViewModel Default => null;

        #endregion

        #region implement INotifyOpacityMaskRenderChanged

        public RenderChangedDispatcher<IElementViewModel, IElementViewModel, IEnumerable<Point>> RenderChangedDispatcher
        { get; }

        #endregion

        #region implement ISelectableList

        private int _selectedIndex;

        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set { SetField(ref _selectedIndex, List.IsValidIndex(value) ? value : -1); }
        }

        public IElementViewModel SelectedItem => List.IsValidIndex(SelectedIndex) ? List[SelectedIndex] : null;

        #endregion

        #region implement IChangeableCollection

        public void MoveTo(int sourceIndex, int destinationIndex) =>
            this.DoMoveTo(List, OnItemChanged, sourceIndex, destinationIndex);

        public bool IsEmpty => this.IsEmpty();
        public void Remove() => this.DoRemove(List, OnItemChanged);
        public void MoveDown() => this.DoMoveDown(List, OnItemChanged);

        public void MoveUp() => this.DoMoveUp(List, OnItemChanged);

        public void Clear() => this.DoClear(List, OnItemChanged);

        public void Append(IElementViewModel element)
        {
            element.MoveToElementCommand = MoveToElementCommand;
            this.DoAppend(List, OnItemChanged, element);
        }

        #endregion

        private readonly NotifyChangedCollection<IElementViewModel> _list;
        public INotifyChangedReadOnlyCollection<IElementViewModel> Collection => _list;
        private IList<IElementViewModel> List => _list;

        private void MoveTo(DataTransition<IElementViewModel, IElementViewModel> dt) =>
            MoveTo(List.IndexOf(dt.Source), List.IndexOf(dt.Destination));

        private void CreateElement(DataTransition<IElementFactoryViewModel<IElementViewModel>, IElementListViewModel> dt)
            =>
                AppendElement(dt.Source);

        public IElementViewModel AppendElement(IElementFactoryViewModel<IElementViewModel> factory)
        {
            var element = factory.CreateElement(WorkspaceBit);
            Append(element);
            return element;
        }

        public IReadOnlyList<IElementViewModel> GetBelow(IElementViewModel element)
        {
            return List.GetBelow(element).ToList().AsReadOnly();
        }

        private readonly Lazy<ICommand> _lazyCreateElementCommand;
        public ICommand CreateElementCommand => _lazyCreateElementCommand.Value;

        private readonly Lazy<ICommand> _lazyMoveUpElementCommand;
        public ICommand MoveUpElementCommand => _lazyMoveUpElementCommand.Value;

        private readonly Lazy<ICommand> _lazyMoveDownElementCommand;
        public ICommand MoveDownElementCommand => _lazyMoveDownElementCommand.Value;

        private readonly Lazy<ICommand> _lazyMoveToElementCommand;
        public ICommand MoveToElementCommand => _lazyMoveToElementCommand.Value;

        private readonly Lazy<ICommand> _lazyRemoveElementCommand;
        public ICommand RemoveElementCommand => _lazyRemoveElementCommand.Value;

        private readonly Lazy<ICommand> _lazyUnselectElementCommand;
        public ICommand UnselectElementCommand => _lazyUnselectElementCommand.Value;

        public ElementListViewModel(WorkspaceBit workspaceBit)
        {
            WorkspaceBit = workspaceBit;
            ItemChanged += (sender, args) =>
            {
                if (args.ChangedInfo.IsEmptyChanged(List.Count))
                {
                    OnPropertyChanged(nameof(IsEmpty));
                }
            };

            RenderChangedDispatcher =
                new RenderChangedDispatcher<IElementViewModel, IElementViewModel, IEnumerable<Point>>();

            _list = new NotifyChangedCollection<IElementViewModel>();
            this.Unselect();

            _lazyCreateElementCommand = new Lazy<ICommand>(
                () => WorkspaceBit.CommandFactory.CreateCommand<DataTransition>(
                    param =>
                        CreateElement(param.Cast<IElementFactoryViewModel<IElementViewModel>, IElementListViewModel>())));
            _lazyMoveUpElementCommand = new Lazy<ICommand>(
                () => WorkspaceBit.CommandFactory.CreateCommand(MoveUp, this.AllowMoveUp));
            _lazyMoveDownElementCommand = new Lazy<ICommand>(
                () => WorkspaceBit.CommandFactory.CreateCommand(MoveDown, this.AllowMoveDown));
            _lazyRemoveElementCommand = new Lazy<ICommand>(
                () => WorkspaceBit.CommandFactory.CreateCommand(Remove, this.AllowRemove));
            _lazyMoveToElementCommand = new Lazy<ICommand>(
                () => WorkspaceBit.CommandFactory.CreateCommand<DataTransition>(
                    param => MoveTo(param.Cast<IElementViewModel, IElementViewModel>())));
            _lazyUnselectElementCommand = new Lazy<ICommand>(
                () => WorkspaceBit.CommandFactory.CreateCommand(this.Unselect, this.AllowUnselect));

            this
                .SetPropertyChanged(nameof(SelectedIndex), () => OnPropertyChanged(nameof(SelectedItem)));
        }

        private WorkspaceBit WorkspaceBit { get; }

        public IScreenViewModel Screen => WorkspaceBit.Screen;
        public IEnumerable<IElementFactoryViewModel<IElementViewModel>> Factories => WorkspaceBit.Factories;


        public void Save(IListDataTransfer<IElementDataTransfer> state)
        {
            List.ToList()
                .ForEach(item => state.List.Add(item.GetDataTransfer()));
        }

        public void Restore(IListDataTransfer<IElementDataTransfer> state)
        {
            Clear();
            foreach (var item in state.List)
            {
                var factory = Factories
                    .SelectMany(f => f.GetType().GetGenericArgs(typeof (IElementFactoryViewModel<>))
                        .Select(t => new {Factory = f, Type = t}))
                    .Where(rec => rec.Type.IsSealed && rec.Type.Name == item.Type)
                    .Select(rec => rec.Factory)
                    .FirstOrDefault();

                if (factory == null)
                {
                    continue;
                }

                var element = AppendElement(factory);
                element.Restore(item.Content);
            }
        }


        public IListDataTransfer<IElementDataTransfer> CreateState() => new ListDataTransfer<IElementDataTransfer>();
    }
}
