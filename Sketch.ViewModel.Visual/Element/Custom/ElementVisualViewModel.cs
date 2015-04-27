using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using PeletonSoft.Sketch.ViewModel.Interface.Element;
using PeletonSoft.Sketch.ViewModel.Interface.Visual;
using PeletonSoft.Sketch.ViewModel.Visual.Element.Layout;
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

        #endregion


        private ILayoutVisualViewModel _layout;
        public virtual ILayoutVisualViewModel Layout
        {
            get
            {
                return _layout ?? (_layout = new LayoutVisualViewModel(VisualOptions, Element.Layout));
            }
        }

        public ElementVisualViewModel(VisualOptions visualOptions, IElementViewModel element)
        {

            VisualOptions = visualOptions;
            _element = element;

            Element.SetPropertyChanged("Visibility", () => OnPropertyChanged("Visibility"));
            Element.SetPropertyChanged("Opacity", () => OnPropertyChanged("Opacity"));
            Element.SetPropertyChanged("Rect", () => OnPropertyChanged("Rect"));
            Element.SetPropertyChanged("Layout", () => _layout = null);
        }

        protected VisualOptions VisualOptions { get; set; }

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
