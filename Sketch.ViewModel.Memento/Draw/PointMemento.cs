using System;
using System.Collections.Generic;
using System.Xml.Linq;
using PeletonSoft.Tools.Model.Draw;
using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewModel.Memento.Draw
{
    public class PointMemento : IMemento<IPointViewModel>
    {

        public double X { get; set; }
        public double Y { get; set; }
        public void GetState(IPointViewModel originator)
        {
            X = originator.X;
            Y = originator.Y;
        }

        public void SetState(IPointViewModel originator)
        {
            originator.RestoreDefault();
            originator.X = X;
            originator.Y = Y;

        }

        public IEnumerable<string> GetFiles()
        {
            return null;
        }

        public XElement GetXml(Dictionary<string, string> files)
        {
            throw new NotImplementedException();
        }

        public void SetXml(XElement xml, string path)
        {
            throw new NotImplementedException();
        }
    }
}
