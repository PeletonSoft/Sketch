using System;
using System.Collections.Generic;
using PeletonSoft.Sketch.Model.Element.Transformation.Reflection;
using PeletonSoft.Sketch.ViewModel.Geometry;
using PeletonSoft.Sketch.ViewModel.Interface.Geometry;
using PeletonSoft.Tools.Model.Collection;

namespace PeletonSoft.Sketch.ViewModel.Container
{
    public class ReflectionViewModels : IContainer<IReflectionViewModel>
    {
        private enum Types
        {
            Same,
            VFlip,
            HFlip,
            DFlip
        };

        private readonly Lazy<IEnumerable<IContainerRecord<IReflectionViewModel>>> _lazyItems;
        public IEnumerable<IContainerRecord<IReflectionViewModel>> Items => _lazyItems.Value;
        public IReflectionViewModel Default => this.GetValueByKey(Types.Same);

        public ReflectionViewModels()
        {
            _lazyItems = new Lazy<IEnumerable<IContainerRecord<IReflectionViewModel>>>(
                () => new[]
                {
                    new ContainerRecord<IReflectionViewModel>(Types.Same, new ReflectionViewModel(new SameReflection())),
                    new ContainerRecord<IReflectionViewModel>(Types.HFlip, new ReflectionViewModel(new HFlipReflection())),
                    new ContainerRecord<IReflectionViewModel>(Types.VFlip, new ReflectionViewModel(new VFlipReflection())),
                    new ContainerRecord<IReflectionViewModel>(Types.DFlip, new ReflectionViewModel(new DFlipReflection()))
                });
        }
    }
}
