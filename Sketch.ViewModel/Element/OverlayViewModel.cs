using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using PeletonSoft.Sketch.Model.Element;
using PeletonSoft.Sketch.ViewModel.Element.Layout;
using PeletonSoft.Sketch.ViewModel.Element.Null;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Sketch.ViewModel.Interface.Element;
using PeletonSoft.Sketch.ViewModel.Interface.Layout;
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

        private void OnPropertyChanged<T>(Expression<Func<OverlayViewModel, T>> expression)
        {
            expression.OnPropertyChanged(OnPropertyChanged);
        }

        #endregion

        #region implement IOriginator
        public void RestoreDefault()
        {

        }
        #endregion

        #region implement ISelectableList
        public bool IsValidIndex(int index)
        {
            return index >= 0 && index < Below.Count();
        }
        private int _selectedIndex = -1;
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set { SetField(ref _selectedIndex, IsValidIndex(value) ? value : -1); }
        }

        private readonly IElementViewModel _nullSelectedItem;

        public IElementViewModel SelectedItem
        {
            get { return IsValidIndex(SelectedIndex) ? Below[SelectedIndex] : _nullSelectedItem; }
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
        private readonly ILayoutViewModel _nullLayoutViewModel;
        public ILayoutViewModel Layout
        {
            get { return SelectedItem != null ? SelectedItem.Layout : _nullLayoutViewModel; }
        }
        public ICommand MoveToElementCommand { get; set; }
        public IList<IElementViewModel> Below
        {
            get { return WorkspaceBit.GetBelowElements(this); }
        }
        public void AfterInsert()
        {
        }
        public void BeforeDelete()
        {
        }
        #endregion

        #region implement IViewModel
        public Overlay Model { get; private set; }
        #endregion

        public OverlayViewModel(IWorkspaceBit workspaceBit, Overlay model)
        {
            WorkspaceBit = workspaceBit;
            Model = model;
            OverHeight = workspaceBit.Screen.Height;
            OverWidth = workspaceBit.Screen.Width;
            _nullLayoutViewModel = new NullLayoutViewModel();
            _nullSelectedItem = new NullElementViewModel();

            workspaceBit.ElementListChanged +=
                (sender, args) =>
                {
                    var changeInfo = args.ChangedInfo;
                    OnPropertyChanged(el => Below);
                    SelectedIndex = changeInfo.RecalculateIndex(SelectedIndex);
                };

            this
                .SetPropertyChanged(el => el.SelectedIndex, () => OnPropertyChanged(el => el.SelectedItem))
                .SetPropertyChanged(
                    new[]
                    {
                        this.GetPropertyName(el => el.SelectedItem),
                        this.GetPropertyName(el => el.OverWidth),
                        this.GetPropertyName(el => el.OverHeight),
                        this.GetPropertyName(el => el.OverOffsetX),
                        this.GetPropertyName(el => el.OverOffsetY)
                    },
                    () => OnPropertyChanged(el => el.OverRect))
                .SetPropertyChanged(
                    el => el.SelectedItem, si => si.Visibility,
                    () => OnPropertyChanged(el => el.Visibility))
                .SetPropertyChanged(
                    el => el.SelectedItem, si => si.Opacity,
                    () => OnPropertyChanged(el => el.Opacity))
                .SetPropertyChanged(
                    el => el.SelectedItem, si => si.Layout,
                    () => OnPropertyChanged(el => el.Layout));
        }

        private IWorkspaceBit WorkspaceBit { get; set; }

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

        public Rect OverRect
        {
            get { return Model.GetOverRect(); }
        }

        
    }
}
