using System;
using System.Collections.Generic;
using PeletonSoft.Sketch.Model.Element.Outline;
using PeletonSoft.Sketch.ViewModel.Element.Primitive;
using PeletonSoft.Tools.Model;

namespace PeletonSoft.Sketch.ViewModel.Container 
{
    public class OutlineViewModels : IContainer<OutlineViewModel>
    {

        private readonly Lazy<OutlineViewModel> _lazyBand = 
            new Lazy<OutlineViewModel>(() => new OutlineViewModel(new BandOutline()));
        private readonly Lazy<OutlineViewModel> _lazyHexagon =
            new Lazy<OutlineViewModel>(() => new OutlineViewModel(new HexagonOutline()));
        private readonly Lazy<OutlineViewModel> _lazyHRectangle =
            new Lazy<OutlineViewModel>(() => new OutlineViewModel(new HRectangleOutline()));
        private readonly Lazy<OutlineViewModel> _lazyVRectangle =
            new Lazy<OutlineViewModel>(() => new OutlineViewModel(new VRectangleOutline()));
        private readonly Lazy<OutlineViewModel> _lazyTrapezium =
            new Lazy<OutlineViewModel>(() => new OutlineViewModel(new TrapeziumOutline()));
        private readonly Lazy<OutlineViewModel> _lazyTriangle =
            new Lazy<OutlineViewModel>(() => new OutlineViewModel(new TriangleOutline()));
        private readonly Lazy<OutlineViewModel> _lazyParallelogram =
            new Lazy<OutlineViewModel>(() => new OutlineViewModel(new ParallelogramOutline()));

        public OutlineViewModel Band
        {
            get { return _lazyBand.Value; }
        }

        public OutlineViewModel Hexagon
        {
            get { return _lazyHexagon.Value; }
        }

        public OutlineViewModel HRectangle
        {
            get { return _lazyHRectangle.Value; }
        }

        public OutlineViewModel VRectangle
        {
            get { return _lazyVRectangle.Value; }
        }

        public OutlineViewModel Trapezium
        {
            get { return _lazyTrapezium.Value; }
        }

        public OutlineViewModel Triangle
        {
            get { return _lazyTriangle.Value; }
        }

        public OutlineViewModel Parallelogram
        {
            get { return _lazyParallelogram.Value; }
        }

        public IEnumerable<OutlineViewModel> Items
        {
            get
            {
                return new[]
                {
                    Band, Parallelogram, Trapezium,
                    Triangle, VRectangle, HRectangle, Hexagon
                };
            }
        }
    }
}
