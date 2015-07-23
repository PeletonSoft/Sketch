using System;
using System.Linq.Expressions;
using System.Windows;
using PeletonSoft.Sketch.ViewModel.Element;
using PeletonSoft.Sketch.ViewModel.Visual.Element.Custom;
using PeletonSoft.Tools.Model.File;
using PeletonSoft.Tools.Model.NotifyChanged;

namespace PeletonSoft.Sketch.ViewModel.Visual.Element
{
    public class ScanVisualViewModel : ElementVisualViewModel
    {
        private void OnPropertyChanged<T>(Expression<Func<ScanVisualViewModel, T>> expression)
        {
            expression.OnPropertyChanged(OnPropertyChanged);
        }

        public ScanVisualViewModel(VisualOptions visualOptions, ScanViewModel element)
            : base(visualOptions, element)
        {
            Element
                .SetPropertyChanged(el => el.Transformation,
                    () =>
                    {
                        OnPropertyChanged(v => v.RotationAngle);
                        OnPropertyChanged(v => v.RotationScale);
                    })
                .SetPropertyChanged(el => el.ImageBox, () => OnPropertyChanged(v => ImageBox))
                .SetPropertyChanged(
                    new[]
                    {
                        element.GetPropertyName(el => el.Width),
                        element.GetPropertyName(el => el.Height),
                        element.GetPropertyName(el => el.Rectangle)
                    },
                    () =>
                    {
                        OnPropertyChanged(v => v.Clip);
                        OnPropertyChanged(v => v.Scale);
                        OnPropertyChanged(v => v.Angle);
                        OnPropertyChanged(v => v.Center);
                        OnPropertyChanged(v => v.Shift);
                        OnPropertyChanged(v => v.RotationScale);
                    });
        }

        private new ScanViewModel Element
        {
            get { return (ScanViewModel) base.Element; }
        }

        public Rect Clip
        {
            get { return new Rect(new Point(0, 0), Element.Rectangle.Size); }
        }

        public Point Scale
        {
            get
            {
                var size = Element.Rectangle.Size;
                return new Point(
                    size.Width > 0 ? Layout.Width/size.Width : 0,
                    size.Height > 0 ? Layout.Height/size.Height : 0);
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

        public double RotationAngle
        {
            get { return -Element.Transformation.Rotation.Angle*180/Math.PI; }
        }

        public Point RotationScale
        {
            get
            {
                var size = new Size(Layout.Width, Layout.Height);
                var rsize = Element.Transformation.Rotation.Rotate(size);
                var r = Element.Transformation.Reflection;
                return new Point(
                    rsize.Width/Layout.Width*r.Scale.X,
                    rsize.Height/Layout.Height*r.Scale.Y);
            }
        }

    }
}
