using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using PeletonSoft.Sketch.ViewMode.Memento.Geometry;
using PeletonSoft.Sketch.ViewMode.Memento.Service;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Sketch.ViewModel.Present;
using PeletonSoft.Tools.Model;
using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewMode.Memento.Present
{
    public sealed class PreviewPresentMemento : PresentMemento, IMemento<PreviewPresentViewModel>
    {
        public string FileName { get; set; }
        public RectangleMemento Quadrangle { get; set; }

        public override void GetState(IPresentViewModel originator)
        {
            GetState((PreviewPresentViewModel)originator);
        }

        public override void SetState(IPresentViewModel originator)
        {
            SetState((PreviewPresentViewModel)originator);
        }

        public void GetState(PreviewPresentViewModel originator)
        {
            base.GetState(originator);

            FileName = originator.FileName;
            if (originator.Quadrangle != null)
            {
                Quadrangle = new RectangleMemento();
                Quadrangle.GetState(originator.Quadrangle);
            }
        }

        public void SetState(PreviewPresentViewModel originator)
        {
            base.SetState(originator);

            originator.FileName = FileName;
            if (Quadrangle != null)
            {
                Quadrangle.SetState(originator.Quadrangle);
            }

        }

        public override IEnumerable<string> GetFiles()
        {
            var filess = new[]
            {
                base.GetFiles(),
                new[] {FileName}
            };
            return filess.GetFiles();
        }

        public override XElement GetXml(Dictionary<string, string> files)
        {
            var xml = base.GetXml(files);

            if (FileName != null)
            {
                xml.Add(new XElement("FileName",files[FileName]));
            }

            if (Quadrangle != null)
            {
                xml.Add(new XElement("Quadrangle",Quadrangle.GetXml(files).Elements()));
            }
            return xml;
        }

        public override void SetXml(XElement xml, string path)
        {
            base.SetXml(xml, path);

            FileName = null;
            Quadrangle = null;

            var xFileName = xml.Element("FileName");
            if (xFileName != null)
            {
                var fileName = Path.Combine(path, (string)xFileName);
                FileName = fileName.GetTemporaryCopy();
            }

            var xQuadrangle = xml.Element("Quadrangle");
            if (xQuadrangle != null)
            {
                Quadrangle = new RectangleMemento();
                Quadrangle.SetXml(xQuadrangle,path);
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
