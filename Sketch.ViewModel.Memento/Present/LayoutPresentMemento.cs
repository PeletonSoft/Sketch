using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Sketch.ViewModel.Memento.Service;
using PeletonSoft.Sketch.ViewModel.Present;
using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewModel.Memento.Present
{
    public sealed class LayoutPresentMemento : PresentMemento, IMemento<LayoutPresentViewModel>
    {
        public override void GetState(IPresentViewModel originator)
        {
            GetState((LayoutPresentViewModel)originator);
        }

        public override void SetState(IPresentViewModel originator)
        {
            SetState((LayoutPresentViewModel)originator);
        }
        public void GetState(LayoutPresentViewModel originator)
        {
            base.GetState(originator);
        }

        public void SetState(LayoutPresentViewModel originator)
        {
            base.GetState(originator);
        }
    }

    public sealed class LayoutPresentMementoRegister : IMementoRegister
    {
        public void Register()
        {
            var service = new PresentMementoService();
            service.Register(
                typeof (LayoutPresentViewModel),
                () => new LayoutPresentMemento());
        }
    }
}
