using System.Collections.Generic;
using System.Xml.Linq;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Tools.Model.File;
using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewModel.Memento.Present
{
    public class PresentMemento : IMemento<IPresentViewModel>
    {
        public double Zoom { get; set; }

        public virtual void GetState(IPresentViewModel originator)
        {
            Zoom = originator.Zoom;
        }

        public virtual void SetState(IPresentViewModel originator)
        {
            originator.RestoreDefault();
            originator.Zoom = Zoom;
        }

        public virtual IEnumerable<IFileBox> GetFiles()
        {
            return null;
        }

        public virtual XElement GetXml(Dictionary<string, IFileBox> files)
        {
            return new XElement("root",
                new XElement("Zoom", Zoom)
                );
        }

        public virtual void SetXml(XElement xml, string path)
        {
            Zoom = (double)xml.Element("Zoom");
        }
    }
}
