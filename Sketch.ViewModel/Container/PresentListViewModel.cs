using System;
using System.Collections.Generic;
using PeletonSoft.Sketch.ViewModel.DataTransfer.Interface;
using PeletonSoft.Sketch.ViewModel.DataTransfer.Present;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Sketch.ViewModel.Interface.Container;
using PeletonSoft.Sketch.ViewModel.Present;
using PeletonSoft.Tools.Model.Collection;
using PeletonSoft.Tools.Model.Memento;
using PeletonSoft.Tools.Model.Memento.Container;

namespace PeletonSoft.Sketch.ViewModel.Container
{
    public class PresentListViewModel : IPresentListViewModel
    {
        private enum Types
        {
            Preview,
            Layout
        };

        private readonly Lazy<IEnumerable<IContainerRecord<IPresentViewModel>>> _lazyItems;

        public IEnumerable<IContainerRecord<IPresentViewModel>> Items => _lazyItems.Value;
        public IPresentViewModel Default => LayoutPresent;

        IOriginator<IPresentDataTransfer> IContainer<IOriginator<IPresentDataTransfer>>.Default => Default;

        IEnumerable<IContainerRecord<IOriginator<IPresentDataTransfer>>> IContainer<IOriginator<IPresentDataTransfer>>.
            Items => Items;



        public PresentListViewModel(WorkspaceViewModel workspace)
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

        public IPresentViewModel PreviewPresent { get; }

        public IPresentViewModel LayoutPresent { get; }

        public IListDataTransfer<IPresentDataTransfer> CreateState() =>
            new ListDataTransfer<IPresentDataTransfer>();

        public void Save(IListDataTransfer<IPresentDataTransfer> state)
        {
            this.PlainSave(state);
        }

        public void Restore(IListDataTransfer<IPresentDataTransfer> state)
        {
            this.ZipRestore(state);
        }
    }
}
