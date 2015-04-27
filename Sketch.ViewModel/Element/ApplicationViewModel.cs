using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using PeletonSoft.Sketch.ViewModel.Container;
using PeletonSoft.Sketch.ViewModel.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Element.Outline.Primitive;
using PeletonSoft.Sketch.ViewModel.Element.Transformation.Primitive;
using PeletonSoft.Sketch.ViewModel.Interface;

namespace PeletonSoft.Sketch.ViewModel.Element
{
    public class ApplicationViewModel : AlignableElementViewModel
    {
        public ApplicationViewModel(IWorkspaceBit workspaceBit)
            : base(workspaceBit)
        {
            Width = Screen.Width*0.3;
            Height = Screen.Height*0.3;
            Thickness = 0.3*Layout.Height;

            Outline = Outlines.Band;
            Transformation = Transformations.Same;

            PropertyChanged += OnPropertyChanged;
        }
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            var properties = new[] {"Width", "Height",};

            if (!properties.Contains(args.PropertyName))
            {
                return;
            }

            if (Layout.Width > 0.001 && Layout.Height > 0.005)
            {
                OnPropertyChanged("Points");
            }
        }

        private double _thickness;
        public double Thickness
        {
            get
            {
                return _thickness;
            }
            set
            {
                if (value != _thickness)
                {
                    _thickness = value;
                    OnPropertyChanged("Thickness");
                    OnPropertyChanged("Points");
                }

            }
        }
        private OutlineViewModel _outline;
        public OutlineViewModel Outline
        {
            get
            {
                return _outline;
            }
            set
            {
                if (value != _outline)
                {
                    _outline = value;
                    OnPropertyChanged("Outline");
                    OnPropertyChanged("Points");
                }
            }
        }
        private OutlineViewModels _outlines;
        public OutlineViewModels Outlines
        {
            get
            {
                if (_outlines == null)
                {
                    _outlines = new OutlineViewModels();
                }
                return _outlines;

            }
        }

        public IEnumerable<Point> Points
        {
            get
            {
                double a = Layout.Width;
                double b = Layout.Height;
                double d = Thickness;
                
                return Transformation.GetPoints(Outline.GetPoints(a,b,d),Layout);
            }
        }

        private TransformationViewModel _transformation;
        public TransformationViewModel Transformation
        {
            get
            {
                return _transformation;
            }
            set
            {
                if (value != _transformation)
                {
                    _transformation = value;
                    OnPropertyChanged("Transformation");
                    OnPropertyChanged("Points");
                }
            }
        }

        private TransformationViewModels _transformations;
        public TransformationViewModels Transformations
        {
            get
            {
                if (_transformations == null)
                {
                    _transformations = new TransformationViewModels();
                }
                return _transformations;
            }
        }

    }
}
