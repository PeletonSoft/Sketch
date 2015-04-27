using PeletonSoft.Sketch.ViewMode.Memento.Element.Custom;
using PeletonSoft.Sketch.ViewMode.Memento.Element.Service;
using PeletonSoft.Sketch.ViewModel.Element;
using PeletonSoft.Sketch.ViewModel.Factory;
using PeletonSoft.Sketch.ViewModel.Interface.Element;
using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewMode.Memento.Element
{
    public sealed class ScaleneTailMemento : ScaleneSwagTailMemento, IMemento<ScaleneTailViewModel>
    {
        protected override void GetState(IElementViewModel originator)
        {
            GetState((ScaleneTailViewModel)originator);
        }

        protected override void SetState(IElementViewModel originator)
        {
            SetState((ScaleneTailViewModel)originator);
        }

        public void GetState(ScaleneTailViewModel originator)
        {
            base.GetState(originator);
        }

        public void SetState(ScaleneTailViewModel originator)
        {
            base.SetState(originator);
        }
    }

    public sealed class ScaleneTailMementoRegister : IMementoRegister
    {
        public void Register()
        {
            var record = new ElementMementoFactoryRecord(
                typeof(ScaleneTailFactoryViewModel),
                typeof(ScaleneTailViewModel),
                typeof(ScaleneTailMemento),
                () => new ScaleneTailMemento());
            ElementMementoFactoryService.Register(record);
        }
    }

}
