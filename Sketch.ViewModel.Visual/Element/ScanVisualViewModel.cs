using System;
using System.ComponentModel;
using System.Windows;
using PeletonSoft.Sketch.ViewModel.Element;
using PeletonSoft.Sketch.ViewModel.Visual.Element.Custom;
using PeletonSoft.Tools.Model.File;

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
                case "Rectangle" :
                    OnPropertyChanged("Clip");
                    OnPropertyChanged("ScaleX");
                    OnPropertyChanged("ScaleY");
                    OnPropertyChanged("Angle");
                    OnPropertyChanged("Center");
                    OnPropertyChanged("Shift");
                    break;
                case "FileName":
                    OnPropertyChanged("FileName");
                    break;
                case "ImageBox":
                    OnPropertyChanged("ImageBox");
                    break;
                case "Width":
                    OnPropertyChanged("ScaleX");
                    break;
                case "Height":
                    OnPropertyChanged("ScaleY");
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
                var size = Element.Rectangle.Size;
                return new Rect(0, 0, size.Width, size.Height);
            }
        }

        public double ScaleX
        {
            get
            {
                var size = Element.Rectangle.Size;
                return size.Width > 0 ? Layout.Width / size.Width : 0;
            }
        }

        public double ScaleY
        {
            get
            {
                var size = Element.Rectangle.Size;
                return size.Height > 0 ? Layout.Height / size.Height : 0;
            }
        }

        public double Angle
        {
            get { return -Element.Rectangle.Angle*180/Math.PI; }
        }

        public Point Center
        {
            get { return Element.Rectangle.Center; }
        }

        public Point Shift
        {
            get { return new Point(-Center.X, -Center.Y); }
        }

        public ImageBox ImageBox
        {
            get { return Element.ImageBox; }
        }
    }
}
