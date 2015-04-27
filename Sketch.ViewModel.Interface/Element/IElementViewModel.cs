using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using PeletonSoft.Sketch.ViewModel.Interface.Layout;
using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewModel.Interface.Element
{
    public interface IElementViewModel : INotifyPropertyChanged, IOriginator
    {
        string Description { get; set; }
        IClotheViewModel Clothe { get; }

        bool Visibility { get; set; }
        double Opacity { get; set; }

        void AfterInsert();
        void BeforeDelete();

        ILayoutViewModel Layout { get; }

        IList<IElementViewModel> Below { get; }
        ICommand MoveToElementCommand { get; set; }
        
    }
}
