using PeletonSoft.Sketch.ViewModel.Element;
using PeletonSoft.Sketch.ViewModel.Factory;
using PeletonSoft.Sketch.ViewModel.Interface.Element;
using PeletonSoft.Sketch.ViewModel.Memento.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Memento.Element.Service;
using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewModel.Memento.Element
{
    public sealed class TulleMemento : CustomElementMemento, IMemento<TulleViewModel>
    {
        protected override void GetState(IElementViewModel originator)
        {
            GetState((TulleViewModel)originator);
        }

        protected override void SetState(IElementViewModel originator)
        {
            SetState((TulleViewModel)originator);
        }
        public void GetState(TulleViewModel originator)
        {
            base.GetState(originator);
        }

        public void SetState(TulleViewModel originator)
        {
            base.SetState(originator);
        }
    }

    public sealed class TulleMementoRegister : IMementoRegister
    {
        public  void Register()
        {
            var record = new ElementMementoFactoryRecord(
                typeof(TulleFactoryViewModel),
                typeof(TulleViewModel),
                typeof(TulleMemento),
                () => new TulleMemento());
            ElementMementoFactoryService.Register(record);
        }
    }
}
