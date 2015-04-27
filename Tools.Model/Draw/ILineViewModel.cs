using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Tools.Model.Draw
{
    public interface ILineViewModel : IDrawViewModel, IOriginator
    {
        IPointViewModel Start { get; set; }
        IPointViewModel Finish { get; set; }
    }
}