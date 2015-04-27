using System;
using System.Collections.Generic;
using System.Xml.Linq;
using PeletonSoft.Sketch.ViewModel.Element.Primitive;
using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewMode.Memento.Element.Primitive
{
    public class TieBackSideMemento : IMemento<TieBackSideViewModel>
    {
        public double Weight { get; set; }
        public double TailScatter { get; set; }
        public void GetState(TieBackSideViewModel originator)
        {
            Weight = originator.Weight;
            TailScatter = originator.TailScatter;
        }

        public void SetState(TieBackSideViewModel originator)
        {
            originator.Weight = Weight;
            originator.TailScatter = TailScatter;
        }

        public IEnumerable<string> GetFiles()
        {
            return null;
        }

        public XElement GetXml(Dictionary<string, string> files)
        {
            return new XElement("root",
                new XElement("TailScatter", TailScatter),
                new XElement("Weight", Weight));
        }

        public void SetXml(XElement xml, string path)
        {
            Weight = (double)xml.Element("Weight");
            TailScatter = (double)xml.Element("TailScatter");
        }
    }
}
