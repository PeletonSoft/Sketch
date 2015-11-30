using System.Collections.Generic;
using PeletonSoft.Sketch.ViewModel.DataTransfer.Interface;
using PeletonSoft.Tools.Model.Collection;
using PeletonSoft.Tools.Model.Memento.Container;

namespace PeletonSoft.Sketch.ViewModel.Interface.Container
{
    public interface IPresentListViewModel : IListOriginator<IPresentDataTransfer>
    {
        new IPresentViewModel Default { get; }
        new IEnumerable<IContainerRecord<IPresentViewModel>> Items { get; }
    }
}
