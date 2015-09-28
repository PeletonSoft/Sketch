using PeletonSoft.Sketch.ViewModel.Element;
using PeletonSoft.Sketch.ViewModel.Factory;
using PeletonSoft.Sketch.ViewModel.Interface.Element;
using PeletonSoft.Sketch.ViewModel.Memento.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Memento.Element.Service;
using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewModel.Memento.Element
{
    public class PleatMemento : PleatableMemento, IMemento<PleatViewModel>
    {
        protected override void GetState(IElementViewModel originator)
        {
            GetState((PleatViewModel)originator);
        }

        protected override void SetState(IElementViewModel originator)
        {
            SetState((PleatViewModel)originator);
        }
        public void GetState(PleatViewModel originator)
        {
            base.GetState(originator);
        }

        public void SetState(PleatViewModel originator)
        {
            base.SetState(originator);
        }
    }

    public sealed class PleatMementoRegister : IMementoRegister
    {
        public void Register()
        {
            var record = new ElementMementoFactoryRecord(
                typeof(PleatFactoryViewModel),
                typeof(PleatViewModel),
                typeof(PleatMemento),
                () => new PleatMemento());
            ElementMementoFactoryService.Register(record);
        }
    }
}
