using PeletonSoft.Sketch.Model.Element.Custom;
using PeletonSoft.Sketch.Model.Element.Primitive;
using PeletonSoft.Sketch.ViewModel.DataTransfer.Element.Custom;
using PeletonSoft.Sketch.ViewModel.DataTransfer.Interface;
using PeletonSoft.Sketch.ViewModel.Element.Primitive;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewModel.Element.Custom
{
    public abstract class EqualSwagTailViewModel : SwagTailViewModel
    {
        public ShoulderViewModel Shoulder { get; }
        protected EqualSwagTailViewModel(IWorkspaceBit workspaceBit, SwagTail model)
            : base(workspaceBit, model)
        {
            Shoulder = new ShoulderViewModel(new Shoulder());

            Shoulder.Synchronize(LeftShoulder);
            Shoulder.Synchronize(RightShoulder);

            Shoulder.Length = 0.25 * Layout.Width;
            Shoulder.WaveHeight = Shoulder.Length / WaveCount * 1.3;
        }

        public override IElementDataTransfer CreateState() => new EqualSwagTailDataTransfer();
        public override void Save(IElementDataTransfer state) => Save((EqualSwagTailDataTransfer)state);
        public override void Restore(IElementDataTransfer state) => Restore((EqualSwagTailDataTransfer)state);
        private void Save(EqualSwagTailDataTransfer state)
        {
            base.Save(state);
            state.Shoulder = Shoulder.Save();
        }
        private void Restore(EqualSwagTailDataTransfer state)
        {
            base.Restore(state);
            Shoulder.Restore(state.Shoulder);
        }
    }
}
