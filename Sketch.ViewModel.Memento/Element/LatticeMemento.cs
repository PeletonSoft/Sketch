using System.Collections.Generic;
using System.Xml.Linq;
using PeletonSoft.Sketch.ViewModel.Element;
using PeletonSoft.Sketch.ViewModel.Factory;
using PeletonSoft.Sketch.ViewModel.Interface.Element;
using PeletonSoft.Sketch.ViewModel.Memento.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Memento.Element.Service;
using PeletonSoft.Tools.Model.File;
using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewModel.Memento.Element
{
    public class LatticeMemento : CustomElementMemento, IMemento<LatticeViewModel> 
    {
        public double CellHeight { get; set; }
        public double CellWidth { get; set; }

        protected override void GetState(IElementViewModel originator)
        {
            GetState((LatticeViewModel)originator);
        }

        protected override void SetState(IElementViewModel originator)
        {
            SetState((LatticeViewModel)originator);
        }
        public void GetState(LatticeViewModel originator)
        {
            base.GetState(originator);
            CellWidth = originator.CellWidth;
            CellHeight = originator.CellHeight;

        }
        public void SetState(LatticeViewModel originator)
        {
            base.SetState(originator);
            originator.CellWidth = CellWidth;
            originator.CellHeight = CellHeight;
        }

        public override XElement GetXml(Dictionary<string, IFileBox> files)
        {
            var xml = base.GetXml(files);
            xml.Add(
                new XElement("CellWidth",CellWidth),
                new XElement("CellHeight", CellHeight));
            return xml;
        }

        public override void SetXml(XElement xml, string path)
        {
            base.SetXml(xml, path);
            CellWidth = (double)xml.Element("CellWidth");
            CellHeight = (double)xml.Element("CellHeight");
        }
    }

    public sealed class LatticeMementoRegister : IMementoRegister
    {
        public void Register()
        {
            var record = new ElementMementoFactoryRecord(
                typeof(LatticeFactoryViewModel),
                typeof(LatticeViewModel),
                typeof(LatticeMemento),
                () => new LatticeMemento());
            ElementMementoFactoryService.Register(record);
        }

    }
}
