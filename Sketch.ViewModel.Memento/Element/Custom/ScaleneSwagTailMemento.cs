using System.Collections.Generic;
using System.Xml.Linq;
using PeletonSoft.Sketch.ViewModel.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Interface.Element;
using PeletonSoft.Sketch.ViewModel.Memento.Element.Primitive;

namespace PeletonSoft.Sketch.ViewModel.Memento.Element.Custom
{
    public class ScaleneSwagTailMemento : SwagTailMemento
    {
        public ShoulderMemento LeftShoulder { get; set; }
        public ShoulderMemento RightShoulder { get; set; }
        protected override void GetState(IElementViewModel originator)
        {
            GetState((ScaleneSwagTailViewModel)originator);
        }

        protected override void SetState(IElementViewModel originator)
        {
            SetState((ScaleneSwagTailViewModel)originator);
        }

        private void GetState(ScaleneSwagTailViewModel originator)
        {
            base.GetState(originator);
            LeftShoulder = new ShoulderMemento();
            RightShoulder = new ShoulderMemento();
            LeftShoulder.GetState(originator.LeftShoulder);
            RightShoulder.GetState(originator.RightShoulder);
        }

        private void SetState(ScaleneSwagTailViewModel originator)
        {
            base.SetState(originator);
            LeftShoulder.SetState(originator.LeftShoulder);
            RightShoulder.SetState(originator.RightShoulder);
        }

        public override XElement GetXml(Dictionary<string, string> files)
        {
            var xml = base.GetXml(files);

            xml.Add(new XElement("LeftShoulder", LeftShoulder.GetXml(files).Elements()));
            xml.Add(new XElement("RightShoulder", RightShoulder.GetXml(files).Elements()));

            return xml;
        }

        public override void SetXml(XElement xml, string path)
        {
            base.SetXml(xml, path);

            LeftShoulder = new ShoulderMemento();
            RightShoulder = new ShoulderMemento();
            LeftShoulder.SetXml(xml.Element("LeftShoulder"), path);
            RightShoulder.SetXml(xml.Element("RightShoulder"), path);
        }
    }
}
