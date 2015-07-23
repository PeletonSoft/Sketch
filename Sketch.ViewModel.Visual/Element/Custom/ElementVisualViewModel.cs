using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using PeletonSoft.Sketch.ViewModel.Interface.Element;
using PeletonSoft.Sketch.ViewModel.Interface.Visual;
using PeletonSoft.Sketch.ViewModel.Visual.Element.Primitive;
using PeletonSoft.Tools.Model.NotifyChanged;

namespace PeletonSoft.Sketch.ViewModel.Visual.Element.Custom
{
    public abstract class ElementVisualViewModel : IElementVisualViewModel
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

        private void OnPropertyChanged<T>(Expression<Func<ElementVisualViewModel, T>> expression)
        {
            expression.OnPropertyChanged(OnPropertyChanged);
        }
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
            _element = element;

            Action setLayout = () => Layout = new LayoutVisualViewModel(VisualOptions, Element.Layout);
            setLayout();

            Element
                .SetPropertyChanged(el => el.Visibility, () => OnPropertyChanged(v => v.Visibility))
                .SetPropertyChanged(el => el.Opacity, () => OnPropertyChanged(v => v.Opacity))
                .SetPropertyChanged(el => el.Layout, setLayout);
        }

        protected VisualOptions VisualOptions { get; private set; }

        public bool Visibility
        {
            get { return Element.Visibility; }
        }

        public double Opacity
        {
            get { return Element.Opacity; }
        }
        

        private readonly IElementViewModel _element;
        protected IElementViewModel Element
        {
            get { return _element; }
        }
    }
}
