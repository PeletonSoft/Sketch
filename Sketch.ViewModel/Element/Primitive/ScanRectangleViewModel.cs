using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;
using PeletonSoft.Sketch.ViewModel.Geometry;
using PeletonSoft.Tools.Model.NotifyChanged;
using PeletonSoft.Tools.Model.SketchMath;

namespace PeletonSoft.Sketch.ViewModel.Element.Primitive
{
    public class ScanRectangleViewModel : RectangleViewModel
    {
        private void OnPropertyChanged<T>(Expression<Func<ScanRectangleViewModel, T>> expression)
        {
            expression.OnPropertyChanged(OnPropertyChanged);
        }

        public void Default(double width, double height)
        {
            _lockFlag.LockAction(() =>
            {
                TopLeft.Point = new Point(0, 0);
                BottomRight.Point = new Point(width, height);
                TopRight.Point = new Point(width, 0);
                BottomLeft.Point = new Point(0, height);    
            });
        }

        private readonly LockFlag _lockFlag = new LockFlag();

        public ScanRectangleViewModel(double width, double height)
        {
            this
                .SetPropertyChanged(_lockFlag, r => r.TopLeft,
                    () =>
                    {
                        TopRight.Point = TopRight.SaveOrthogonal(BottomRight, TopLeft);
                        BottomLeft.Point = BottomLeft.SaveOrthogonal(BottomRight, TopLeft);
                        OnPropertyChanged(el => el.Points);
                    })
                .SetPropertyChanged(_lockFlag, r => TopRight,
                    () =>
                    {
                        BottomRight.Point = BottomRight.SaveDiagonal(TopLeft, TopRight);
                        BottomLeft.Point = TopRight.FindSymmetry(TopLeft, BottomRight);
                        OnPropertyChanged(el => el.Points);
                    })
                .SetPropertyChanged(_lockFlag, r => BottomLeft,
                    () =>
                    {
                        TopLeft.Point = TopLeft.SaveDiagonal(BottomRight, BottomLeft);
                        TopRight.Point = BottomLeft.FindSymmetry(BottomRight, TopLeft);
                        OnPropertyChanged(el => el.Points);
                    })
                .SetPropertyChanged(_lockFlag, r => r.BottomRight,
                    () =>
                    {
                        TopRight.Point = TopRight.SaveOrthogonal(TopLeft, BottomRight);
                        BottomLeft.Point = BottomLeft.SaveOrthogonal(TopLeft, BottomRight);
                        OnPropertyChanged(el => el.Points);
                    });

            Default(width, height);
        }

        public IEnumerable<Point> Points
        {
            get { return Vertices.Select(v => v.Point); }
        }


        public Point Center
        {
            get { return new Point(TopLeft.X, TopLeft.Y); }
        }

        public double Angle
        {
            get { return Math.Atan2(TopRight.Y - TopLeft.Y, TopRight.X - TopLeft.X); }
        }

        public Size Size
        {
            get
            {
                var a = Math.Pow(TopRight.X - TopLeft.X, 2) + Math.Pow(TopRight.Y - TopLeft.Y, 2);
                var b = Math.Pow(BottomRight.X - TopRight.X, 2) + Math.Pow(BottomRight.Y - TopRight.Y, 2);
                return new Size(Math.Sqrt(a), Math.Sqrt(b));
            }
        }
    }
}
