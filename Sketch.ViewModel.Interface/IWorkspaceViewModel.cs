using System.Collections.Generic;
using PeletonSoft.Sketch.Model.Interface;
using PeletonSoft.Sketch.ViewModel.Interface.Element;
using PeletonSoft.Tools.Model.Dependency;
using PeletonSoft.Tools.Model.File;
using PeletonSoft.Tools.Model.Logic;
using PeletonSoft.Tools.Model.Memento;
using PeletonSoft.Tools.Model.Memento.Container;
using PeletonSoft.Tools.Model.Setting;

namespace PeletonSoft.Sketch.ViewModel.Interface
{
    public interface IWorkspaceViewModel : INotifyViewModel<IWorkspace>, IOriginator
    {
        IScreenViewModel Screen { get; set; }
        IWorkModeViewModel WorkMode { get; set; }
        IPresentViewModel Present { get; set; }
        IContainerOriginator<IWorkModeViewModel> WorkModes { get; }
        IContainerOriginator<IPresentViewModel> Presents { get;  }
        IElementListViewModel ElementList { get; }
        IEnumerable<IElementFactoryViewModel<IElementViewModel>> Factories { get; }
        ICommandFactory CommandFactory { get;  }
        ImageBox ImageBox { get; set; }
        ISettingProvider SettingProvider { get;}
    }
}