using System.Collections.Generic;
using PeletonSoft.Sketch.ViewModel.Element.Outline;
using PeletonSoft.Sketch.ViewModel.Element.Outline.Primitive;
using PeletonSoft.Tools.Model;

namespace PeletonSoft.Sketch.ViewModel.Container 
{
    public class OutlineViewModels : IContainer<OutlineViewModel>
    {

        private OutlineViewModel _band;
        public OutlineViewModel Band
        {
            get
            {
                if (_band == null)
                {
                    _band = new BandOutlineViewModel();
                }
                return _band;
            }
        }

        private OutlineViewModel _hexagon;
        public OutlineViewModel Hexagon
        {
            get
            {
                if (_hexagon == null)
                {
                    _hexagon = new HexagonOutlineViewModel();
                }
                return _hexagon;
            }
        }

        private OutlineViewModel _hRectangle;
        public OutlineViewModel HRectangle
        {
            get
            {
                if (_hRectangle == null)
                {
                    _hRectangle = new HRectangleOutlineViewModel();
                }
                return _hRectangle;
            }
        }

        private OutlineViewModel _vRectangle;
        public OutlineViewModel VRectangle
        {
            get
            {
                if (_vRectangle == null)
                {
                    _vRectangle = new VRectangleOutlineViewModel();
                }
                return _vRectangle;
            }
        }
        private OutlineViewModel _trapezium;
        public OutlineViewModel Trapezium
        {
            get
            {
                if (_trapezium == null)
                {
                    _trapezium = new TrapeziumOutlineViewModel();
                }
                return _trapezium;
            }
        }
        private OutlineViewModel _triangle;
        public OutlineViewModel Triangle
        {
            get
            {
                if (_triangle == null)
                {
                    _triangle = new TriangleOutlineViewModel();
                }
                return _triangle;
            }
        }
        private OutlineViewModel _parallelogram;
        public OutlineViewModel Parallelogram
        {
            get
            {
                if (_parallelogram == null)
                {
                    _parallelogram = new ParallelogramOutlineViewModel();
                }
                return _parallelogram;
            }
        }

        public IEnumerable<OutlineViewModel> Items
        {
            get
            {
                return new[]
                {
                    Band,
                    Parallelogram,
                    Trapezium,
                    Triangle,
                    VRectangle,
                    HRectangle,
                    Hexagon
                };
            } 
        }
    }
}
