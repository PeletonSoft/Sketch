using PeletonSoft.Sketch.ViewMode.Memento.Element.Custom;
using PeletonSoft.Sketch.ViewMode.Memento.Element.Service;
using PeletonSoft.Sketch.ViewModel.Element;
using PeletonSoft.Sketch.ViewModel.Factory;
using PeletonSoft.Sketch.ViewModel.Interface.Element;
using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewMode.Memento.Element
{
    public sealed class FilletMemento : CustomElementMemento, IMemento<FilletViewModel>
    {
        protected override void GetState(IElementViewModel originator)
        {
            GetState((FilletViewModel)originator);
        }

        protected override void SetState(IElementViewModel originator)
        {
            SetState((FilletViewModel)originator);
        }
        public void GetState(FilletViewModel originator)
        {
            base.GetState(originator);
        }

        public void SetState(FilletViewModel originator)
        {
            base.SetState(originator);
        }
    }

    public sealed class FilletMementoRegister : IMementoRegister
    {
        public  void Register()
        {
            var record = new ElementMementoFactoryRecord(
                typeof(FilletFactoryViewModel),
                typeof(FilletViewModel),
                typeof(FilletMemento),
                () => new FilletMemento());
            ElementMementoFactoryService.Register(record);
        }
    }
}
