using System;
using System.Collections.Generic;
using System.Windows;
using PeletonSoft.Sketch.Model.Element;
using PeletonSoft.Sketch.ViewModel.Container;
using PeletonSoft.Sketch.ViewModel.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Element.Outline.Primitive;
using PeletonSoft.Sketch.ViewModel.Element.Transformation.Primitive;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Tools.Model.NotifyChanged;

namespace PeletonSoft.Sketch.ViewModel.Element
{
    public sealed class ApplicationViewModel : AlignableElementViewModel
    {
        public ApplicationViewModel(IWorkspaceBit workspaceBit, Application model)
            : base(workspaceBit, model)
        {
            Width = Screen.Width*0.3;
            Height = Screen.Height*0.3;
            Thickness = 0.3*Layout.Height;

            Outline = Outlines.Band;
            Transformation = Transformations.Same;

            this.SetPropertyChanged(
                new[] { "Thickness", "Outline", "Transformation" }, 
                () => OnPropertyChanged("Points"));
            this.SetPropertyChanged(new[] { "Width", "Height" }, SizePropertyChanged);
        }

        private new Application Model
        {
            get { return (Application) base.Model; }
        }

        private void SizePropertyChanged()
        {
            if (Width > 0.001 && Height > 0.005)
            {
                OnPropertyChanged("Points");
            }
        }

        public double Thickness
        {
            get { return Model.Thickness; }
            set { SetField(() => Thickness, v => Model.Thickness = v, value); }
        }

        private OutlineViewModel _outline;
        public OutlineViewModel Outline
        {
            get { return _outline; }
            set { SetField(ref _outline, value); }
        }

        private readonly Lazy<OutlineViewModels> _lazyOutlines = 
            new Lazy<OutlineViewModels>(() => new OutlineViewModels());
        public OutlineViewModels Outlines
        {
            get { return _lazyOutlines.Value; }
        }

        public IEnumerable<Point> Points
        {
            get
            {
                var points = Outline.GetPoints(Layout.Width, Layout.Height, Thickness);
                return Transformation.GetPoints(points, Layout);
            }
        }

        private TransformationViewModel _transformation;
        public TransformationViewModel Transformation
        {
            get { return _transformation; }
            set { SetField(ref _transformation, value); }
        }

        private readonly Lazy<TransformationViewModels> _lazyTransformations = 
            new Lazy<TransformationViewModels>(() => new TransformationViewModels());
        public TransformationViewModels Transformations
        {
            get { return _lazyTransformations.Value; }
        }

    }
}
