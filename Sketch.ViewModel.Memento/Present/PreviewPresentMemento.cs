using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Sketch.ViewModel.Memento.Geometry;
using PeletonSoft.Sketch.ViewModel.Memento.Service;
using PeletonSoft.Sketch.ViewModel.Present;
using PeletonSoft.Tools.Model.File;
using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewModel.Memento.Present
{
    public sealed class PreviewPresentMemento : PresentMemento, IMemento<PreviewPresentViewModel>
    {
        public ImageBox ImageBox { get; set; }
        public RectangleMemento Quadrangle { get; set; }
        public SuperimposeOptionMemento SuperimposeOption { get; set; }
        public override void GetState(IPresentViewModel originator)
        {
            GetState((PreviewPresentViewModel) originator);
        }

        public override void SetState(IPresentViewModel originator)
        {
            SetState((PreviewPresentViewModel) originator);
        }

        public void GetState(PreviewPresentViewModel originator)
        {
            base.GetState(originator);
            SuperimposeOption = new SuperimposeOptionMemento();

            if (originator.ImageBox != null)
            {
                ImageBox = originator.ImageBox;
                Quadrangle = new RectangleMemento();
                Quadrangle.GetState(originator.Quadrangle);
            }
            SuperimposeOption.GetState(originator.SuperimposeOption);
        }

        public void SetState(PreviewPresentViewModel originator)
        {
            base.SetState(originator);

            if (ImageBox != null)
            {
                originator.ImageBox = ImageBox;
                Quadrangle.SetState(originator.Quadrangle);
            }
            SuperimposeOption.SetState(originator.SuperimposeOption);
        }

        public override IEnumerable<IFileBox> GetFiles()
        {
            var filess = new[]
            {
                base.GetFiles(), new[] {ImageBox}
            };
            return filess.GetFiles();
        }

        public override XElement GetXml(Dictionary<string, IFileBox> files)
        {
            var xml = base.GetXml(files);

            xml.Add(new XElement("SuperimposeOption", SuperimposeOption.GetXml(files).Elements()));
            if (ImageBox != null)
            {
                xml.Add(
                    new XElement("ImageWidth", ImageBox.Width),
                    new XElement("FileName", files.Single(x => x.Value.Data == ImageBox.Data).Key),
                    new XElement("ImageHeight", ImageBox.Height),
                    new XElement("Quadrangle", Quadrangle.GetXml(files).Elements()));
            }

            if (Quadrangle != null)
            {

            }
            return xml;
        }

        public override void SetXml(XElement xml, string path)
        {
            base.SetXml(xml, path);
            SuperimposeOption = new SuperimposeOptionMemento {ForegroundOpacity = 0.9};

            ImageBox = null;
            Quadrangle = null;

            var xFileName = xml.Element("FileName");
            if (xFileName != null)
            {
                var fileName = Path.Combine(path, (string) xFileName);
                try
                {
                    if (Path.GetExtension(fileName).ToLower() == ".png")
                    {
                        ImageBox = new PngImageBox(
                            File.ReadAllBytes(fileName),
                            (int)xml.Element("ImageWidth"),
                            (int)xml.Element("ImageHeight"));
                        var xQuadrangle = xml.Element("Quadrangle");
                        Quadrangle = new RectangleMemento();
                        Quadrangle.SetXml(xQuadrangle, path);
                    }
                }
                catch
                {
                }
            }
            var superimposeOption = xml.Element("SuperimposeOption");
            if (superimposeOption != null)
            {
                SuperimposeOption.SetXml(superimposeOption, path);
            }
        }

    }

    public sealed class PreviewPresentMementoRegister : IMementoRegister
    {
        public void Register()
        {
            var service = new PresentMementoService();
            service.Register(
                typeof(PreviewPresentViewModel),
                () => new PreviewPresentMemento());
        }
    }
}
