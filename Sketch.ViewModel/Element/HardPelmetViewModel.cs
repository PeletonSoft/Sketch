using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using PeletonSoft.Sketch.ViewModel.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Element.Primitive;
using PeletonSoft.Sketch.ViewModel.Interface;

namespace PeletonSoft.Sketch.ViewModel.Element
{
    public class HardPelmetViewModel : AlignableElementViewModel
    {
        public DecorativeBorderViewModel DecorativeBorder { get; set; }
        public IEnumerable<Point> Points
        {
            get
            {
                Func<Point, Point> transformer =
                    point =>
                        new Point
                        {
                            X = point.X,
                            Y = point.Y + Height - DecorativeBorder.Height
                        };
                var decorative = DecorativeBorder.Points.Select(transformer);

                var points = new List<Point>
                {
                    new Point(0, 0),
                    new Point(Width, 0)
                };

                return points.Concat(decorative.Reverse());
            }
        }


        public HardPelmetViewModel(IWorkspaceBit workspaceBit)
            : base(workspaceBit)
        {
            DecorativeBorder = new DecorativeBorderViewModel(workspaceBit);

            PropertyChanged +=
                (sender, args) =>
                {
                    switch (args.PropertyName)
                    {
                        case "Width":
                            OnPropertyChanged("Points");
                            DecorativeBorder.Width = Width;
                            break;
                        case "Height":
                            OnPropertyChanged("Points");
                            break;
                    }
                };

            DecorativeBorder.PropertyChanged +=
                (sender, args) =>
                {
                    switch (args.PropertyName)
                    {
                        case "Width":
                            Width = DecorativeBorder.Width;
                            break;
                        case "Points":
                            OnPropertyChanged("Points");
                            break;
                    }
                };

            Width = Screen.Width;
            Height = 0.5 * Screen.Height;

            DecorativeBorder.Width = Width;
            DecorativeBorder.Height = 0.3 * Height;
            DecorativeBorder.ResetChains();
        }
    }
}
