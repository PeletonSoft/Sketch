using System.Collections.Generic;
using PeletonSoft.Sketch.ViewModel.Element.Transformation;
using PeletonSoft.Sketch.ViewModel.Element.Transformation.Primitive;
using PeletonSoft.Tools.Model;

namespace PeletonSoft.Sketch.ViewModel.Container
{
    public class TransformationViewModels : IContainer<TransformationViewModel>
    {
        private TransformationViewModel _same;
        public TransformationViewModel Same
        {
            get
            {
                if (_same == null)
                {
                    _same = new SameTransformationViewModel();
                }
                return _same;
            }
        }

        private TransformationViewModel _hFlip;
        public TransformationViewModel HFlip
        {
            get
            {
                if (_hFlip == null)
                {
                    _hFlip = new HFlipTransformationViewModel();
                }
                return _hFlip;
            }
        }

        private TransformationViewModel _vFlip;
        public TransformationViewModel VFlip
        {
            get
            {
                if (_vFlip == null)
                {
                    _vFlip = new VFlipTransformationViewModel();
                }
                return _vFlip;
            }
        }

        private TransformationViewModel _dFlip;
        public TransformationViewModel DFlip
        {
            get
            {
                if (_dFlip == null)
                {
                    _dFlip = new DFlipTransformationViewModel();
                }
                return _dFlip;
            }
        }

        public IEnumerable<TransformationViewModel> Items
        {
            get
            {
                return new[]
                {
                    Same,
                    VFlip,
                    HFlip,
                    DFlip
                };
            } 
        }
    }
}
