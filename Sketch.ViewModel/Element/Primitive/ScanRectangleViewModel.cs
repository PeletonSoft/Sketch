using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using PeletonSoft.Sketch.ViewModel.Geometry;
using PeletonSoft.Tools.Model.NotifyChanged;
using PeletonSoft.Tools.Model.SketchMath;

namespace PeletonSoft.Sketch.ViewModel.Element.Primitive
{
    public class ScanRectangleViewModel : RectangleViewModel
    {
        private void Default(double width, double height)
        {
            TopLeft = new VertexViewModel(0, 0);
            TopRight = new VertexViewModel(width, 0);
            BottomLeft = new VertexViewModel(0, height);
            BottomRight = new VertexViewModel(width, height);
        }

        public ScanRectangleViewModel(double width, double height)
        {

            Default(width, height);

            new Dictionary<INotifyPropertyChanged, string>
            {
                {TopLeft, "TopLeft"},
                {TopRight, "TopRight"},
                {BottomLeft, "BottomLeft"},
                {BottomRight, "BottomRight"}
            }.SetPropertyChanged(
                new[] {"X", "Y"},
                OnPropertyChanged);

            this.SetPropertyChanged(
                new[] {"TopLeft", "TopRight", "BottomLeft", "BottomRight"},
                () => OnPropertyChanged("Vertex"));

            PropertyChanged += VertexChanged;

        }

        private bool _lock;
        private void VertexChanged(object sender, PropertyChangedEventArgs args)
        {
            if (_lock)
            {
                return;
            }

            _lock = true;
            if (_lock)
            {
                try
                {
                    switch (args.PropertyName)
                    {
                        case "TopLeft":
                            TopRight.Point = TopRight.SaveOrthogonal(BottomRight, TopLeft);
                            BottomLeft.Point = BottomLeft.SaveOrthogonal(BottomRight, TopLeft);
                            OnPropertyChanged("Points");
                            break;
                        case "TopRight":
                            BottomRight.Point = BottomRight.SaveDiagonal(TopLeft, TopRight);
                            BottomLeft.Point = TopRight.FindSymmetry(TopLeft, BottomRight);
                            OnPropertyChanged("Points");
                            break;
                        case "BottomLeft":
                            TopLeft.Point = TopLeft.SaveDiagonal(BottomRight, BottomLeft);
                            TopRight.Point = BottomLeft.FindSymmetry(BottomRight, TopLeft);
                            OnPropertyChanged("Points");
                            break;
                        case "BottomRight":
                            TopRight.Point = TopRight.SaveOrthogonal(TopLeft, BottomRight);
                            BottomLeft.Point = BottomLeft.SaveOrthogonal(TopLeft, BottomRight);
                            OnPropertyChanged("Points");
                            break;
                    }
                }
                finally
                {
                    _lock = false;
                }
            }
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
