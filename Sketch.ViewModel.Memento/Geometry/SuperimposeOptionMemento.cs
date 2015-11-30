using System.Collections.Generic;
using System.Xml.Linq;
using PeletonSoft.Sketch.ViewModel.Interface.Element;
using PeletonSoft.Tools.Model.File;
using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewModel.Memento.Geometry
{
    public class SuperimposeOptionMemento : IMemento<ISuperimposeOptionViewModel>
    {
        public double MarkerRadius { get; set; }
        public double MarkerOpacity { get; set; }
        public double ForegroundOpacity { get; set; }
        public double BackgroundOpacity { get; set; }


        public void GetState(ISuperimposeOptionViewModel originator)
        {
            BackgroundOpacity = originator.BackgroundOpacity;
            ForegroundOpacity = originator.ForegroundOpacity;
            MarkerOpacity = originator.MarkerOpacity;
            MarkerRadius = originator.MarkerRadius;
        }

        public void SetState(ISuperimposeOptionViewModel originator)
        {
            originator.RestoreDefault();
            originator.BackgroundOpacity = BackgroundOpacity;
            originator.ForegroundOpacity = ForegroundOpacity;
            originator.MarkerOpacity = MarkerOpacity;
            originator.MarkerRadius = MarkerRadius;
        }

        public IEnumerable<IFileBox> GetFiles()
        {
            return null;
        }

        public XElement GetXml(Dictionary<string, IFileBox> files)
        {
            return new XElement("root",
                new XElement("BackgroundOpacity", BackgroundOpacity),
                new XElement("ForegroundOpacity", ForegroundOpacity),
                new XElement("MarkerOpacity", MarkerOpacity),
                new XElement("MarkerRadius", MarkerRadius));
        }

        public void SetXml(XElement xml, string path)
        {
            BackgroundOpacity = (double)xml.Element("BackgroundOpacity");
            ForegroundOpacity = (double)xml.Element("ForegroundOpacity");
            MarkerOpacity = (double)xml.Element("MarkerOpacity");
            MarkerRadius = (double)xml.Element("MarkerRadius");
        }

        public SuperimposeOptionMemento()
        {
            BackgroundOpacity = 0.9;
            ForegroundOpacity = 0.1;
            MarkerOpacity = 0.2;
            MarkerRadius = 8;
        }
    }
}
