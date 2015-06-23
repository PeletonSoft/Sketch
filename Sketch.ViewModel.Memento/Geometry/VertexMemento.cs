using System.Collections.Generic;
using System.Xml.Linq;
using PeletonSoft.Sketch.ViewModel.Geometry;
using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewModel.Memento.Geometry
{
    public class VertexMemento : IMemento<VertexViewModel>
    {
        public double X { get; set; }
        public double Y { get; set; }
        public void GetState(VertexViewModel originator)
        {
            X = originator.X;
            Y = originator.Y;
        }

        public void SetState(VertexViewModel originator)
        {
            originator.X = X;
            originator.Y = Y;
        }

        public IEnumerable<string> GetFiles()
        {
            return null;
        }

        public XElement GetXml(Dictionary<string, string> files)
        {
            return new XElement("root",
                new XElement("X", X),
                new XElement("Y", Y)
                );
        }

        public void SetXml(XElement xml, string path)
        {
            X = (double)xml.Element("X");
            Y = (double)xml.Element("Y");
        }
    }
}
