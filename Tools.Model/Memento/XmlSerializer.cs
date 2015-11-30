using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Xml.Linq;
using PeletonSoft.Tools.Model.File;

namespace PeletonSoft.Tools.Model.Memento
{
    public sealed class XmlSerializer
    {
        private readonly IDictionary<string, ImageBox> _list = new Dictionary<string, ImageBox>();
        public IReadOnlyDictionary<string, ImageBox> List => new ReadOnlyDictionary<string, ImageBox>(_list);

        public IDataTransfer DataTransfer { get; }
        public XmlSerializer(IDataTransfer dataTransfer)
        {
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

            var isList = type.GetInterfaces()
                .Any(x =>
                    x.IsGenericType &&
                    x.GetGenericTypeDefinition() == typeof (IListDataTransfer<>));

            foreach (var property in type.GetProperties())
            {
                var name = property.Name;
                var value = property.GetValue(dataTransfer);

                if (value == null)
                {
                    continue;
                }

                if (name == "List" && isList)
                {
                    foreach (IDataTransfer rec in (IEnumerable)value)
                    {
                        xml.Add(new XElement("Item", Serialize(rec).Elements()));
                    }
                }
                else if (property.PropertyType == typeof (int))
                {
                    xml.Add(new XElement(name, (int)value));
                }
                else if (property.PropertyType == typeof (string))
                {
                    xml.Add(new XElement(name, (string)value));
                }
                else if (property.PropertyType == typeof(double))
                {
                    xml.Add(new XElement(name, (double)value));
                }
                else if (property.PropertyType == typeof(bool))
                {
                    xml.Add(new XElement(name, (bool)value));
                }
                else if (property.PropertyType == typeof(double?))
                {
                    xml.Add(new XElement(name, (double?)value));
                }
                else if (property.PropertyType.IsEnum)
                {
                    xml.Add(new XElement(name, value.ToString()));
                }
                else if (property.PropertyType == typeof(Point))
                {
                    var point = (Point) value;
                    xml.Add(new XElement(name,
                        new XAttribute(nameof(point.X), point.X),
                        new XAttribute(nameof(point.Y), point.Y)));
                }
                else if (property.PropertyType == typeof (List<Point>))
                {
                    var points = (List<Point>) value;
                    xml.Add(new XElement(name,
                        points.Select(
                            p => new XElement(
                                nameof(Point),
                                new XAttribute(nameof(p.X), p.X),
                                new XAttribute(nameof(p.Y), p.Y)))));
                }
                else if (property.PropertyType == typeof (ImageBox))
                {
                    var imageBox = (ImageBox) value;
                    var key = $"{_list.Count + 1}.{imageBox.Extention}";
                    _list.Add(key, imageBox);
                    xml.Add(new XElement(name,
                        new XAttribute(nameof(imageBox.Width), imageBox.Width),
                        new XAttribute(nameof(imageBox.Height), imageBox.Height),
                        new XAttribute("FileName", key)));
                }
                else if (value is IDataTransfer)
                {
                    xml.Add(new XElement(name, Serialize((IDataTransfer) value).Elements()));
                }
            }
            return xml;
        }
    }
}
