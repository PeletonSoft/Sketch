using System;
using System.Windows;
using PeletonSoft.Sketch.ViewModel.Element;
using PeletonSoft.Sketch.ViewModel.Interface.Visual;
using PeletonSoft.Sketch.ViewModel.Visual.Element.Custom;
using PeletonSoft.Tools.Model.File;
using PeletonSoft.Tools.Model.ObjectEvent.NotifyChanged;

namespace PeletonSoft.Sketch.ViewModel.Visual.Element
{
    public sealed class ScanVisualViewModel : ElementVisualViewModel, IElementVisualViewModel<ScanViewModel>
    {
        public ScanVisualViewModel(VisualOptions visualOptions, ScanViewModel element)
            : base(visualOptions, element)
        {
            Element
                .SetPropertyChanged(nameof(Element.Transformation),
                    () =>
                    {
                        OnPropertyChanged(nameof(RotationAngle));
                        OnPropertyChanged(nameof(RotationScale));
                    })
                .SetPropertyChanged(nameof(Element.ImageBox), () => OnPropertyChanged(nameof(ImageBox)))
                .SetPropertyChanged(
                    new[]
                    {
                        nameof(Element.Width), nameof(Element.Height), nameof(Element.Rectangle)
                    },
                    () =>
                    {
                        OnPropertyChanged(nameof(Clip));
                        OnPropertyChanged(nameof(Scale));
                        OnPropertyChanged(nameof(Angle));
                        OnPropertyChanged(nameof(Center));
                        OnPropertyChanged(nameof(Shift));
                        OnPropertyChanged(nameof(RotationScale));
                    });
        }

        public new ScanViewModel Element => (ScanViewModel) base.Element;
        public Rect Clip => new Rect(new Point(0, 0), Element.Rectangle.Size);

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

        public double Angle => -Element.Rectangle.Angle*180/Math.PI;
        public Point Center => Element.Rectangle.Center;
        public Point Shift => new Point(-Center.X, -Center.Y);
        public ImageBox ImageBox => Element.ImageBox;
        public double RotationAngle => -Element.Transformation.Rotation.Angle*180/Math.PI;
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
