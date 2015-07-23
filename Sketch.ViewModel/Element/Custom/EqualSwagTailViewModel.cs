using PeletonSoft.Sketch.Model.Element.Custom;
using PeletonSoft.Sketch.Model.Element.Primitive;
using PeletonSoft.Sketch.ViewModel.Element.Primitive;
using PeletonSoft.Sketch.ViewModel.Interface;

namespace PeletonSoft.Sketch.ViewModel.Element.Custom
{
    public abstract class EqualSwagTailViewModel : SwagTailViewModel
    {
        public ShoulderViewModel Shoulder { get; private set; }
        protected EqualSwagTailViewModel(IWorkspaceBit workspaceBit, SwagTail model)
            : base(workspaceBit, model)
        {
            Shoulder = new ShoulderViewModel(new Shoulder());

            Shoulder.Synchronize(LeftShoulder);
            Shoulder.Synchronize(RightShoulder);

            Shoulder.Length = 0.25 * Layout.Width;
            Shoulder.WaveHeight = Shoulder.Length / WaveCount * 1.3;
        }
    }
}
