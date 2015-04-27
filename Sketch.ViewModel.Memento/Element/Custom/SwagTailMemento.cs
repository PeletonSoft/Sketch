using System.Collections.Generic;
using System.Xml.Linq;
using PeletonSoft.Sketch.ViewModel.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Interface.Element;

namespace PeletonSoft.Sketch.ViewMode.Memento.Element.Custom
{
    public abstract class SwagTailMemento : CustomElementMemento
    {
        public double IndentDepth { get; set; }
        public int WaveCount { get; set; }
        protected override void GetState(IElementViewModel originator)
        {
            GetState((SwagTailViewModel)originator);
        }

        protected override void SetState(IElementViewModel originator)
        {
            SetState((SwagTailViewModel)originator);
        }

        private void GetState(SwagTailViewModel originator)
        {
            base.GetState(originator);

            IndentDepth = originator.IndentDepth;
            WaveCount = originator.WaveCount;

        }

        private void SetState(SwagTailViewModel originator)
        {
            base.SetState(originator);

            originator.IndentDepth = IndentDepth;
            originator.WaveCount = WaveCount;
        }

        public override XElement GetXml(Dictionary<string, string> files)
        {
            var xml = base.GetXml(files);

            xml.Add(
                new XElement("IndentDepth", IndentDepth),
                new XElement("WaveCount", WaveCount)
                );

            return xml;
        }

        public override void SetXml(XElement xml, string path)
        {
            base.SetXml(xml, path);
            IndentDepth = (double) xml.Element("IndentDepth");
            WaveCount = (int)xml.Element("WaveCount");
        }
    }
}
