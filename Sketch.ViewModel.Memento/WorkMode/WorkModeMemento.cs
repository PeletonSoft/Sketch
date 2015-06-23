using System.Collections.Generic;
using System.Xml.Linq;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewModel.Memento.WorkMode
{
    public class WorkModeMemento : IMemento<IWorkModeViewModel>
    {
        public void GetState(IWorkModeViewModel originator)
        {
        }

        public void SetState(IWorkModeViewModel originator)
        {
            originator.RestoreDefault();
        }

        public IEnumerable<string> GetFiles()
        {
            return null;
        }

        public XElement GetXml(Dictionary<string, string> files)
        {
            return new XElement("root");
        }

        public void SetXml(XElement xml, string path)
        {
            
        }
    }
}
