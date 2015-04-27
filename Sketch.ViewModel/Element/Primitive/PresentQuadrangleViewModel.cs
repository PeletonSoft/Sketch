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
        public override void RestoreDefault()
        {
            Default();
        }
        #endregion

        private void Default()
        {
            TopLeft = new VertexViewModel(0, 0);
            TopRight = new VertexViewModel(0, Size);
            BottomLeft = new VertexViewModel(Size, 0);
            BottomRight = new VertexViewModel(Size, Size);
        }

        public double Size { get; private set; }

        public PresentQuadrangleViewModel(double size)
        {
            Size = size;
            Default();

            new[] {TopLeft, TopRight, BottomLeft, BottomRight}
                .SetPropertyChanged(
                    new[] {"X", "Y"},
                    VisualChanged);

            this.SetPropertyChanged(
                new[] {"TopLeft", "TopRight", "BottomLeft", "BottomRight"},
                VisualChanged);
        }

        private void VisualChanged()
        {
            OnPropertyChanged("Points");
        }

        public override IEnumerable<Point> Points
        {
            get
            {
                return new[] {TopLeft, TopRight, BottomLeft, BottomRight}
                    .Select(v => new Point(v.X / Size, 1 - v.Y / Size));
            }
        }

    }
}
