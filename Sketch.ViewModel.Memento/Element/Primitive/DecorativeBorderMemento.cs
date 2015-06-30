using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Xml.Linq;
using PeletonSoft.Sketch.ViewModel.Element.Primitive;
using PeletonSoft.Tools.Model.File;
using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewModel.Memento.Element.Primitive
{
    public class DecorativeBorderMemento : IMemento<DecorativeBorderViewModel>
    {
        public double Width { get; set; }
        public double Height { get; set; }
        public IEnumerable<Point> Points { get; set; }
        public void GetState(DecorativeBorderViewModel originator)
        {
            Width = originator.Width;
            Height = originator.Height;
            Points = new List<Point>(originator.Points);
        }

        public void SetState(DecorativeBorderViewModel originator)
        {
            originator.Width = Width;
            originator.Height = Height;
            originator.Points = new List<Point>(Points);
            
        }

        public IEnumerable<IFileBox> GetFiles()
        {
            return null;
        }

        public XElement GetXml(Dictionary<string, IFileBox> files)
        {
            return new XElement("root",
                new XElement("Width", Width),
                new XElement("Height", Height),
                new XElement("Points", Points
                    .Select(point => new XElement("Point",
                        new XElement("X", point.X),
                        new XElement("Y", point.Y)))
                    )
                );

        }

        public void SetXml(XElement xml, string path)
        {
            Width = (double)xml.Element("Width");
            Height = (double)xml.Element("Height");
            Points = xml.Element("Points").Elements("Point")
                .Select(el => new Point((double) el.Element("X"), (double) el.Element("Y")))
                .ToList();
        }
    }
}
