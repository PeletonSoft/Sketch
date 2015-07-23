using System;
using System.Collections.Generic;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Sketch.ViewModel.Present;
using PeletonSoft.Tools.Model.Collection;
using PeletonSoft.Tools.Model.Memento.Container;

namespace PeletonSoft.Sketch.ViewModel.Container
{
    public class PresentViewModels : IContainerOriginator<IPresentViewModel>
    {
        private enum Types
        {
            Preview,
            Layout
        };
        private readonly Lazy<IEnumerable<IContainerRecord<IPresentViewModel>>> _lazyItems;
        public IEnumerable<IContainerRecord<IPresentViewModel>> Items
        {
            get
            {
                return _lazyItems.Value;
            }
        }

        public IPresentViewModel Default
        {
            get { return this.GetValueByKey(Types.Layout); }
        }

        public PresentViewModels(WorkspaceViewModel workspace)
        {
            LayoutPresent = new LayoutPresentViewModel(workspace);
            PreviewPresent = new PreviewPresentViewModel(workspace);

            _lazyItems = new Lazy<IEnumerable<IContainerRecord<IPresentViewModel>>>(
                () => new[]
                {
                    new ContainerRecord<IPresentViewModel>(Types.Preview,
                        typeof (PreviewPresentViewModel), PreviewPresent),
                    new ContainerRecord<IPresentViewModel>(Types.Layout,
                        typeof (LayoutPresentViewModel), LayoutPresent)
                });
        }

        public IPresentViewModel PreviewPresent { get; private set; }

        public IPresentViewModel LayoutPresent { get; private set; }

        public void RestoreDefault()
        {
        }
    }
}
