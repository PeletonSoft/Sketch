using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using PeletonSoft.Sketch.ViewMode.Memento.Element.Custom;
using PeletonSoft.Sketch.ViewMode.Memento.Element.Service;
using PeletonSoft.Sketch.ViewMode.Memento.Geometry;
using PeletonSoft.Sketch.ViewModel.Element;
using PeletonSoft.Sketch.ViewModel.Factory;
using PeletonSoft.Sketch.ViewModel.Interface.Element;
using PeletonSoft.Tools.Model;
using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewMode.Memento.Element
{
    public sealed class ScanMemento : CustomElementMemento, IMemento<ScanViewModel>
    {
        public string FileName { get; set; }
        public RectangleMemento Rectangle { get; set; }
        public double ImagePixelWidth { get; set; }
        public double ImagePixelHeight { get; set; }
        protected override void GetState(IElementViewModel originator)
        {
            GetState((ScanViewModel)originator);
        }
        protected override void SetState(IElementViewModel originator)
        {
            SetState((ScanViewModel)originator);
        }

        public void GetState(ScanViewModel originator)
        {
            base.GetState(originator);

            Rectangle = new RectangleMemento();

            FileName = originator.FileName;
            Rectangle.GetState(originator.Rectangle);
            ImagePixelWidth = originator.ImageWidth;
            ImagePixelHeight = originator.ImageHeight;
        }

        public void SetState(ScanViewModel originator)
        {
            base.SetState(originator);

            originator.FileName = FileName;

            originator.ImageWidth = ImagePixelWidth;
            originator.ImageHeight = ImagePixelHeight;

            Rectangle.SetState(originator.Rectangle);
            originator.SaveRectange();

        }

        public override IEnumerable<string> GetFiles()
        {
            var filess = new[]
            {
                base.GetFiles(),
                Rectangle.GetFiles(),
                new[]{FileName}
            };
            return filess.GetFiles();
        }

        public override XElement GetXml(Dictionary<string, string> files)
        {
            var xml = base.GetXml(files);

            xml.Add(
                new XElement("ImageWidth", ImagePixelWidth),
                new XElement("ImageHeight", ImagePixelHeight),
                new XElement("FileName", files[FileName]),
                new XElement("Rectangle", Rectangle.GetXml(files).Elements())
                );

            return xml;
        }

        public override void SetXml(XElement xml, string path)
        {
            base.SetXml(xml, path);

            Rectangle = new RectangleMemento();
            FileName = null;

            ImagePixelWidth = (double)xml.Element("ImageWidth");
            ImagePixelHeight = (double) xml.Element("ImageHeight");

            var xFileName = xml.Element("FileName");
            if (xFileName != null)
            {
                var fileName = Path.Combine(path, (string) xFileName);
                FileName = fileName.GetTemporaryCopy();
            }

            var xRectangle = xml.Element("Rectangle");
            if (xRectangle != null)
            {
                Rectangle = new RectangleMemento();
                Rectangle.SetXml(xRectangle, path);
            }
        }
    }

    public sealed class ScanMementoRegister : IMementoRegister
    {
        public void Register()
        {

            var record = new ElementMementoFactoryRecord(
                typeof(ScanFactoryViewModel),
                typeof(ScanViewModel),
                typeof(ScanMemento),
                () => new ScanMemento());
            ElementMementoFactoryService.Register(record);
        }
    }
}
