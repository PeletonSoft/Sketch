using PeletonSoft.Sketch.ViewModel.Element;
using PeletonSoft.Sketch.ViewModel.Factory;
using PeletonSoft.Sketch.ViewModel.Interface.Element;
using PeletonSoft.Sketch.ViewModel.Memento.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Memento.Element.Service;
using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewModel.Memento.Element
{

    public sealed class EqualTailMemento : EqualSwagTailMemento, IMemento<EqualTailViewModel>
    {
        protected override void GetState(IElementViewModel originator)
        {
            GetState((EqualTailViewModel)originator);
        }

        protected override void SetState(IElementViewModel originator)
        {
            SetState((EqualTailViewModel)originator);
        }

        public void GetState(EqualTailViewModel originator)
        {
            base.GetState(originator);
        }

        public void SetState(EqualTailViewModel originator)
        {
            base.SetState(originator);
        }
    }

    public sealed class EqualTailMementoRegister : IMementoRegister
    {
        public void Register()
        {
            var record = new ElementMementoFactoryRecord(
                typeof(EqualTailFactoryViewModel),
                typeof(EqualTailViewModel),
                typeof(EqualTailMemento),
                () => new EqualTailMemento());
            ElementMementoFactoryService.Register(record);
        }
    }
    }
