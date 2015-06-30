using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using PeletonSoft.Sketch.ViewModel.Element;
using PeletonSoft.Sketch.ViewModel.Factory;
using PeletonSoft.Sketch.ViewModel.Interface.Element;
using PeletonSoft.Sketch.ViewModel.Memento.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Memento.Element.Service;
using PeletonSoft.Sketch.ViewModel.Memento.Geometry;
using PeletonSoft.Tools.Model.File;
using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewModel.Memento.Element
{
    public sealed class ScanMemento : CustomElementMemento, IMemento<ScanViewModel>
    {
        
        public RectangleMemento Rectangle { get; set; }
        public ImageBox ImageBox { get; set; }
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
            if (originator.ImageBox != null)
            {
                ImageBox = originator.ImageBox;
                Rectangle.GetState(originator.Rectangle);
            }
        }

        public void SetState(ScanViewModel originator)
        {
            base.SetState(originator);

            originator.ImageBox = ImageBox;
            Rectangle.SetState(originator.Rectangle);

        }

        public override IEnumerable<IFileBox> GetFiles()
        {
            var filess = new[]
            {
                base.GetFiles(),
                Rectangle.GetFiles(),
                new IFileBox[]{ImageBox}
            };
            return filess.GetFiles();
        }

        public override XElement GetXml(Dictionary<string, IFileBox> files)
        {

            var xml = base.GetXml(files);
            if (ImageBox != null)
            {
                xml.Add(
                new XElement("ImageWidth", ImageBox.Width),
                new XElement("ImageHeight", ImageBox.Height),
                new XElement("FileName", files.Single(x => x.Value.Data == ImageBox.Data).Key),
                new XElement("Rectangle", Rectangle.GetXml(files).Elements())
                );
            }
            return xml;
        }

        public override void SetXml(XElement xml, string path)
        {
            base.SetXml(xml, path);

            Rectangle = new RectangleMemento();

            var xFileName = xml.Element("FileName");
            if (xFileName != null)
            {
                var fileName = Path.Combine(path, (string) xFileName);
                if (Path.GetExtension(fileName).ToLower() == ".png")
                {
                    ImageBox = new PngImageBox(
                        File.ReadAllBytes(fileName),
                        (int) xml.Element("ImageWidth"),
                        (int) xml.Element("ImageHeight"));
                }
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
