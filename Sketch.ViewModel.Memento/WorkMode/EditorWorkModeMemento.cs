using PeletonSoft.Sketch.ViewModel.Memento.Service;
using PeletonSoft.Sketch.ViewModel.WorkMode;
using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewModel.Memento.WorkMode
{
    public class EditorWorkModeMemento : WorkModeMemento
    {
    }

    public class EditorWorkModeMementoRegister : IMementoRegister
    {
        public void Register()
        {
            var service = new WorkModeMementoService();
            service.Register(
                typeof (EditorWorkModeViewModel),
                () => new EditorWorkModeMemento());
        }
    }
}
