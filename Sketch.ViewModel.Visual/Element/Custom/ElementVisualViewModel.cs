using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using PeletonSoft.Sketch.ViewModel.Interface.Element;
using PeletonSoft.Sketch.ViewModel.Interface.Visual;
using PeletonSoft.Sketch.ViewModel.Visual.Element.Primitive;
using PeletonSoft.Tools.Model.ObjectEvent.NotifyChanged;
using static PeletonSoft.Tools.Model.ObjectEvent.NotifyChanged.NotifyPropertyChangedHelper;

namespace PeletonSoft.Sketch.ViewModel.Visual.Element.Custom
{
    public abstract class ElementVisualViewModel : IElementVisualViewModel<IElementViewModel>
    {
        #region implement INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName) =>
            this.OnPropertyChanged(PropertyChanged, propertyName);

        protected void SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null) =>
            SetFieldValue(() => OnPropertyChanged(propertyName), ref field, value);
        #endregion


        private ILayoutVisualViewModel _layout;
        public virtual ILayoutVisualViewModel Layout
        {
            get { return _layout; }
            set { SetField(ref _layout, value); }
        }

        protected ElementVisualViewModel(VisualOptions visualOptions, IElementViewModel element)
        {

            VisualOptions = visualOptions;
            Element = element;

            Action setLayout = () => Layout = new LayoutVisualViewModel(VisualOptions, Element.Layout);
            setLayout();

            Element
                .SetPropertyChanged(nameof(Element.Visibility), () => OnPropertyChanged(nameof(Visibility)))
                .SetPropertyChanged(nameof(Element.Opacity), () => OnPropertyChanged(nameof(Opacity)))
                .SetPropertyChanged(nameof(Element.Layout), setLayout);
        }

        protected VisualOptions VisualOptions { get; }
        public bool Visibility => Element.Visibility;
        public double Opacity => Element.Opacity;
        public IElementViewModel Element { get; }
    }
}
