using PeletonSoft.Sketch.Model.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Element.Primitive;
using PeletonSoft.Sketch.ViewModel.Interface;

namespace PeletonSoft.Sketch.ViewModel.Element.Custom
{
    public abstract class ScaleneSwagTailViewModel : SwagTailViewModel
    {
        protected ScaleneSwagTailViewModel(IWorkspaceBit workspaceBit, SwagTail model)
            : base(workspaceBit, model)
        {
            LeftShoulder.Length = 0.25 * Layout.Width;
            LeftShoulder.WaveHeight = LeftShoulder.Length / WaveCount * 1.3;
            RightShoulder.Length = 0.15 * Layout.Width;
            RightShoulder.WaveHeight = RightShoulder.Length / WaveCount * 1.3;
        }

        public new ShoulderViewModel LeftShoulder => base.LeftShoulder;
        public new ShoulderViewModel RightShoulder => base.RightShoulder;
    }
}
