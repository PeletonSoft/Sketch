using System.Collections.Generic;
using System.Windows;
using PeletonSoft.Sketch.ViewModel.Interface.Element;
using PeletonSoft.Tools.Model.ObjectEvent.Render;

namespace PeletonSoft.Sketch.ViewModel.Interface.Tools
{
    public interface INotifyOpacityMaskRenderChanged :
        INotifyRenderChanged<IElementViewModel, IElementViewModel, IEnumerable<Point>>
    {
    }
}
