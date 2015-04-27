using System.Collections.Generic;
using System.ComponentModel;
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
            try
            {
                switch (args.PropertyName)
                {
                    case "TopLeft":
                        TopRight.Point = TopRight.SaveOrthogonal(BottomRight, TopLeft);
                        BottomLeft.Point = BottomLeft.SaveOrthogonal(BottomRight, TopLeft);
                        OnPropertyChanged("Point");
                        break;
                    case "TopRight":
                        BottomRight.Point = BottomRight.SaveDiagonal(TopLeft, TopRight);
                        BottomLeft.Point = TopRight.FindSymmetry(TopLeft, BottomRight);
                        OnPropertyChanged("Point");
                        break;
                    case "BottomLeft":
                        TopLeft.Point = TopLeft.SaveDiagonal(BottomRight, BottomLeft);
                        TopRight.Point = BottomLeft.FindSymmetry(BottomRight, TopLeft);
                        OnPropertyChanged("Point");
                        break;
                    case "BottomRight":
                        TopRight.Point = TopRight.SaveOrthogonal(TopLeft, BottomRight);
                        BottomLeft.Point = BottomLeft.SaveOrthogonal(TopLeft, BottomRight);
                        OnPropertyChanged("Point");
                        break;
                }
            }
            finally
            {
                _lock = false;
            }
        }
    }
}
