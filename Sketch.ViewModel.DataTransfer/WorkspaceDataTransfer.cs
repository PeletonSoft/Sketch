using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeletonSoft.Sketch.ViewModel.DataTransfer.Interface;
using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewModel.DataTransfer
{
    public class WorkspaceDataTransfer : IDataTransfer
    {
        public string ProgramName { get; set; }
        public string Version { get; set; }
        public string Present { get; set; }
        public string WorkMode { get; set; }
        public IScreenDataTransfer Screen { get; set; }
    }
}
