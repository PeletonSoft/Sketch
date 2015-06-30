using System.Collections.Generic;
using System.Xml.Linq;
using PeletonSoft.Sketch.ViewModel.Interface.Element;
using PeletonSoft.Tools.Model.File;
using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewModel.Memento.Element.Primitive
{
    public class ClotheMemento : IMemento<IClotheViewModel>
    {
        public double? Width { get; set; }
        public double? Height { get; set; }

        public void GetState(IClotheViewModel originator)
        {
            Width = originator.Width;
            Height = originator.Height;    
        }

        public void SetState(IClotheViewModel originator)
        {
            originator.RestoreDefault();

            originator.Width = Width;
            originator.Height = Height;
        }

        public IEnumerable<IFileBox> GetFiles()
        {
            return null;
        }

        public XElement GetXml(Dictionary<string, IFileBox> files)
        {
            var xml = new XElement("root");

            if (Width != null)
            {
                xml.Add(new XElement("Width", Width));
            }

            if (Height != null)
            {
                xml.Add(new XElement("Height", Height));
            }

            return xml;
        }

        public void SetXml(XElement xml, string path)
        {
            
        }
    }
}
