using System.Collections.Generic;
using System.Windows.Input;
using PeletonSoft.Sketch.ViewModel.Interface.Element;
using PeletonSoft.Sketch.ViewModel.Interface.Tools;
using PeletonSoft.Tools.Model.Collection;
using PeletonSoft.Tools.Model.Memento;
using PeletonSoft.Tools.Model.NotifyChanged.ChangedItem;

namespace PeletonSoft.Sketch.ViewModel.Interface
{
    public interface IElementListViewModel : IOriginator, IContainer<IElementViewModel>,
        IChangeableCollection<IElementViewModel>, INotifyOpacityMaskRenderChanged
    {
        IReadOnlyList<IElementViewModel> GetBelow(IElementViewModel element);
        IScreenViewModel Screen { get; }
        IEnumerable<IElementFactoryViewModel<IElementViewModel>> Factories { get; }
        IElementViewModel AppendElement(IElementFactoryViewModel<IElementViewModel> factory);

        ICommand MoveUpElementCommand { get; }
        ICommand MoveDownElementCommand { get; }
        ICommand MoveToElementCommand { get; }
        ICommand RemoveElementCommand { get; }
        ICommand UnselectElementCommand { get; }

    }

}