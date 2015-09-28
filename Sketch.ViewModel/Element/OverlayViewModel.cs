using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using PeletonSoft.Sketch.Model.Element;
using PeletonSoft.Sketch.ViewModel.Element.Layout;
using PeletonSoft.Sketch.ViewModel.Element.Null;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Sketch.ViewModel.Interface.Element;
using PeletonSoft.Sketch.ViewModel.Interface.Layout;
using PeletonSoft.Tools.Model.Collection;
using PeletonSoft.Tools.Model.Logic;
using PeletonSoft.Tools.Model.NotifyChanged;

namespace PeletonSoft.Sketch.ViewModel.Element
{
    public sealed class OverlayViewModel : IElementViewModel, ISelectableList<IElementViewModel>, INotifyViewModel<Overlay>
    {
        #region implement INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            this.OnPropertyChanged(PropertyChanged, propertyName);
        }

        private void SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            Action notificator = () => OnPropertyChanged(propertyName);
            notificator.SetField(ref field, value);
        }

        private void SetField<T>(Func<T> getValue, Action<T> setValue, T value, [CallerMemberName] string propertyName = null)
        {
            Action notificator = () => OnPropertyChanged(propertyName);
            notificator.SetField(getValue, setValue, value);
        }
        #endregion

        #region implement IOriginator
        public void RestoreDefault()
        {

        }
        #endregion

        #region implement ISelectableList
        private int _selectedIndex = -1;
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set { SetField(ref _selectedIndex, Below.IsValidIndex(value) ? value : -1); }
        }

        private IElementViewModel NullSelectedItem { get; } = new NullElementViewModel();
        public IElementViewModel SelectedItem => Below.IsValidIndex(SelectedIndex) ? Below[SelectedIndex] : NullSelectedItem;
        #endregion

        #region implment ICollectionItem
        public void AfterInsert()
        {
        }

        public void BeforeDelete()
        {
        }
        #endregion

        #region implement IElementViewModel
        public string Description
        {
            get { return Model.Description; }
            set { SetField(() => Model.Description, v => Model.Description = v, value); }
        }
        public bool Visibility
        {
            get { return SelectedItem.Visibility; }
            set { SelectedItem.Visibility = value; }
        }
        public double Opacity
        {
            get { return SelectedItem.Opacity; }
            set { SelectedItem.Opacity = value; }
        }

        private ILayoutViewModel NullLayoutViewModel { get; } = new NullLayoutViewModel();
        public ILayoutViewModel Layout => SelectedItem != null ? SelectedItem.Layout : NullLayoutViewModel;
        public ICommand MoveToElementCommand { get; set; }
        public IReadOnlyList<IElementViewModel> Below => WorkspaceBit.GetBelowElements(this);

        #endregion

        #region implement IViewModel
        public Overlay Model { get; }
        #endregion

        public OverlayViewModel(IWorkspaceBit workspaceBit, Overlay model)
        {
            WorkspaceBit = workspaceBit;
            Model = model;
            OverHeight = workspaceBit.Screen.Height;
            OverWidth = workspaceBit.Screen.Width;

            workspaceBit.ItemChanged +=
                (sender, args) =>
                {
                    var changeInfo = args.ChangedInfo;
                    OnPropertyChanged(nameof(Below));
                    SelectedIndex = changeInfo.RecalculateIndex(SelectedIndex);
                };

            this
                .SetPropertyChanged(nameof(SelectedIndex), () => OnPropertyChanged(nameof(SelectedItem)))
                .SetPropertyChanged(
                    new[]
                    {
                        nameof(SelectedItem), nameof(OverWidth), nameof(OverHeight),
                        nameof(OverOffsetX), nameof(OverOffsetY)
                    },
                    () => OnPropertyChanged(nameof(OverRect)))
                .SetPropertyChanged(
                    this.ExtractGetter(nameof(SelectedItem), el => el.SelectedItem),
                    nameof(SelectedItem.Visibility),
                    () => OnPropertyChanged(nameof(Visibility)))
                .SetPropertyChanged(
                    this.ExtractGetter(nameof(SelectedItem), el => el.SelectedItem),
                    nameof(SelectedItem.Opacity),
                    () => OnPropertyChanged(nameof(Opacity)))
                .SetPropertyChanged(
                    this.ExtractGetter(nameof(SelectedItem), el => el.SelectedItem),
                    nameof(SelectedItem.Layout),
                    () => OnPropertyChanged(nameof(Layout)));
        }

        private IWorkspaceBit WorkspaceBit { get; }

        public double OverWidth
        {
            get { return Model.OverWidth; }
            set { SetField(() => Model.OverWidth, v => Model.OverWidth = v, value); }
        }
        public double OverHeight
        {
            get { return Model.OverHeight; }
            set { SetField(() => Model.OverHeight, v => Model.OverHeight = v, value); }
        }
        public double OverOffsetX

        {
            get { return Model.OverOffsetX; }
            set { SetField(() => Model.OverOffsetX, v => Model.OverOffsetX = v, value); }
        }
        public double OverOffsetY
        {
            get { return Model.OverOffsetY; }
            set { SetField(() => Model.OverOffsetY, v => Model.OverOffsetY = v, value); }
        }

        public Rect OverRect => Model.GetOverRect();
    }
}
