using System.Collections.Generic;
using System.Linq;
using System.Windows;
using PeletonSoft.Sketch.ViewModel.Geometry;
using PeletonSoft.Tools.Model.NotifyChanged;

namespace PeletonSoft.Sketch.ViewModel.Element.Primitive
{
    public class PresentQuadrangleViewModel : RectangleViewModel
    {
        #region implement IOriginator

        public override void RestoreDefault() => Default();

        #endregion

        private void Default()
        {
            TopLeft.Point = new Point(0, 0);
            BottomRight.Point = new Point(Size, Size);
            BottomLeft.Point = new Point(Size, 0);
            TopRight.Point = new Point(0, Size);
        }

        public double Size { get; }

        public PresentQuadrangleViewModel(double size, SuperimposeOptionViewModel superimposeOption)
        {
            Size = size;
            SuperimposeOption = superimposeOption;
            Default();

            this.SetPropertyChanged(
                new[]
                {
                    nameof(TopLeft), nameof(TopRight),
                    nameof(BottomLeft), nameof(BottomRight)
                },
                () => OnPropertyChanged(nameof(Points)));
        }

        public IEnumerable<Point> Points
        {
            get { return Vertices.Select(v => new Point(v.X/Size, 1 - v.Y/Size)); }
        }

        public SuperimposeOptionViewModel SuperimposeOption { get; private set; }

    }
}
