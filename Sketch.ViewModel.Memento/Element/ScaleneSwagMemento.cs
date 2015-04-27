using PeletonSoft.Sketch.ViewMode.Memento.Element.Custom;
using PeletonSoft.Sketch.ViewMode.Memento.Element.Service;
using PeletonSoft.Sketch.ViewModel.Element;
using PeletonSoft.Sketch.ViewModel.Factory;
using PeletonSoft.Sketch.ViewModel.Interface.Element;
using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewMode.Memento.Element
{
    public sealed class ScaleneSwagMemento : ScaleneSwagTailMemento, IMemento<ScaleneSwagViewModel>
    {
        protected override void GetState(IElementViewModel originator)
        {
            GetState((ScaleneSwagViewModel)originator);
        }

        protected override void SetState(IElementViewModel originator)
        {
            SetState((ScaleneSwagViewModel)originator);
        }

        public void GetState(ScaleneSwagViewModel originator)
        {
            base.GetState(originator);
        }

        public void SetState(ScaleneSwagViewModel originator)
        {
            base.SetState(originator);
        }
    }

    public sealed class ScaleneWaveMementoRegister : IMementoRegister
    {
        public void Register()
        {
            var record = new ElementMementoFactoryRecord(
                typeof(ScaleneSwagFactoryViewModel),
                typeof(ScaleneSwagViewModel),
                typeof(ScaleneSwagMemento),
                () => new ScaleneSwagMemento());
            ElementMementoFactoryService.Register(record);
        }
    }
}
