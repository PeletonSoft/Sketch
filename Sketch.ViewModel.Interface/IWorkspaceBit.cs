using System.Collections.Generic;
using PeletonSoft.Sketch.ViewModel.Interface.Element;
using PeletonSoft.Sketch.ViewModel.Interface.Tools;
using PeletonSoft.Tools.Model.Dependency;
using PeletonSoft.Tools.Model.NotifyChanged.ChangedItem;


namespace PeletonSoft.Sketch.ViewModel.Interface
{
    public interface IWorkspaceBit : INotifyItemChanged, INotifyOpacityMaskRenderChanged
    {
        IScreenViewModel Screen { get; }
        IReadOnlyList<IElementViewModel> GetBelowElements(IElementViewModel element);
        ICommandFactory CommandFactory { get; }
    }
}