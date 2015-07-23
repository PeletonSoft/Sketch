using System.Collections.Generic;
using System.Xml.Linq;
using PeletonSoft.Sketch.ViewModel.Element;
using PeletonSoft.Sketch.ViewModel.Factory;
using PeletonSoft.Sketch.ViewModel.Interface.Element;
using PeletonSoft.Sketch.ViewModel.Memento.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Memento.Element.Service;
using PeletonSoft.Tools.Model.Collection;
using PeletonSoft.Tools.Model.File;
using PeletonSoft.Tools.Model.Memento;
using PeletonSoft.Tools.Model.Memento.Container;

namespace PeletonSoft.Sketch.ViewModel.Memento.Element
{
    public sealed class ApplicationMemento : CustomElementMemento, 
        IMemento<ApplicationViewModel>
    {

        public double Thickness { get; set; }
        public string Reflection { get; set; }
        public string Outline { get; set; }

        protected override void GetState(IElementViewModel originator)
        {
            GetState((ApplicationViewModel)originator);

        }
        protected override void SetState(IElementViewModel originator)
        {
            SetState((ApplicationViewModel)originator);
        }

        public void GetState(ApplicationViewModel originator)
        {
            base.GetState(originator);

            Thickness = originator.Thickness;
            Reflection = originator.Reflections.GetKeyByValue(originator.Reflection);
            Outline = originator.Outlines.GetKeyByValue(originator.Outline);
        }

        public void SetState(ApplicationViewModel originator)
        {
            base.SetState(originator);

            originator.Thickness = Thickness;

            originator.Outline = originator.Outlines.GetValueByKeyOrDefault(Outline);
            originator.Reflection = originator.Reflections.GetValueByKeyOrDefault(Reflection);
        }
        public override XElement GetXml(Dictionary<string, IFileBox> files)
        {
            var xml = base.GetXml(files);

            xml.Add(
                new XElement("Thickness", Thickness),
                new XElement("Transformation", Reflection),
                new XElement("Outline", Outline)
                );

            return xml;
        }

        public override void SetXml(XElement xml, string path)
        {
            base.SetXml(xml, path);

            Thickness = (double)xml.Element("Thickness");
            Reflection = ((string)xml.Element("Transformation"));
            Outline = ((string) xml.Element("Outline"));
        }

    }

    public sealed class ApplicationMementoRegister : IMementoRegister
    {
        public void Register()
        {
            var record = new ElementMementoFactoryRecord(
                typeof(ApplicationFactoryViewModel),
                typeof(ApplicationViewModel),
                typeof(ApplicationMemento),
                () => new ApplicationMemento());
            ElementMementoFactoryService.Register(record);
        }
    }
}
