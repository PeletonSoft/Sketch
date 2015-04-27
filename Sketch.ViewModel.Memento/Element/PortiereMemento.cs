using PeletonSoft.Sketch.ViewMode.Memento.Element.Custom;
using PeletonSoft.Sketch.ViewMode.Memento.Element.Service;
using PeletonSoft.Sketch.ViewModel.Element;
using PeletonSoft.Sketch.ViewModel.Factory;
using PeletonSoft.Sketch.ViewModel.Interface.Element;
using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewMode.Memento.Element
{

    public sealed class PortiereMemento : CustomElementMemento, IMemento<PortiereViewModel>
    {
        protected override void GetState(IElementViewModel originator)
        {
            GetState((PortiereViewModel)originator);
        }

        protected override void SetState(IElementViewModel originator)
        {
            SetState((PortiereViewModel)originator);
        }

        public void GetState(PortiereViewModel originator)
        {
            base.GetState(originator);
        }

        public void SetState(PortiereViewModel originator)
        {
            base.SetState(originator);
        }
    }

    public sealed class PortiereMementoRegister : IMementoRegister
    {
        public void Register()
        {
            var record = new ElementMementoFactoryRecord(
                typeof(PortiereFactoryViewModel),
                typeof(PortiereViewModel),
                typeof(PortiereMemento),
                () => new PortiereMemento());
            ElementMementoFactoryService.Register(record);
        }
    }
}
