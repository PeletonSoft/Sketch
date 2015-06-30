using System.Collections.Generic;
using System.Xml.Linq;
using PeletonSoft.Sketch.ViewModel.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Interface.Element;
using PeletonSoft.Sketch.ViewModel.Interface.Layout;
using PeletonSoft.Sketch.ViewModel.Memento.Element.Primitive;
using PeletonSoft.Tools.Model.File;
using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewModel.Memento.Element.Custom
{
    public class CustomElementMemento : 
        IMemento<IElementViewModel>, 
        IMemento<IAlignableElementViewModel>,
        IMemento<AlignableElementViewModel>
    {
        public string Description { get; set; }
        public ClotheMemento Clothe { get; set; }
        public bool Visibility { get; set; }
        public double Opacity { get; set; }
        public string Layout { get; set; }

        public double OffsetY { get; set; }
        public double OffsetX { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }

        protected virtual void GetState(IElementViewModel originator)
        {
            ((IMemento<AlignableElementViewModel>)this).GetState((AlignableElementViewModel)originator);      
        }

        protected virtual void SetState(IElementViewModel originator)
        {
            ((IMemento<AlignableElementViewModel>)this).SetState((AlignableElementViewModel)originator);
        }

        void IMemento<IElementViewModel>.GetState(IElementViewModel originator)
        {
            GetState(originator);
        }

        void IMemento<IElementViewModel>.SetState(IElementViewModel originator)
        {
            SetState(originator);
        }

        void IMemento<IAlignableElementViewModel>.GetState(IAlignableElementViewModel originator)
        {
            GetState(originator);
        }

        void IMemento<IAlignableElementViewModel>.SetState(IAlignableElementViewModel originator)
        {
            GetState(originator);
        }

        void IMemento<AlignableElementViewModel>.GetState(AlignableElementViewModel originator)
        {
            Clothe = new ClotheMemento();

            Clothe.GetState(originator.Clothe);
            Description = originator.Description;

            Visibility = originator.Visibility;
            Opacity = originator.Opacity;

            Layout = originator.Layout.GetTypeName();

            Width = originator.Width;
            Height = originator.Height;
            OffsetX = originator.OffsetX;
            OffsetY = originator.OffsetY;
        }

        void IMemento<AlignableElementViewModel>.SetState(AlignableElementViewModel originator)
        {
            originator.RestoreDefault();

            Clothe.SetState(originator.Clothe);
            originator.Description = Description;

            originator.Visibility = Visibility;
            originator.Opacity = Opacity;

            originator.Layout = Layout.SetByTypeName<ILayoutViewModel>(originator.Layouts);
            
            originator.Width = Width;
            originator.Height = Height;
            originator.OffsetX = OffsetX;
            originator.OffsetY = OffsetY;
            
        }

        public virtual IEnumerable<IFileBox> GetFiles()
        {
            return null;
        }

        public virtual XElement GetXml(Dictionary<string, IFileBox> files)
        {
            return new XElement("root",
                new XElement("Clothe", Clothe.GetXml(files).Elements()),
                new XElement("Description", Description),
                new XElement("Layout", Layout),
                new XElement("Opacity", Opacity),
                new XElement("Visibility", Visibility),
                new XElement("Width", Width),
                new XElement("Height", Height),
                new XElement("OffsetX", OffsetX),
                new XElement("OffsetY", OffsetY)
                );
        }

        public virtual void SetXml(XElement xml, string path)
        {
            Clothe = new ClotheMemento();

            Layout = (string)xml.Element("Layout");
            Clothe.SetXml(xml.Element("Clothe"), path);
            Description = (string)xml.Element("Description");

            Visibility = (bool)xml.Element("Visibility");
            Opacity = (double)xml.Element("Opacity");
            
            Width = (double)xml.Element("Width");
            Height = (double)xml.Element("Height");
            OffsetX = (double)xml.Element("OffsetX");
            OffsetY = (double)xml.Element("OffsetY");
            
        }
    }
}
