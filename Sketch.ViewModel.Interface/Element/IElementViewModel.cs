using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewModel.Interface.Element
{
    public interface IElementViewModel : INotifyPropertyChanged, IOriginator, ILayoutable
    {
        string Description { get; set; }

        bool Visibility { get; set; }
        double Opacity { get; set; }

        void AfterInsert();
        void BeforeDelete();

        IList<IElementViewModel> Below { get; }
        ICommand MoveToElementCommand { get; set; }
    }
}
