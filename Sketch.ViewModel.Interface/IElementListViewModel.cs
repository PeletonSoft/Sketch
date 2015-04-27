using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using PeletonSoft.Sketch.ViewModel.Interface.Element;
using PeletonSoft.Sketch.ViewModel.Interface.Tools;
using PeletonSoft.Tools.Model;
using PeletonSoft.Tools.Model.Memento;
using PeletonSoft.Tools.Model.NotifyChanged.ElementList;

namespace PeletonSoft.Sketch.ViewModel.Interface
{
    public interface IElementListViewModel : IOriginator, INotifyPropertyChanged,
        INotifyElementListChanged, IContainer<IElementViewModel>,
        INotifyOpacityMaskRenderChanged
    {
        int SelectedIndex { get; set; }
        IElementViewModel SelectedItem { get; }
        bool IsEmpty { get; }
        ICommand CreateElementCommand { get; }
        ICommand MoveUpElementCommand { get; }
        ICommand MoveDownElementCommand { get; }
        ICommand MoveToElementCommand { get; }
        ICommand RemoveElementCommand { get; }

        IScreenViewModel Screen { get; }
        IEnumerable<IElementFactoryViewModel<IElementViewModel>> Factories { get; }

        IElementViewModel AddElement(IElementFactoryViewModel<IElementViewModel> factory);

        void Clear();
    }

}