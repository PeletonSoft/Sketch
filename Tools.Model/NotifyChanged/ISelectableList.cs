using System.ComponentModel;

namespace PeletonSoft.Tools.Model.NotifyChanged
{
    public interface ISelectableList<out T> : INotifyPropertyChanged
    {
        int SelectedIndex { get; set; }
        bool IsValidIndex(int index);
        T SelectedItem { get; }
    }
}
