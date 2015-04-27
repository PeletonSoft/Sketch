using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Linq;

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
                .Select((file, index) => new {File = file, Index = index + 1})
                .ToDictionary(
                    x => x.File,
                    x => x.Index.ToString(CultureInfo.InvariantCulture) + Path.GetExtension(x.File)
                );

            var xml = Memento.GetXml(files);
            xml.Save(Path.Combine(path,"content.xml"));

            foreach (var file in files)
            {
                File.Copy(file.Key, Path.Combine(path, file.Value), true);
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
