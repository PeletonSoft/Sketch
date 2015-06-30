using System;
using System.Collections.Generic;
using PeletonSoft.Sketch.Model.Element.Transformation;
using PeletonSoft.Sketch.ViewModel.Element.Primitive;
using PeletonSoft.Tools.Model;

namespace PeletonSoft.Sketch.ViewModel.Container
{
    public class TransformationViewModels : IContainer<TransformationViewModel>
    {
        private readonly Lazy<TransformationViewModel> _lazySame = 
            new Lazy<TransformationViewModel>(() => new TransformationViewModel(new SameTransformation())) ;
        private readonly Lazy<TransformationViewModel> _lazyHFlip =
            new Lazy<TransformationViewModel>(() => new TransformationViewModel(new HFlipTransformation()));
        private readonly Lazy<TransformationViewModel> _lazyVFlip =
            new Lazy<TransformationViewModel>(() => new TransformationViewModel(new VFlipTransformation()));
        private readonly Lazy<TransformationViewModel> _lazyDFlip =
            new Lazy<TransformationViewModel>(() => new TransformationViewModel(new DFlipTransformation()));

        public TransformationViewModel Same
        {
            get { return _lazySame.Value; }
        }
        public TransformationViewModel HFlip
        {
            get { return _lazyHFlip.Value; }
        }
        public TransformationViewModel VFlip
        {
            get { return _lazyVFlip.Value; }
        }
        public TransformationViewModel DFlip
        {
            get { return _lazyDFlip.Value; }
        }

        public IEnumerable<TransformationViewModel> Items
        {
            get { return new[] {Same, VFlip, HFlip, DFlip}; }
        }
    }
}
