using System;
using System.Collections.Generic;
using System.Xml.Linq;
using PeletonSoft.Sketch.ViewModel.Geometry;
using PeletonSoft.Tools.Model.File;
using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewModel.Memento.Geometry
{
    public class RectangleMemento : IMemento<RectangleViewModel>
    {
        public VertexMemento TopLeft { get; set; }
        public VertexMemento TopRight { get; set; }
        public VertexMemento BottomLeft { get; set; }
        public VertexMemento BottomRight { get; set; }

        public void GetState(RectangleViewModel originator)
        {
            TopLeft = new VertexMemento();
            TopRight = new VertexMemento();
            BottomLeft = new VertexMemento();
            BottomRight = new VertexMemento();

            TopLeft.GetState(originator.TopLeft);
            TopRight.GetState(originator.TopRight);
            BottomLeft.GetState(originator.BottomLeft);
            BottomRight.GetState(originator.BottomRight);
        }

        public void SetState(RectangleViewModel originator)
        {
            if (originator != null)
            {
                try
                {
                    TopLeft.SetState(originator.TopLeft);
                    BottomRight.SetState(originator.BottomRight);
                    TopRight.SetState(originator.TopRight);
                    BottomLeft.SetState(originator.BottomLeft);
                }
                catch (NullReferenceException)
                {
                }
            }
        }

        public IEnumerable<IFileBox> GetFiles()
        {
            return null;
        }

        public XElement GetXml(Dictionary<string, IFileBox> files)
        {
            return new XElement("root",
                new XElement("TopLeft", TopLeft.GetXml(files).Elements()),
                new XElement("TopRight", TopRight.GetXml(files).Elements()),
                new XElement("BottomLeft", BottomLeft.GetXml(files).Elements()),
                new XElement("BottomRight", BottomRight.GetXml(files).Elements())
                );
        }

        public void SetXml(XElement xml, string path)
        {
            TopLeft = new VertexMemento();
            TopRight = new VertexMemento();
            BottomLeft = new VertexMemento();
            BottomRight = new VertexMemento();

            TopLeft.SetXml(xml.Element("TopLeft"), path);
            TopRight.SetXml(xml.Element("TopRight"), path);
            BottomLeft.SetXml(xml.Element("BottomLeft"), path);
            BottomRight.SetXml(xml.Element("BottomRight"), path);
        }
    }
}
