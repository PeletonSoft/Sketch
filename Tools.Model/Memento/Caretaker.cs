using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using PeletonSoft.Tools.Model.File;

namespace PeletonSoft.Tools.Model.Memento
{
    public class Caretaker<T> : ICaretaker<T> where T : IOriginator
    {
        public IMemento<T> Memento { get; set; }

        public void GetState(T originator)
        {
            Memento?.GetState(originator);
        }

        public void SetState(T originator)
        {
            Memento?.SetState(originator);
        }

        public void Save(string path)
        {
            if (Memento == null)
            {
                return;
            }

            var dictionary = (Memento.GetFiles() ?? new List<IFileBox>())
                .Where(fileBox => fileBox != null)
                .Select((fileBox, index) => new {FileBox = fileBox, Index = index + 1})
                .ToDictionary(
                    file => file.FileBox.GetFileName(file.Index.ToString(CultureInfo.InvariantCulture)),
                    file => file.FileBox);

            var xml = Memento.GetXml(dictionary);
            xml.Save(Path.Combine(path, "content.xml"));

            foreach (var file in dictionary)
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
