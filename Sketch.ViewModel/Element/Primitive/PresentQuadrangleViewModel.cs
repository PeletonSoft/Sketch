using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;
using PeletonSoft.Sketch.ViewModel.Geometry;
using PeletonSoft.Tools.Model.NotifyChanged;

namespace PeletonSoft.Sketch.ViewModel.Element.Primitive
{
    public class PresentQuadrangleViewModel : RectangleViewModel
    {
        private void OnPropertyChanged<T>(Expression<Func<PresentQuadrangleViewModel, T>> expression)
        {
            expression.OnPropertyChanged(OnPropertyChanged);
        }

        #region implement IOriginator
        public override void RestoreDefault()
        {
            Default();
        }
        #endregion

        private void Default()
        {
            TopLeft.Point = new Point(0, 0);
            BottomRight.Point = new Point(Size, Size);
            BottomLeft.Point = new Point(Size, 0);
            TopRight.Point = new Point(0, Size);
        }

        public double Size { get; private set; }

        public PresentQuadrangleViewModel(double size, SuperimposeOptionViewModel superimposeOption)
        {
            Size = size;
            SuperimposeOption = superimposeOption;
            Default();

            this.SetPropertyChanged(
                new Expression<Func<PresentQuadrangleViewModel,VertexViewModel>>[]
                {
                    q => q.TopLeft,q => q.TopRight, 
                    q => q.BottomLeft, q => q.BottomRight
                },
                () => OnPropertyChanged(q => q.Points));
        }

        public IEnumerable<Point> Points
        {
            get { return Vertices.Select(v => new Point(v.X/Size, 1 - v.Y/Size)); }
        }

        public SuperimposeOptionViewModel SuperimposeOption { get; private set; }

    }
}
