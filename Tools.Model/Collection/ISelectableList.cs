using System.ComponentModel;

namespace PeletonSoft.Tools.Model.Collection
{

    public interface ISelectableList<out T> : INotifyPropertyChanged
    {
        int SelectedIndex { get; set; }
        T SelectedItem { get; }
    }
}
