using System.Collections.Generic;
using System.Xml.Linq;
using PeletonSoft.Sketch.ViewModel.Element;
using PeletonSoft.Sketch.ViewModel.Factory;
using PeletonSoft.Sketch.ViewModel.Interface.Element;
using PeletonSoft.Sketch.ViewModel.Memento.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Memento.Element.Primitive;
using PeletonSoft.Sketch.ViewModel.Memento.Element.Service;
using PeletonSoft.Tools.Model.File;
using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewModel.Memento.Element
{
    class TieBackMemento : PleatableMemento, IMemento<TieBackViewModel>
    {
        public double Length { get; set; }
        public double Depth { get; set; }
        public double DropHeight { get; set; }
        public double Protrusion { get; set; }
        public double OffsetX { get; set; }
        public double OffsetY { get; set; }

        public TieBackSideMemento LeftSide { get; set; }
        public TieBackSideMemento RightSide { get; set; }


        protected override void GetState(IElementViewModel originator)
        {
            GetState((TieBackViewModel)originator);
        }

        protected override void SetState(IElementViewModel originator)
        {
            SetState((TieBackViewModel)originator);
        }

        public void GetState(TieBackViewModel originator)
        {
            base.GetState(originator);

            LeftSide = new TieBackSideMemento();
            RightSide = new TieBackSideMemento();

            DropHeight = originator.DropHeight;
            Length = originator.Length;
            OffsetY = originator.OffsetY;
            OffsetX = originator.OffsetX;
            Depth = originator.Depth;
            Protrusion = originator.Protrusion;

            LeftSide.GetState(originator.LeftSide);
            RightSide.GetState(originator.RightSide);
        }

        public void SetState(TieBackViewModel originator)
        {
            base.SetState(originator);

            originator.DropHeight = DropHeight;
            originator.Length = Length;
            originator.OffsetY = OffsetY;
            originator.OffsetX = OffsetX;
            originator.Depth = Depth;
            originator.Protrusion = Protrusion;

            LeftSide.SetState(originator.LeftSide);
            RightSide.SetState(originator.RightSide);
        }

        public override XElement GetXml(Dictionary<string, IFileBox> files)
        {
            var xml = base.GetXml(files);
            xml.Add(
                new XElement("DropHeight", DropHeight),
                new XElement("Length", Length),
                new XElement("OffsetY", OffsetY),
                new XElement("OffsetX", OffsetX),
                new XElement("Depth", Depth),
                new XElement("Protrusion", Protrusion),
                new XElement("LeftSide", LeftSide.GetXml(files).Elements()),
                new XElement("RightSide", RightSide.GetXml(files).Elements()));
            return xml;
        }

        public override void SetXml(XElement xml, string path)
        {
            base.SetXml(xml, path);
            LeftSide = new TieBackSideMemento();
            RightSide = new TieBackSideMemento();

            DropHeight = (double)xml.Element("DropHeight");
            Length = (double)xml.Element("Length");
            OffsetY = (double)xml.Element("OffsetY");
            OffsetX = (double)xml.Element("OffsetX");
            Depth = (double)xml.Element("Depth");
            Protrusion = (double)xml.Element("Protrusion");

            LeftSide.SetXml(xml.Element("LeftSide"), path);
            RightSide.SetXml(xml.Element("RightSide"), path);
        }
    }

    public sealed class TieBackMementoRegister : IMementoRegister
    {
        public void Register()
        {
            var record = new ElementMementoFactoryRecord(
                typeof(TieBackFactoryViewModel),
                typeof(TieBackViewModel),
                typeof(TieBackMemento),
                () => new TieBackMemento());
            ElementMementoFactoryService.Register(record);
        }
    }

}
