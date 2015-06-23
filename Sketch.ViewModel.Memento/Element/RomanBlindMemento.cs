using System.Collections.Generic;
using System.Xml.Linq;
using PeletonSoft.Sketch.ViewModel.Element;
using PeletonSoft.Sketch.ViewModel.Factory;
using PeletonSoft.Sketch.ViewModel.Interface.Element;
using PeletonSoft.Sketch.ViewModel.Memento.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Memento.Element.Primitive;
using PeletonSoft.Sketch.ViewModel.Memento.Element.Service;
using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewModel.Memento.Element
{
    public class RomanBlindMemento : CustomElementMemento, IMemento<RomanBlindViewModel>
    {
        public DecorativeBorderMemento DecorativeBorder { get; set; }
        public int WaveCount { get; set; }
        public double DenseHeight { get; set; }
        public double CoulisseThickness{ get; set; }

        protected override void GetState(IElementViewModel originator)
        {
            GetState((RomanBlindViewModel)originator);
        }

        protected override void SetState(IElementViewModel originator)
        {
            SetState((RomanBlindViewModel)originator);
        }
        public void GetState(RomanBlindViewModel originator)
        {
            base.GetState(originator);
            DecorativeBorder = new DecorativeBorderMemento();
            DecorativeBorder.GetState(originator.DecorativeBorder);
            WaveCount = originator.WaveCount;
            DenseHeight = originator.DenseHeight;
            CoulisseThickness = originator.CoulisseThickness;
        }

        public void SetState(RomanBlindViewModel originator)
        {
            base.SetState(originator);
            originator.WaveCount = WaveCount;
            originator.DenseHeight = DenseHeight;
            originator.CoulisseThickness = CoulisseThickness;
            DecorativeBorder.SetState(originator.DecorativeBorder);
        }

        public override XElement GetXml(Dictionary<string, string> files)
        {
            var xml = base.GetXml(files);
            xml.Add(
                new XElement("DecorativeBorder", DecorativeBorder.GetXml(files).Elements()),
                new XElement("DenseHeight", DenseHeight),
                new XElement("WaveCount", WaveCount),
                new XElement("CoulisseThickness", CoulisseThickness)
                );

            return xml;
        }

        public override void SetXml(XElement xml, string path)
        {
            base.SetXml(xml, path);
            DecorativeBorder = new DecorativeBorderMemento();
            DenseHeight = (double)xml.Element("DenseHeight");
            WaveCount = (int)xml.Element("WaveCount");
            CoulisseThickness = (double)xml.Element("CoulisseThickness");
            DecorativeBorder.SetXml(xml.Element("DecorativeBorder"), path);
        }
    }

    public sealed class RomanBlindMementoRegister : IMementoRegister
    {
        public void Register()
        {
            var record = new ElementMementoFactoryRecord(
                typeof(RomanBlindFactoryViewModel),
                typeof(RomanBlindViewModel),
                typeof(RomanBlindMemento),
                () => new RomanBlindMemento());
            ElementMementoFactoryService.Register(record);
        }
    }
}
