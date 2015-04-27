using System.Collections.Generic;
using System.Xml.Linq;

namespace PeletonSoft.Tools.Model.Memento
{
    public interface IMemento<in T>  where T : IOriginator
    {
        void GetState(T originator);
        void SetState(T originator);

        IEnumerable<string> GetFiles();

        XElement GetXml(Dictionary<string,string> files);
        void SetXml(XElement xml, string path);
    }
}
