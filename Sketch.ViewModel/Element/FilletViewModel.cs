using PeletonSoft.Sketch.Model.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Interface;

namespace PeletonSoft.Sketch.ViewModel.Element
{
    public class FilletViewModel : AlignableElementViewModel
    {

        public FilletViewModel(IWorkspaceBit workspaceBit)
            : base(workspaceBit, new AlignableElement())
        {
            Height = 0.05;
        }
    }
}
