using PeletonSoft.Sketch.ViewModel.Element;
using PeletonSoft.Sketch.ViewModel.Factory;
using PeletonSoft.Sketch.ViewModel.Interface.Element;
using PeletonSoft.Sketch.ViewModel.Memento.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Memento.Element.Service;
using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewModel.Memento.Element
{
    public sealed class EqualSwagMemento : EqualSwagTailMemento, IMemento<EqualSwagViewModel>
    {
        protected override void GetState(IElementViewModel originator)
        {
            GetState((EqualSwagViewModel)originator);
        }

        protected override void SetState(IElementViewModel originator)
        {
            SetState((EqualSwagViewModel)originator);
        }

        public void GetState(EqualSwagViewModel originator)
        {
            base.GetState(originator);
        }

        public void SetState(EqualSwagViewModel originator)
        {
            base.SetState(originator);
        }
    }

    public sealed class EqualWaveMementoRegister : IMementoRegister
    {
        public void Register()
        {
            var record = new ElementMementoFactoryRecord(
                typeof(EqualSwagFactoryViewModel),
                typeof(EqualSwagViewModel),
                typeof(EqualSwagMemento),
                () => new EqualSwagMemento());
            ElementMementoFactoryService.Register(record);
        }
    }
}
