using System.Collections.Generic;
using System.Xml.Linq;
using PeletonSoft.Sketch.ViewModel.Element;
using PeletonSoft.Sketch.ViewModel.Element.Primitive;
using PeletonSoft.Sketch.ViewModel.Factory;
using PeletonSoft.Sketch.ViewModel.Interface.Element;
using PeletonSoft.Sketch.ViewModel.Memento.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Memento.Element.Service;
using PeletonSoft.Tools.Model.File;
using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewModel.Memento.Element
{
    public sealed class ApplicationMemento : CustomElementMemento, 
        IMemento<ApplicationViewModel>
    {

        public double Thickness { get; set; }
        public string Transformation { get; set; }
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
            Transformation = originator.Transformation.GetType().Name;
            Outline = originator.Outline.GetType().Name;
        }

        public void SetState(ApplicationViewModel originator)
        {
            base.SetState(originator);

            originator.Thickness = Thickness;

            originator.Outline =
                Outline.SetByTypeName<OutlineViewModel>(originator.Outlines);
            originator.Transformation =
                Transformation.SetByTypeName<TransformationViewModel>(originator.Transformations);
        }
        public override XElement GetXml(Dictionary<string, IFileBox> files)
        {
            var xml = base.GetXml(files);

            xml.Add(
                new XElement("Thickness", Thickness),
                new XElement("Transformation", Transformation),
                new XElement("Outline", Outline)
                );

            return xml;
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
