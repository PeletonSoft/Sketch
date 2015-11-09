using System.Collections.Generic;
using System.Windows.Input;
using PeletonSoft.Sketch.ViewModel.DataTransfer.Interface;
using PeletonSoft.Tools.Model.Collection;
using PeletonSoft.Tools.Model.Logic;
using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewModel.Interface.Element
{
    public interface IElementViewModel : INotifyViewModel, IOriginator, ILayoutable, ICollectionItem, IOriginator<IElementDataTransfer>
    {
        string Description { get; set; }
        bool Visibility { get; set; }
        double Opacity { get; set; }

        IReadOnlyList<IElementViewModel> Below { get; }
        ICommand MoveToElementCommand { get; set; }
    }
}
