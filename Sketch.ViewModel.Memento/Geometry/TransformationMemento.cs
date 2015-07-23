using System.Collections.Generic;
using System.Xml.Linq;
using PeletonSoft.Sketch.ViewModel.Geometry;
using PeletonSoft.Tools.Model.Collection;
using PeletonSoft.Tools.Model.File;
using PeletonSoft.Tools.Model.Memento;
using PeletonSoft.Tools.Model.Memento.Container;

namespace PeletonSoft.Sketch.ViewModel.Memento.Geometry
{
    public class TransformationMemento : IMemento<TransformationViewModel>
    {
        public string Rotation { get; set; }
        public string Reflection { get; set; }
        public void GetState(TransformationViewModel originator)
        {
            Reflection = originator.Reflections.GetKeyByValue(originator.Reflection);
            Rotation = originator.Rotations.GetKeyByValue(originator.Rotation);
        }

        public void SetState(TransformationViewModel originator)
        {
            originator.Reflection = originator.Reflections.GetValueByKeyOrDefault(Reflection);
            originator.Rotation = originator.Rotations.GetValueByKeyOrDefault(Rotation);
        }

        public IEnumerable<IFileBox> GetFiles()
        {
            return null;
        }

        public XElement GetXml(Dictionary<string, IFileBox> files)
        {
            return new XElement("root",
                new XElement("Rotation", Rotation),
                new XElement("Reflection", Reflection));
        }

        public void SetXml(XElement xml, string path)
        {
            Rotation = (string) xml.Element("Rotation");
            Reflection = (string)xml.Element("Reflection");
        }
    }
}
