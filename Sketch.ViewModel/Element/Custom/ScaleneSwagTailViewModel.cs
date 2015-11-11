using PeletonSoft.Sketch.Model.Element.Custom;
using PeletonSoft.Sketch.ViewModel.DataTransfer.Element.Custom;
using PeletonSoft.Sketch.ViewModel.DataTransfer.Interface;
using PeletonSoft.Sketch.ViewModel.Element.Primitive;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Tools.Model.Memento;

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

        public override IElementDataTransfer CreateState() => new ScaleneSwagTailDataTransfer();
        public override void Save(IElementDataTransfer state) => Save((ScaleneSwagTailDataTransfer)state);
        public override void Restore(IElementDataTransfer state) => Restore((ScaleneSwagTailDataTransfer)state);

        private void Save(ScaleneSwagTailDataTransfer state)
        {
            base.Save(state);
            state.LeftShoulder = LeftShoulder.Save();
            state.RightShoulder = RightShoulder.Save();
        }

        private void Restore(ScaleneSwagTailDataTransfer state)
        {
            base.Restore(state);
            LeftShoulder.Restore(state.LeftShoulder);
            RightShoulder.Restore(state.RightShoulder);
        }

    }

}
