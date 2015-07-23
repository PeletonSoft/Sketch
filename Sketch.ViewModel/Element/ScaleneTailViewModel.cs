using PeletonSoft.Sketch.Model.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Interface;

namespace PeletonSoft.Sketch.ViewModel.Element
{
    public class ScaleneTailViewModel : ScaleneSwagTailViewModel
    {
        public ScaleneTailViewModel(IWorkspaceBit workspaceBit, SwagTail model)
            : base(workspaceBit, model)
        {
        }
    }
}
