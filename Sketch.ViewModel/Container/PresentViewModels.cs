using System.Collections.Generic;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Sketch.ViewModel.Present;
using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewModel.Container
{
    public class PresentViewModels : IContainerOriginator<IPresentViewModel>
    {

        public IEnumerable<IPresentViewModel> Items
        {
            get { return new[] {PreviewPresent, LayoutPresent}; }
        }

        public PresentViewModels(WorkspaceViewModel workspace)
        {
            LayoutPresent = new LayoutPresentViewModel(workspace);
            PreviewPresent = new PreviewPresentViewModel(workspace);
        }

        public IPresentViewModel PreviewPresent { get; set; }

        public IPresentViewModel LayoutPresent { get; set; }

        public void RestoreDefault()
        {
            
        }
    }
}
