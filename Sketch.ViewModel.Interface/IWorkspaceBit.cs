using System.Collections.Generic;
using PeletonSoft.Sketch.ViewModel.Interface.Element;
using PeletonSoft.Sketch.ViewModel.Interface.Tools;
using PeletonSoft.Tools.Model;
using PeletonSoft.Tools.Model.NotifyChanged.ElementList;

namespace PeletonSoft.Sketch.ViewModel.Interface
{
    public interface IWorkspaceBit : INotifyElementListChanged, INotifyOpacityMaskRenderChanged
    {
        IScreenViewModel Screen { get; }
        IEnumerable<IElementFactoryViewModel<IElementViewModel>> Factories { get; }
        IList<IElementViewModel> GetBelowElements(IElementViewModel element);
        ICommandFactory CommandFactory { get; }
    }
}