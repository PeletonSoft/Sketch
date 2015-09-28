using System.Collections.Generic;
using System.Xml.Linq;
using PeletonSoft.Sketch.ViewModel.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Interface.Element;
using PeletonSoft.Tools.Model;
using PeletonSoft.Tools.Model.File;
using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewModel.Memento.Element.Custom
{
    public class PleatableMemento : IMemento<IElementViewModel>, IMemento<PleatableViewModel>
    {
        public string Description { get; set; }
        public bool Visibility { get; set; }
        public double Opacity { get; set; }

        public double DenseWidth { get; set; }
        public int WaveCount { get; set; }
        public ElementAlignment Alignment { get; set; }

        protected virtual void GetState(IElementViewModel originator)
        {
            ((IMemento<PleatableViewModel>)this).GetState((PleatableViewModel)originator);
        }

        protected virtual void SetState(IElementViewModel originator)
        {
            ((IMemento<PleatableViewModel>)this).SetState((PleatableViewModel)originator);
        }

        void IMemento<PleatableViewModel>.GetState(PleatableViewModel originator)
        {
            Description = originator.Description;

            Visibility = originator.Visibility;
            Opacity = originator.Opacity;

            DenseWidth = originator.DenseWidth;
            WaveCount = originator.WaveCount;
            Alignment = originator.Alignment;
        }

        void IMemento<PleatableViewModel>.SetState(PleatableViewModel originator)
        {
            originator.RestoreDefault();

            originator.Description = Description;
            originator.Visibility = Visibility;
            originator.Opacity = Opacity;

            originator.DenseWidth = DenseWidth;
            originator.WaveCount = WaveCount;
            originator.Alignment = Alignment;
        }

        void IMemento<IElementViewModel>.GetState(IElementViewModel originator)
        {
            GetState(originator);
        }

        void IMemento<IElementViewModel>.SetState(IElementViewModel originator)
        {
            SetState(originator);
        }

        public virtual IEnumerable<IFileBox> GetFiles()
        {
            return null;
        }

        public virtual XElement GetXml(Dictionary<string, IFileBox> files)
        {
            return new XElement("root",
                new XElement("Description", Description),
                new XElement("Opacity", Opacity),
                new XElement("Visibility", Visibility),
                new XElement("DenseWidth", DenseWidth),
                new XElement("WaveCount", WaveCount),
                new XElement("Alignment", Alignment.ToString()));
        }

        public virtual void SetXml(XElement xml, string path)
        {
            Description = (string)xml.Element("Description");
            Visibility = (bool)xml.Element("Visibility");
            Opacity = (double)xml.Element("Opacity");

            DenseWidth = (double)xml.Element("DenseWidth");
            WaveCount = (int)xml.Element("WaveCount");
            Alignment = ((string)xml.Element("Alignment")).ToElementAlignment();
        }

    }
}
