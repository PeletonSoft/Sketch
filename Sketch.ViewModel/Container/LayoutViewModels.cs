using System.Collections.Generic;
using PeletonSoft.Sketch.ViewModel.Element.Layout;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Sketch.ViewModel.Interface.Element;
using PeletonSoft.Sketch.ViewModel.Interface.Layout;
using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewModel.Container
{
    public class LayoutViewModels :IContainerOriginator<ILayoutViewModel>
    {
        public void RestoreDefault()
        {
        }

        public IEnumerable<ILayoutViewModel> Items {
            get
            {
                return new[]
                {
                    RightLayout,
                    LeftLayout
                };
            }
        }

        public LayoutViewModels(IWorkspaceBit workspaceBit, IAlignableElementViewModel element)
        {
            LeftLayout = new LeftLayoutViewModel(workspaceBit,element);
            RightLayout = new RightLayoutViewModel(workspaceBit, element);
        }

        public ILayoutViewModel RightLayout { get; private set; }

        public ILayoutViewModel LeftLayout { get; private set; }

        protected WorkspaceBit WorkspaceBit { get; set; }
        protected IAlignableElementViewModel Element { get; set; }
    }
}
