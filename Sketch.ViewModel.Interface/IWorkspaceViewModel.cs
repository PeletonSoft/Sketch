using System.ComponentModel;
using PeletonSoft.Tools.Model;
using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewModel.Interface
{
    public interface IWorkspaceViewModel : INotifyPropertyChanged, IOriginator
    {
        IScreenViewModel Screen { get; set; }
        IWorkModeViewModel WorkMode { get; set; }
        IPresentViewModel Present { get; set; }
        IContainerOriginator<IWorkModeViewModel> WorkModes { get; }
        IContainerOriginator<IPresentViewModel> Presents { get;  }
        IElementListViewModel ElementList { get; }
        FactoryCollection Factories { get; }
        ICommandFactory CommandFactory { get;  }
        byte[] ImageData { get; set; }
    }
}