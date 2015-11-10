using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using PeletonSoft.Sketch.Model.Interface.Element;
using PeletonSoft.Sketch.ViewModel.Container;
using PeletonSoft.Sketch.ViewModel.DataTransfer.Element.Custom;
using PeletonSoft.Sketch.ViewModel.DataTransfer.Interface;
using PeletonSoft.Sketch.ViewModel.Element.Primitive;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Sketch.ViewModel.Interface.Element;
using PeletonSoft.Sketch.ViewModel.Interface.Layout;
using PeletonSoft.Tools.Model.Collection;
using PeletonSoft.Tools.Model.Memento;
using PeletonSoft.Tools.Model.Memento.Container;
using PeletonSoft.Tools.Model.ObjectEvent.NotifyChanged;
using static PeletonSoft.Tools.Model.ObjectEvent.EventAction;
using static PeletonSoft.Tools.Model.ObjectEvent.NotifyChanged.NotifyPropertyChangedHelper;


namespace PeletonSoft.Sketch.ViewModel.Element.Custom
{
    public abstract class AlignableElementViewModel : IAlignableElementViewModel, IOriginator<AlignableElementDataTransfer>
    {
        #region implement INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            this.OnPropertyChanged(PropertyChanged, propertyName);

        protected void SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null) =>
            SetFieldValue(() => OnPropertyChanged(propertyName), ref field, value);

        protected void SetField<T>(Func<T> getValue, Action<T> setValue, T value, [CallerMemberName] string propertyName = null) =>
            SetFieldValue(() => OnPropertyChanged(propertyName), getValue, setValue, value);

        #endregion

        #region implement IOriginator

        public virtual void RestoreDefault() => DoNothing();
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
        private ILayoutViewModel _layout;
        public ILayoutViewModel Layout
        {
            get { return _layout; }
            set { SetField(ref _layout, value); }
        }
        public IContainerOriginator<ILayoutViewModel> Layouts { get; }
        #endregion

        #region implement IElementViewModel
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

        public IReadOnlyList<IElementViewModel> Below => WorkspaceBit.GetBelowElements(this);
        public ICommand MoveToElementCommand { get; set; }
        public void AfterInsert() => DoNothing();
        public void BeforeDelete() => DoNothing();

        #endregion

        #region implement IViewModel
        public IAlignableElement Model { get; }
        #endregion

        #region IClotheableViewModel
        public IClotheViewModel Clothe { get; }

        #endregion

        protected AlignableElementViewModel(IWorkspaceBit workspaceBit, IAlignableElement model)
        {
            Model = model;

            WorkspaceBit = workspaceBit;

            Model.Opacity = 1;
            Model.Visibility = true;

            Model.OffsetX = 0;
            Model.OffsetY = 0;
            Model.Width = Screen.Width;
            Model.Height = Screen.Height;

            Layouts = new LayoutViewModels(WorkspaceBit, this);

            Layout = Layouts.Default;
            Clothe = new ClotheViewModel(WorkspaceBit, Model.Clothe);
        }

        protected IWorkspaceBit WorkspaceBit { get; }
        protected IScreenViewModel Screen => WorkspaceBit.Screen;


        public abstract IElementDataTransfer CreateState();

        public virtual void Save(IElementDataTransfer state)
        {
            (this as IOriginator<AlignableElementDataTransfer>).Save((AlignableElementDataTransfer)state);
        }

        public virtual void Restore(IElementDataTransfer state)
        {
            (this as IOriginator<AlignableElementDataTransfer>).Restore((AlignableElementDataTransfer)state);
        }

        AlignableElementDataTransfer IOriginator<AlignableElementDataTransfer>.CreateState()
        {
            return (AlignableElementDataTransfer)CreateState();
        }

        void IOriginator<AlignableElementDataTransfer>.Save(AlignableElementDataTransfer state)
        {
            state.Width = Width;
            state.Height = Height;
            state.OffsetX = OffsetX;
            state.OffsetY = OffsetY;
            state.Visibility = Visibility;
            state.Opacity = Opacity;
            state.Description = Description;
            state.Layout = Layouts.GetKeyByValue(Layout);
        }

        void IOriginator<AlignableElementDataTransfer>.Restore(AlignableElementDataTransfer state)
        {
            Width = state.Width;
            Height = state.Height;
            OffsetX = state.OffsetX;
            OffsetY = state.OffsetY;
            Visibility = state.Visibility;
            Opacity = state.Opacity;
            Description = state.Description;
            Layout = Layouts.GetValueByKeyOrDefault(state.Layout);
        }


    }
}
