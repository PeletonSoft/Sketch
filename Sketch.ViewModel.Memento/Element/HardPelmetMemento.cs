using System.Collections.Generic;
using System.Windows;
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
    public sealed class HardPelmetMemento : CustomElementMemento, IMemento<HardPelmetViewModel>
    {
        public DecorativeBorderMemento DecorativeBorder { get; set; }

        protected override void GetState(IElementViewModel originator)
        {
            GetState((HardPelmetViewModel)originator);
        }

        protected override void SetState(IElementViewModel originator)
        {
            SetState((HardPelmetViewModel)originator);
        }

        public void GetState(HardPelmetViewModel originator)
        {
            base.GetState(originator);
            DecorativeBorder = new DecorativeBorderMemento();
            DecorativeBorder.GetState(originator.DecorativeBorder);

        }

        public void SetState(HardPelmetViewModel originator)
        {
            base.SetState(originator);

            DecorativeBorder.SetState(originator.DecorativeBorder);

        }

        public override XElement GetXml(Dictionary<string, IFileBox> files)
        {
            var xml = base.GetXml(files);
            xml.Add(new XElement("DecorativeBorder", DecorativeBorder.GetXml(files).Elements()));
            return xml;
        }

        public override void SetXml(XElement xml, string path)
        {
            base.SetXml(xml, path);
            DecorativeBorder = new DecorativeBorderMemento();
            DecorativeBorder.SetXml(xml.Element("DecorativeBorder"), path);
        }
    }

    public sealed class HardPelmetMementoRegister : IMementoRegister
    {
        public  void Register()
        {
            var record = new ElementMementoFactoryRecord(
                typeof(HardPelmetFactoryViewModel),
                typeof(HardPelmetViewModel),
                typeof(HardPelmetMemento),
                () => new HardPelmetMemento());
            ElementMementoFactoryService.Register(record);
        }

    }
}
