using System.ComponentModel;
using System.Windows;
using PeletonSoft.Sketch.ViewModel.Element;
using PeletonSoft.Sketch.ViewModel.Visual.Element.Custom;

namespace PeletonSoft.Sketch.ViewModel.Visual.Element
{
    public class ScanVisualViewModel : ElementVisualViewModel
    {
        public ScanVisualViewModel(VisualOptions visualOptions, ScanViewModel element) 
            : base(visualOptions, element)
        {
            Element.PropertyChanged += ElementOnPropertyChanged;
        }

        private void ElementOnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            switch (args.PropertyName)
            {
                case "RectangleSize" :
                    OnPropertyChanged("Clip");
                    OnPropertyChanged("ScaleX");
                    OnPropertyChanged("ScaleY");
                    break;
                case "FileName":
                    OnPropertyChanged("FileName");
                    break;
                case "ImageWidth":
                    OnPropertyChanged("ImageWidth");
                    break;
                case "ImageHeight":
                    OnPropertyChanged("ImageHeight");
                    break;
                case "Width":
                    OnPropertyChanged("ScaleX");
                    break;
                case "Height":
                    OnPropertyChanged("ScaleY");
                    break;
                case "RectangleAngle":
                    OnPropertyChanged("Angle");
                    break;
                case "RectangleCenter":
                    OnPropertyChanged("Center");
                    break;

            }
        }

        private new ScanViewModel Element
        {
            get
            {
                return (ScanViewModel)base.Element;
            }
        }

        public Rect Clip
        {
            get
            {
                var size = Element.RectangleSize;
                return new Rect(0, 0, size.X, size.Y);
            }
        }

        public double ScaleX
        {
            get
            {
                var pixelPerUnit = VisualOptions.PixelPerUnit;
                var size = Element.RectangleSize;
                return size.X > 0 ? pixelPerUnit.Value * Element.Width / size.X : 0;
            }
        }

        public double ScaleY
        {
            get
            {
                var pixelPerUnit = VisualOptions.PixelPerUnit;
                var size = Element.RectangleSize;
                return size.Y > 0 ? pixelPerUnit.Value * Element.Height / size.Y : 0;
            }
        }

        public double Angle
        {
            get
            {
                return Element.RectangleAngle;
            }
        }

        public Point Center
        {
            get
            {
                return Element.RectangleCenter;
            }
        }

        public double ImageWidth
        {
            get
            {
                return Element.ImageWidth;
            }
        }

        public double ImageHeight
        {
            get
            {
                return Element.ImageHeight;
            }
        }

        public string FileName
        {
            get
            {
                return Element.FileName;
            }
        }
    }
}
