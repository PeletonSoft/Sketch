using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Windows;
using PeletonSoft.Sketch.ViewModel.Element;
using PeletonSoft.Sketch.ViewModel.Interface.Visual;
using PeletonSoft.Sketch.ViewModel.Visual.Element.Custom;
using PeletonSoft.Tools.Model.NotifyChanged;

namespace PeletonSoft.Sketch.ViewModel.Visual.Element
{
    public sealed class ApplicationVisualViewModel : ElementVisualViewModel, IElementVisualViewModel<ApplicationViewModel>
    {
        private void OnPropertyChanged<T>(Expression<Func<ApplicationVisualViewModel,T>> expression)
        {
            expression.OnPropertyChanged(OnPropertyChanged);
        }

        public ApplicationVisualViewModel(VisualOptions visualOptions, ApplicationViewModel element)
            : base(visualOptions, element)
        {
            Element.SetPropertyChanged(el => el.Points, () => OnPropertyChanged(v => v.Points));
        }

        public new ApplicationViewModel Element
        {
            get { return (ApplicationViewModel)base.Element; }
        }

        public IEnumerable<Point> Points
        {
            get
            {
                var pixelPerUnit = VisualOptions.PixelPerUnit;
                var points = pixelPerUnit.Transform(Element.Points);
                return points;
            }
        }
    }
}
