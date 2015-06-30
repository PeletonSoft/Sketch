using System;
using System.Collections.Generic;
using System.Xml.Linq;
using PeletonSoft.Tools.Model.Draw;
using PeletonSoft.Tools.Model.File;
using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewModel.Memento.Draw
{
    public class LineMememto : IMemento<ILineViewModel>
    {
        public PointMemento Start { get; set; }
        public PointMemento Finish { get; set; }
        public void GetState(ILineViewModel originator)
        {
            Start = new PointMemento();
            Finish = new PointMemento();

            Start.GetState(originator.Start);
            Finish.GetState(originator.Finish);
        }

        public void SetState(ILineViewModel originator)
        {
            originator.RestoreDefault();

            Start.SetState(originator.Start);
            Finish.SetState(originator.Finish);
        }

        public IEnumerable<IFileBox> GetFiles()
        {
            return null;
        }

        public XElement GetXml(Dictionary<string, IFileBox> files)
        {
            throw new NotImplementedException();
        }

        public void SetXml(XElement xml, string path)
        {
            throw new NotImplementedException();
        }
    }
}
