using System;
using PeletonSoft.Sketch.ViewModel.DataTransfer.Interface;
using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewModel.DataTransfer
{
    [Serializable]
    public class WorkspaceDataTransfer : IDataTransfer
    {
        public string ProgramName { get; set; }
        public string Version { get; set; }
        
        public IScreenDataTransfer Screen { get; set; }
        public IListDataTransfer<IElementDataTransfer> ElementList { get; set; }
        public string WorkMode { get; set; }
        public IListDataTransfer<IWorkModeDataTransfer>  WorkModes { get; set; }
        public string Present { get; set; }
        public IListDataTransfer<IPresentDataTransfer> Presents { get; set; }
    }
}
