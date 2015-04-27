using System.Collections.Generic;
using System.Xml.Linq;
using PeletonSoft.Sketch.ViewMode.Memento.Element.Service;
using PeletonSoft.Sketch.ViewModel.Element;
using PeletonSoft.Sketch.ViewModel.Factory;
using PeletonSoft.Sketch.ViewModel.Interface.Element;
using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewMode.Memento.Element
{
    public sealed class OverlayMemento : IMemento<OverlayViewModel>, IMemento<IElementViewModel>
    {
        public int SelectedIndex { get; set; }
        public string Description { get; set; }
        public double OverOffsetY { get; set; }
        public double OverOffsetX { get; set; }
        public double OverHeight { get; set; }
        public double OverWidth { get; set; }
        public void GetState(OverlayViewModel originator)
        {
            SelectedIndex = originator.SelectedIndex;
            Description = originator.Description;
            OverWidth = originator.OverWidth;
            OverHeight = originator.OverHeight;
            OverOffsetX = originator.OverOffsetX;
            OverOffsetY = originator.OverOffsetY;
        }



        public void SetState(OverlayViewModel originator)
        {
            originator.SelectedIndex = SelectedIndex;
            originator.Description = Description;

            originator.OverWidth = OverWidth;
            originator.OverHeight = OverHeight;
            originator.OverOffsetX = OverOffsetX;
            originator.OverOffsetY = OverOffsetY;
        }

        public void GetState(IElementViewModel originator)
        {
            GetState((OverlayViewModel)originator);
        }

        public void SetState(IElementViewModel originator)
        {
            SetState((OverlayViewModel)originator);
        }

        public IEnumerable<string> GetFiles()
        {
            return null;
        }

        public XElement GetXml(Dictionary<string, string> files)
        {
            return new XElement("root",
                new XElement("Description", Description),
                new XElement("SelectedIndex", SelectedIndex),
                new XElement("OverWidth", OverWidth),
                new XElement("OverHeight", OverHeight),
                new XElement("OverOffsetX", OverOffsetX),
                new XElement("OverOffsetY", OverOffsetY)
                );
        }

        public void SetXml(XElement xml, string path)
        {
            Description = (string)xml.Element("Description");
            SelectedIndex = (int)xml.Element("SelectedIndex");
            OverWidth = (double)xml.Element("OverWidth");
            OverHeight = (double)xml.Element("OverHeight");
            OverOffsetX = (double)xml.Element("OverOffsetX");
            OverOffsetY = (double)xml.Element("OverOffsetY");
        }
    }

    public sealed class OverlayMementoRegister : IMementoRegister
    {
        public void Register()
        {
            
            var record = new ElementMementoFactoryRecord(
                typeof(OverlayFactoryViewModel),
                typeof(OverlayViewModel),
                typeof(OverlayMemento),
                () => new OverlayMemento());
            ElementMementoFactoryService.Register(record);
            
        }
    }
}
