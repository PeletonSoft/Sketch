using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;
using PeletonSoft.Tools.Model.Collection;
using PeletonSoft.Tools.Model.File;

namespace PeletonSoft.Tools.Model.Memento.Serialize
{
    public sealed class XmlSerializer
    {
        public IDictionary<Type, XmlPrimitive> Primitives { get; }
        private readonly IDictionary<string, ImageBox> _list = new Dictionary<string, ImageBox>();
        public IReadOnlyDictionary<string, ImageBox> List => new ReadOnlyDictionary<string, ImageBox>(_list);

        public IDataTransfer DataTransfer { get; }
        public XmlSerializer(IDictionary<Type, XmlPrimitive> primitives, IDataTransfer dataTransfer)
        {
            Primitives = primitives;
            DataTransfer = dataTransfer;
        }
        public XElement Serialize()
        {
            _list.Clear();
            return Serialize(DataTransfer);
        }

        private class SerializeRecord
        {
            public Type Type { get; set; }
            public string Name { get; set; }
            public object Value { get; set; }
        }

        private XElement Serialize(IDataTransfer dataTransfer)
        {
            var type = dataTransfer.GetType();

            if (type.GetInterfaces().Any(t => t.IsListDataTransfer()))
            {
                return SerializeList(dataTransfer);
            }

            return new XElement("root",
                type.GetProperties()
                    .OrderBy(t => t.Name)
                    .Select(
                        p =>
                            new SerializeRecord {Type = p.PropertyType, Name = p.Name, Value = p.GetValue(dataTransfer)})
                    .Where(rec => rec.Value != null)
                    .FilteredSelect(new Dictionary<Func<SerializeRecord, bool>, Func<SerializeRecord, XElement>>()
                    {
                        [rec => Primitives.ContainsKey(rec.Type)] =
                            rec => Primitives[rec.Type].Serializer(rec.Name, rec.Value),
                        [rec => rec.Type.IsEnum] =
                            rec => new XElement(rec.Name, rec.Value.ToString()),
                        [rec => rec.Type == typeof (ImageBox)] =
                            rec => Serialize(rec.Name, (ImageBox) rec.Value),
                        [rec => rec.Value is IDataTransfer] =
                            rec => new XElement(rec.Name, Serialize((IDataTransfer) rec.Value).Elements())
                    }));
        }

        private XElement Serialize(string name, ImageBox imageBox)
        {
            var key = $"{_list.Count + 1}.{imageBox.Extention}";
            _list.Add(key, imageBox);
            return new XElement(name,
                new XAttribute(nameof(imageBox.Width), imageBox.Width),
                new XAttribute(nameof(imageBox.Height), imageBox.Height),
                new XAttribute("FileName", key));
        }
        private XElement SerializeList(IDataTransfer dataTransfer)
        {
            var xml = new XElement("root");
            var property = dataTransfer.GetType().GetProperty("List");
            var value = property.GetValue(dataTransfer);
            foreach (IDataTransfer rec in (IEnumerable)value)
            {
                xml.Add(new XElement("Item", Serialize(rec).Elements()));
            }
            return xml;
        }
    }
}
