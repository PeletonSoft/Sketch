using System.Collections.Generic;
using System.Xml.Linq;
using PeletonSoft.Sketch.ViewModel.Element;
using PeletonSoft.Sketch.ViewModel.Factory;
using PeletonSoft.Sketch.ViewModel.Interface.Element;
using PeletonSoft.Sketch.ViewModel.Memento.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Memento.Element.Service;
using PeletonSoft.Tools.Model;
using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewModel.Memento.Element
{
    public sealed class DeJabotMemento : CustomElementMemento, IMemento<DeJabotViewModel>
    {
        public double SmallHeight { get; set; }
        public int WaveCount { get; set; }
        public double WaveHeight { get; set; }
        public ElementAlignment Alignment { get; set; }
        public ElementAlignment WaveAlignment { get; set; }

        protected override void GetState(IElementViewModel originator)
        {
            GetState((DeJabotViewModel)originator);
        }
        protected override void SetState(IElementViewModel originator)
        {
            SetState((DeJabotViewModel)originator);
        }
        public void GetState(DeJabotViewModel originator)
        {
            base.GetState(originator);

            SmallHeight = originator.SmallHeight;
            WaveCount = originator.WaveCount;
            WaveHeight = originator.WaveHeight;
            Alignment = originator.Alignment;
            WaveAlignment = originator.WaveAlignment;

        }
        public void SetState(DeJabotViewModel originator)
        {
            base.SetState(originator);

            originator.Alignment = Alignment;
            originator.WaveAlignment = WaveAlignment;
            originator.SmallHeight = SmallHeight;
            originator.WaveCount = WaveCount;
            originator.WaveHeight = WaveHeight;

        }

        public override XElement GetXml(Dictionary<string, string> files)
        {
            var xml = base.GetXml(files);

            xml.Add(
                new XElement("SmallHeight", SmallHeight),
                new XElement("WaveCount", WaveCount),
                new XElement("WaveHeight", WaveHeight),
                new XElement("Alignment", Alignment.ToString()),
                new XElement("WaveAlignment", WaveAlignment.ToString())
                );

            return xml;
        }

        public override void SetXml(XElement xml, string path)
        {
            base.SetXml(xml, path);

            SmallHeight = (double)xml.Element("SmallHeight");
            WaveCount = (int)xml.Element("WaveCount");
            WaveHeight = (double)xml.Element("WaveHeight");
            Alignment = ((string)xml.Element("Alignment")).ToElementAlignment();
            WaveAlignment = ((string)xml.Element("WaveAlignment")).ToElementAlignment();
        }

    }

    public sealed class DeJabotMementoRegister : IMementoRegister
    {
        public void Register()
        {
            var record = new ElementMementoFactoryRecord(
                typeof(DeJabotFactoryViewModel),
                typeof(DeJabotViewModel),
                typeof(DeJabotMemento),
                () => new DeJabotMemento());
            ElementMementoFactoryService.Register(record);
        }
    }
}
