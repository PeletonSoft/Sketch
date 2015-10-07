using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewModel.Interface.Draw
{
    public interface ILineViewModel : IDrawViewModel, IOriginator
    {
        IPointViewModel Start { get; set; }
        IPointViewModel Finish { get; set; }
    }
}