using PeletonSoft.Sketch.ViewModel.Element.Primitive;
using PeletonSoft.Sketch.ViewModel.Interface;

namespace PeletonSoft.Sketch.ViewModel.Element.Custom
{
    public abstract class ScaleneSwagTailViewModel : SwagTailViewModel
    {
        public ScaleneSwagTailViewModel(IWorkspaceBit workspaceBit)
            : base(workspaceBit)
        {
            LeftShoulder.Length = 0.25 * Layout.Width;
            LeftShoulder.WaveHeight = LeftShoulder.Length / WaveCount * 1.3;
            RightShoulder.Length = 0.15 * Layout.Width;
            RightShoulder.WaveHeight = RightShoulder.Length / WaveCount * 1.3;
        }

        public new ShoulderViewModel LeftShoulder
        {
            get { return base.LeftShoulder; }
        }

        public new ShoulderViewModel RightShoulder
        {
            get { return base.RightShoulder; }
        }
    }
}
