using System.Collections.Generic;
using System.Windows.Input;
using PeletonSoft.Sketch.ViewModel.Interface.Element;
using PeletonSoft.Sketch.ViewModel.Interface.Tools;
using PeletonSoft.Tools.Model.Collection;
using PeletonSoft.Tools.Model.Memento;
using PeletonSoft.Tools.Model.Memento.Container;
using PeletonSoft.Tools.Model.NotifyChanged;
using PeletonSoft.Tools.Model.NotifyChanged.ElementList;

namespace PeletonSoft.Sketch.ViewModel.Interface
{
    public interface IElementListViewModel : IOriginator, ISelectableList<IElementViewModel>,
        INotifyElementListChanged, IContainer<IElementViewModel>,
        INotifyOpacityMaskRenderChanged
    {
        bool IsEmpty { get; }
        INotifyChangedReadOnlyCollection<IElementViewModel> Collection { get; }
        IList<IElementViewModel> GetBelowElements(IElementViewModel element);
        ICommand MoveUpElementCommand { get; }
        ICommand MoveDownElementCommand { get; }
        ICommand MoveToElementCommand { get; }
        ICommand RemoveElementCommand { get; }
        ICommand UnselectElementCommand { get; }

        IScreenViewModel Screen { get; }
        IEnumerable<IElementFactoryViewModel<IElementViewModel>> Factories { get; }
        IElementViewModel AddElement(IElementFactoryViewModel<IElementViewModel> factory);

        void Clear();
        void Unselect();
    }

}