using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Xml.Linq;
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

        private XElement Serialize(IDataTransfer dataTransfer)
        {
            var xml = new XElement("root");
            var type = dataTransfer.GetType();

            if (type.GetInterfaces().Any(t => t.IsListDataTransfer()))
            {
                return SerializeList(dataTransfer);
            }

            foreach (var property in type.GetProperties().OrderBy(t => t.Name))
            {
                var name = property.Name;
                var value = property.GetValue(dataTransfer);

                if (value == null)
                {
                    continue;
                }

                if (Primitives.ContainsKey(property.PropertyType))
                {
                    xml.Add(Primitives[property.PropertyType].Serializer(name, value));
                }
                else if (property.PropertyType.IsEnum)
                {
                    xml.Add(new XElement(name, value.ToString()));
                }
                else if (property.PropertyType == typeof (ImageBox))
                {
                    xml.Add(Serialize(name, (ImageBox)value));
                }
                else if (value is IDataTransfer)
                {
                    xml.Add(new XElement(name, Serialize((IDataTransfer) value).Elements()));
                }
            }
            return xml;
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
