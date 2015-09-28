using System.ComponentModel;

namespace PeletonSoft.Tools.Model.Logic
{
    public interface ISynchViewModel<in T> : INotifyPropertyChanged 
        where T : INotifyPropertyChanged
    {
        void Synchronize(T destination);
    }
}
