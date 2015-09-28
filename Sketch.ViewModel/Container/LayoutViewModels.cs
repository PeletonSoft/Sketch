using System;
using System.Collections.Generic;
using PeletonSoft.Sketch.ViewModel.Element.Layout;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Sketch.ViewModel.Interface.Element;
using PeletonSoft.Sketch.ViewModel.Interface.Layout;
using PeletonSoft.Tools.Model.Collection;
using PeletonSoft.Tools.Model.Memento.Container;

namespace PeletonSoft.Sketch.ViewModel.Container
{
    public class LayoutViewModels : IContainerOriginator<ILayoutViewModel>
    {
        public void RestoreDefault()
        {
        }

        private readonly Lazy<IEnumerable<IContainerRecord<ILayoutViewModel>>> _lazyItems;
        public IEnumerable<IContainerRecord<ILayoutViewModel>> Items => _lazyItems.Value;
        public ILayoutViewModel Default => LeftLayout;

        public LayoutViewModels(IWorkspaceBit workspaceBit, IAlignableElementViewModel element)
        {
            LeftLayout = new LeftLayoutViewModel(workspaceBit,element);
            RightLayout = new RightLayoutViewModel(workspaceBit, element);

            _lazyItems = new Lazy<IEnumerable<IContainerRecord<ILayoutViewModel>>>(
                () => new[]
                {
                    new ContainerRecord<ILayoutViewModel>("Left",
                        typeof (LeftLayoutViewModel), LeftLayout),
                    new ContainerRecord<ILayoutViewModel>("Right",
                        typeof (RightLayoutViewModel), RightLayout),
                });
        }

        public ILayoutViewModel RightLayout { get; }
        public ILayoutViewModel LeftLayout { get; }

        protected WorkspaceBit WorkspaceBit { get; set; }
        protected IAlignableElementViewModel Element { get; set; }
    }
}
