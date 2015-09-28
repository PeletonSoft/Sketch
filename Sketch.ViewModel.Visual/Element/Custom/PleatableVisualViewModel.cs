using System;
using System.Linq.Expressions;
using PeletonSoft.Sketch.ViewModel.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Interface.Element;
using PeletonSoft.Sketch.ViewModel.Interface.Visual;
using PeletonSoft.Sketch.ViewModel.Visual.Element.Primitive;
using PeletonSoft.Tools.Model.NotifyChanged;

namespace PeletonSoft.Sketch.ViewModel.Visual.Element.Custom
{
    public abstract class PleatableVisualViewModel : ElementVisualViewModel, IElementVisualViewModel<PleatableViewModel>
    {
        private void OnPropertyChanged<T>(Expression<Func<PleatableVisualViewModel, T>> expression)
        {
            expression.OnPropertyChanged(OnPropertyChanged);
        }
        public new PleatableViewModel Element
        {
            get { return (PleatableViewModel)base.Element; }
        }

        protected PleatableVisualViewModel(VisualOptions visualOptions, PleatableViewModel element) :
            base(visualOptions, element)
        {
            Element
                .SetPropertyChanged(el => el.WavySurface, () => OnPropertyChanged(v => v.WavySurface));
        }

        public WavySurfaceVisualViewModel WavySurface
        {
            get { return new WavySurfaceVisualViewModel(VisualOptions, Element.WavySurface); }
        }

    }
}
