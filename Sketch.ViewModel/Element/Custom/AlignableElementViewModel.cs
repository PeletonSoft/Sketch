using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using PeletonSoft.Sketch.Model.Element.Custom;
using PeletonSoft.Sketch.Model.Interface.Element;
using PeletonSoft.Sketch.ViewModel.Container;
using PeletonSoft.Sketch.ViewModel.Element.Clothe;
using PeletonSoft.Sketch.ViewModel.Element.Primitive;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Sketch.ViewModel.Interface.Element;
using PeletonSoft.Sketch.ViewModel.Interface.Layout;
using PeletonSoft.Tools.Model.Memento;
using PeletonSoft.Tools.Model.NotifyChanged;

namespace PeletonSoft.Sketch.ViewModel.Element.Custom
{
    public abstract class AlignableElementViewModel : IAlignableElementViewModel
    {
        #region implement INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            this.OnPropertyChanged(PropertyChanged, propertyName);
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            Action notificator = () => OnPropertyChanged(propertyName);
            return notificator.SetField(ref field, value);
        }

        protected bool SetField<T>(Func<T> getValue, Action<T> setValue, T value, [CallerMemberName] string propertyName = null)
        {
            Action notificator = () => OnPropertyChanged(propertyName);
            return notificator.SetField(getValue, setValue, value);
        }
        #endregion

        #region implement IOriginator
        public virtual void RestoreDefault()
        {

        }
        #endregion

        #region implement IAlignable
        public double Width
        {
            get { return Model.Width; }
            set { SetField(() => Width, v => Model.Width = v, value); }
        }

        public double Height
        {
            get { return Model.Height; }
            set { SetField(() => Height, v => Model.Height = v, value); }
        }

        public double OffsetX
        {
            get { return Model.OffsetX; }
            set { SetField(() => OffsetX, v => Model.OffsetX = v, value); }
        }

        public double OffsetY
        {
            get { return Model.OffsetY; }
            set { SetField(() => OffsetY, v => Model.OffsetY = v, value); }
        }

        #endregion

        #region implement ILayoutable
        private ILayoutViewModel _layout;
        public ILayoutViewModel Layout
        {
            get { return _layout; }
            set { SetField(ref _layout, value); }
        }

        #endregion

        #region implement IElementViewModel
        public IAlignableElement Model { get; private set; }
        public string Description
        {
            get { return Model.Description; }
            set { SetField(() => Description, v => Model.Description = v, value); }
        }
        public bool Visibility
        {
            get { return Model.Visibility; }
            set { SetField(() => Visibility, v => Model.Visibility = v, value); }
        }
        public double Opacity
        {
            get { return Model.Opacity; }
            set { SetField(() => Opacity, v => Model.Opacity = v, value); }
        }

        public IList<IElementViewModel> Below
        {
            get { return WorkspaceBit.GetBelowElements(this); }
        }
        public ICommand MoveToElementCommand { get; set; }
        public void AfterInsert()
        {
        }

        public void BeforeDelete()
        {
        }

        public IClotheViewModel Clothe { get; private set; }
        #endregion
        protected AlignableElementViewModel(IWorkspaceBit workspaceBit)
        {
            Model = new AlignableElement();

            WorkspaceBit = workspaceBit;

            Model.Opacity = 1;
            Model.Visibility = true;

            Model.OffsetX = 0;
            Model.OffsetY = 0;
            Model.Width = Screen.Width;
            Model.Height = Screen.Height;

            var layouts = new LayoutViewModels(WorkspaceBit, this);
            Layouts = layouts;
            Layout = layouts.LeftLayout;
            Clothe = new ClotheViewModel(WorkspaceBit, new ClotheCalculateStrategy(this));
        }

        public IContainerOriginator<ILayoutViewModel> Layouts { get; private set; }

        public IWorkspaceBit WorkspaceBit { get; private set; }

        protected IScreenViewModel Screen
        {
            get { return WorkspaceBit.Screen; }
        }

    }
}
