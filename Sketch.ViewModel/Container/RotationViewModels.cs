using System;
using System.Collections.Generic;
using PeletonSoft.Sketch.Model.Element.Transformation.Rotation;
using PeletonSoft.Sketch.ViewModel.Geometry;
using PeletonSoft.Tools.Model.Collection;
using PeletonSoft.Tools.Model.Memento.Container;

namespace PeletonSoft.Sketch.ViewModel.Container
{
    public class RotationViewModels : IContainer<RotationViewModel>
    {
        private enum Types
        {
            By0,
            By90,
            By180,
            By270
        }

        private readonly Lazy<IEnumerable<IContainerRecord<RotationViewModel>>> _lazyItems;
        public IEnumerable<IContainerRecord<RotationViewModel>> Items => _lazyItems.Value;
        public RotationViewModel Default => this.GetValueByKey(Types.By0);

        public RotationViewModels()
        {
            _lazyItems = new Lazy<IEnumerable<IContainerRecord<RotationViewModel>>>(
                () => new[]
                {
                    new ContainerRecord<RotationViewModel>(Types.By0,
                        typeof (RotationViewModel), new RotationViewModel(new RotationBy0())),
                    new ContainerRecord<RotationViewModel>(Types.By90,
                        typeof (RotationViewModel), new RotationViewModel(new RotationBy90())),
                    new ContainerRecord<RotationViewModel>(Types.By180,
                        typeof (RotationViewModel), new RotationViewModel(new RotationBy180())),
                    new ContainerRecord<RotationViewModel>(Types.By270,
                        typeof (RotationViewModel), new RotationViewModel(new RotationBy270()))
                });
        }
    }
}
