using PeletonSoft.Sketch.Model.Element;
using PeletonSoft.Sketch.ViewModel.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Tools.Model.Logic;

namespace PeletonSoft.Sketch.ViewModel.Element
{
    public sealed class FilletViewModel : AlignableElementViewModel, IViewModel<Fillet>
    {

        public FilletViewModel(IWorkspaceBit workspaceBit, Fillet model)
            : base(workspaceBit, model)
        {
            Height = 0.05;
        }

        public new Fillet Model => (Fillet) base.Model;

    }
}
