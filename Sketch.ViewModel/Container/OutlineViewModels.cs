﻿using System;
using System.Collections.Generic;
using PeletonSoft.Sketch.Model.Element.Outline;
using PeletonSoft.Sketch.ViewModel.Element.Primitive;
using PeletonSoft.Tools.Model.Collection;
using PeletonSoft.Tools.Model.Memento.Container;

namespace PeletonSoft.Sketch.ViewModel.Container
{
    public class OutlineViewModels : IContainer<OutlineViewModel>
    {
        private enum Types
        {
            Band,
            Parallelogram,
            Trapezium,
            Triangle,
            VRectangle,
            HRectangle,
            Hexagon
        };

        private readonly Lazy<IEnumerable<IContainerRecord<OutlineViewModel>>> _lazyItems;
        public IEnumerable<IContainerRecord<OutlineViewModel>> Items => _lazyItems.Value;
        public OutlineViewModel Default => this.GetValueByKey(Types.HRectangle);

        public OutlineViewModels()
        {
            _lazyItems = new Lazy<IEnumerable<IContainerRecord<OutlineViewModel>>>(
                () => new[]
                {
                    new ContainerRecord<OutlineViewModel>(Types.Band, new OutlineViewModel(new BandOutline())),
                    new ContainerRecord<OutlineViewModel>(Types.Parallelogram, new OutlineViewModel(new ParallelogramOutline())),
                    new ContainerRecord<OutlineViewModel>(Types.Trapezium, new OutlineViewModel(new TrapeziumOutline())),
                    new ContainerRecord<OutlineViewModel>(Types.Triangle, new OutlineViewModel(new TriangleOutline())),
                    new ContainerRecord<OutlineViewModel>(Types.VRectangle, new OutlineViewModel(new VRectangleOutline())),
                    new ContainerRecord<OutlineViewModel>(Types.HRectangle, new OutlineViewModel(new HRectangleOutline())),
                    new ContainerRecord<OutlineViewModel>(Types.Hexagon, new OutlineViewModel(new HexagonOutline()))
                });
        }
    }
}
