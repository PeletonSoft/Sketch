using PeletonSoft.Sketch.ViewMode.Memento.Element.Custom;
using PeletonSoft.Sketch.ViewMode.Memento.Element.Service;
using PeletonSoft.Sketch.ViewModel.Element;
using PeletonSoft.Sketch.ViewModel.Factory;
using PeletonSoft.Sketch.ViewModel.Interface.Element;
using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewMode.Memento.Element
{
    public sealed class PanelMemento : CustomElementMemento, IMemento<PanelViewModel>
    {
        protected override void GetState(IElementViewModel originator)
        {
            GetState((PanelViewModel)originator);
        }

        protected override void SetState(IElementViewModel originator)
        {
            SetState((PanelViewModel)originator);
        }
        public void GetState(PanelViewModel originator)
        {
            base.GetState(originator);
        }

        public void SetState(PanelViewModel originator)
        {
            base.SetState(originator);
        }
    }

    public sealed class PanelMementoRegister : IMementoRegister
    {
        public  void Register()
        {
            var record = new ElementMementoFactoryRecord(
                typeof(PanelFactoryViewModel),
                typeof(PanelViewModel),
                typeof(PanelMemento),
                () => new PanelMemento());
            ElementMementoFactoryService.Register(record);
        }
    }
}
