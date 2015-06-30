using System.Collections.Generic;
using System.Xml.Linq;
using PeletonSoft.Sketch.ViewModel.Element.Primitive;
using PeletonSoft.Tools.Model.File;
using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewModel.Memento.Element.Primitive
{
    public class ShoulderMemento : IMemento<ShoulderViewModel>
    {
        public double Length { get; set; }
        public double OffsetY { get; set; }
        public double WaveHeight { get; set; }
        public double Slope { get; set; }
        public void GetState(ShoulderViewModel originator)
        {
            Length = originator.Length;
            OffsetY = originator.OffsetY;
            WaveHeight = originator.WaveHeight;
            Slope = originator.Slope;
        }

        public void SetState(ShoulderViewModel originator)
        {
            originator.RestoreDefault();
            originator.Length = Length;
            originator.OffsetY = OffsetY;
            originator.WaveHeight = WaveHeight;
            originator.Slope = Slope;
        }

        public IEnumerable<IFileBox> GetFiles()
        {
            return null;
        }

        public XElement GetXml(Dictionary<string, IFileBox> files)
        {
            return new XElement("root",
                new XElement("Length", Length),
                new XElement("OffsetY", OffsetY),
                new XElement("WaveHeight", WaveHeight),
                new XElement("Slope", Slope));
        }

        public void SetXml(XElement xml, string path)
        {
            Length = (double)xml.Element("Length");
            OffsetY = (double)xml.Element("OffsetY");
            WaveHeight = (double)xml.Element("WaveHeight");
            Slope = (double)xml.Element("Slope");
        }
    }
}
