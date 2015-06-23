using System.Collections.Generic;
using System.Xml.Linq;
using PeletonSoft.Sketch.ViewModel.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Interface.Element;
using PeletonSoft.Sketch.ViewModel.Memento.Element.Primitive;

namespace PeletonSoft.Sketch.ViewModel.Memento.Element.Custom
{
    public class EqualSwagTailMemento : SwagTailMemento
    {
        public ShoulderMemento Shoulder { get; set; }
        protected override void GetState(IElementViewModel originator)
        {
            GetState((EqualSwagTailViewModel)originator);   
        }

        protected override void SetState(IElementViewModel originator)
        {
            SetState((EqualSwagTailViewModel)originator);   
        }

        private void GetState(EqualSwagTailViewModel originator)
        {
            base.GetState(originator);
            Shoulder = new ShoulderMemento();            
            Shoulder.GetState(originator.Shoulder);
        }

        private void SetState(EqualSwagTailViewModel originator)
        {
            base.SetState(originator);
            Shoulder.SetState(originator.Shoulder);

        }

        public override XElement GetXml(Dictionary<string, string> files)
        {
            var xml = base.GetXml(files);
            xml.Add(new XElement("Shoulder", Shoulder.GetXml(files).Elements()));
            return xml;
        }

        public override void SetXml(XElement xml, string path)
        {
            base.SetXml(xml, path);
            Shoulder = new ShoulderMemento();            
            Shoulder.SetXml(xml.Element("Shoulder"),path);
        }
    }
}
