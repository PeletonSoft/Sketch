using System.Collections.Generic;
using System.Xml.Linq;
using PeletonSoft.Tools.Model.File;

namespace PeletonSoft.Tools.Model.Memento
{
    public interface IMemento<in T>  where T : IOriginator
    {
        void GetState(T originator);
        void SetState(T originator);

        IEnumerable<IFileBox> GetFiles();

        XElement GetXml(Dictionary<string,IFileBox> files);
        void SetXml(XElement xml, string path);
    }
}
