using System.Collections.Generic;
using System.Xml.Linq;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewModel.Memento
{
    public class ScreenMemento : IMemento<IScreenViewModel>
    {
        public double Width { get; set; }
        public double Height { get; set; }
        public void GetState(IScreenViewModel originator)
        {
            Width = originator.Width;
            Height = originator.Height;
        }

        public void SetState(IScreenViewModel originator)
        {
            originator.RestoreDefault();
            originator.Width = Width;
            originator.Height = Height;
        }

        public IEnumerable<string> GetFiles()
        {
            return null;
        }

        public XElement GetXml(Dictionary<string, string> files)
        {
            return new XElement("root",
                new XElement("Width", Width),
                new XElement("Height", Height)
                );
        }

        public void SetXml(XElement xml, string path)
        {
            Width = (double)xml.Element("Width");
            Height = (double)xml.Element("Height");
        }
    }
}
