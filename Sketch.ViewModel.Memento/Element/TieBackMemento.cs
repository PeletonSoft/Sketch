using System.Collections.Generic;
using System.Xml.Linq;
using PeletonSoft.Sketch.ViewMode.Memento.Element.Primitive;
using PeletonSoft.Sketch.ViewMode.Memento.Element.Service;
using PeletonSoft.Sketch.ViewModel.Element;
using PeletonSoft.Sketch.ViewModel.Factory;
using PeletonSoft.Sketch.ViewModel.Interface.Element;
using PeletonSoft.Tools.Model;
using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewMode.Memento.Element
{
    class TieBackMemento : IMemento<TieBackViewModel>, IMemento<IElementViewModel>
    {
        public string Description { get; set; }
        public bool Visibility { get; set; }
        public double Opacity { get; set; }
        public string Layout { get; set; }

        public double Length { get; set; }
        public double Depth { get; set; }
        public double DropHeight { get; set; }
        public ElementAlignment Alignment { get; set; }

        public double DenseWidth { get; set; }
        public double Protrusion { get; set; }
        public int WaveCount { get; set; }

        public double OffsetX { get; set; }
        public double OffsetY { get; set; }

        public TieBackSideMemento LeftSide { get; set; }
        public TieBackSideMemento RightSide { get; set; }


        public void GetState(TieBackViewModel originator)
        {
            LeftSide = new TieBackSideMemento();
            RightSide = new TieBackSideMemento();

            Description = originator.Description;

            Visibility = originator.Visibility;
            Opacity = originator.Opacity;

            DropHeight = originator.DropHeight;
            Length = originator.Length;
            OffsetY = originator.OffsetY;
            OffsetX = originator.OffsetX;
            Depth = originator.Depth;
            DenseWidth =  originator.DenseWidth;
            WaveCount = originator.WaveCount;
            Protrusion = originator.Protrusion;
            Alignment = originator.Alignment;

            LeftSide.GetState(originator.LeftSide);
            RightSide.GetState(originator.RightSide);
        }

        public void SetState(TieBackViewModel originator)
        {
            originator.RestoreDefault();

            originator.Description = Description;
            originator.Visibility = Visibility;
            originator.Opacity = Opacity;

            originator.DropHeight = DropHeight;
            originator.Length = Length;
            originator.OffsetY = OffsetY;
            originator.OffsetX = OffsetX;
            originator.Depth = Depth;
            originator.DenseWidth = DenseWidth;
            originator.Protrusion = Protrusion;
            originator.WaveCount = WaveCount;
            originator.Alignment = Alignment;

            LeftSide.SetState(originator.LeftSide);
            RightSide.SetState(originator.RightSide);
        }

        public void GetState(IElementViewModel originator)
        {
            GetState((TieBackViewModel)originator);
        }

        public void SetState(IElementViewModel originator)
        {
            SetState((TieBackViewModel)originator);
        }

        public IEnumerable<string> GetFiles()
        {
            return null;
        }

        public XElement GetXml(Dictionary<string, string> files)
        {
            return new XElement("root",
                new XElement("Description", Description),
                new XElement("Layout", Layout),
                new XElement("Opacity", Opacity),
                new XElement("Visibility", Visibility),
                new XElement("DropHeight", DropHeight),
                new XElement("Length", Length),
                new XElement("OffsetY", OffsetY),
                new XElement("OffsetX", OffsetX),
                new XElement("Depth", Depth),
                new XElement("DenseWidth", DenseWidth),
                new XElement("Protrusion", Protrusion),
                new XElement("WaveCount", WaveCount),
                new XElement("Alignment", Alignment.ToString()),
                new XElement("LeftSide", LeftSide.GetXml(files).Elements()),
                new XElement("RightSide", RightSide.GetXml(files).Elements()));
        }

        public void SetXml(XElement xml, string path)
        {
            LeftSide = new TieBackSideMemento();
            RightSide = new TieBackSideMemento();

            Layout = (string)xml.Element("Layout");
            Description = (string)xml.Element("Description");

            Visibility = (bool)xml.Element("Visibility");
            Opacity = (double)xml.Element("Opacity");

            DropHeight = (double)xml.Element("DropHeight");
            Length = (double)xml.Element("Length");
            OffsetY = (double)xml.Element("OffsetY");
            OffsetX = (double)xml.Element("OffsetX");
            Depth = (double)xml.Element("Depth");
            DenseWidth = (double)xml.Element("DenseWidth");
            Protrusion = (double)xml.Element("Protrusion");
            WaveCount = (int)xml.Element("WaveCount");
            Alignment = ((string) xml.Element("Alignment")).ToElementAlignment();

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
