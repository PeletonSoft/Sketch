using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using PeletonSoft.Tools.Model.File;

namespace PeletonSoft.Tools.Model.Memento
{
    public class Caretaker<T> where T:IOriginator
    {
        public IMemento<T> Memento { get; set; }

        public void GetState(T originator)
        {
            if (Memento != null)
            {
                Memento.GetState(originator);
            }
        }

        public void SetState(T originator)
        {
            if (Memento != null)
            {
                Memento.SetState(originator);
            }
        }

        public void Save(string path)
        {
            if (Memento == null)
            {
                return;
            }

            var files = Memento.GetFiles()
                .Select((fileBox, index) => new {FileBox = fileBox, Index = index + 1})
                .ToDictionary(
                    file => file.FileBox.GetFileName(file.Index.ToString(CultureInfo.InvariantCulture)),
                    file => file.FileBox);

            var xml = Memento.GetXml(files);
            xml.Save(Path.Combine(path,"content.xml"));

            foreach (var file in files)
            {
                file.Value.WriteToFile(Path.Combine(path, file.Key));
            }

        }
        public void Load(string path)
        {
            if (Memento == null)
            {
                return;
            }

            var xml = XElement.Load(Path.Combine(path, "content.xml"));
            Memento.SetXml(xml, path);

        }
    }
}
