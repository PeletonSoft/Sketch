using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Xml.Linq;
using PeletonSoft.Tools.Model.File;

namespace PeletonSoft.Tools.Model.Memento.Serialize
{
    public sealed class XmlDeserializer
    {
        public IDictionary<Type, XmlPrimitive> Primitives { get; }
        public Type[] Types { get; }
        public Func<string,Size,ImageBox> Resolver { get; }

        public XmlDeserializer(IDictionary<Type, XmlPrimitive> primitives,
            Type[] types, Func<string, Size, ImageBox> resolver)
        {
            Primitives = primitives;
            Types = types;
            Resolver = resolver;
        }

        public IDataTransfer Deserialize(XElement xml, Type target)
        {
            if (target.IsListDataTransfer())
            {
                return DeserializeList(xml, target);
            };

            var result = target.IsInterface
                ? Types
                    .Where(t => t.GetInterfaces().Contains(target) && t.IsClass)
                    .Select(Activator.CreateInstance)
                    .FirstOrDefault()
                : Activator.CreateInstance(target);

            foreach (var property in target.GetProperties())
            {
                var name = property.Name;
                var element = xml.Element(name);

                if (element == null)
                {
                    continue;
                }

                if (Primitives.ContainsKey(property.PropertyType))
                {
                    Primitives[property.PropertyType].Deserializer(property, result, element);
                }
                else if (property.PropertyType.IsEnum)
                {
                    property.SetValue(result, Enum.Parse(property.PropertyType, (string) element));
                }
                else if (property.PropertyType.GetInterfaces().Contains(typeof (IDataTransfer)))
                {
                    property.SetValue(result, Deserialize(element, property.PropertyType));
                }
                else if (property.PropertyType == typeof(ImageBox))
                {
                    Deserialize(property, result, element);
                }
            }
            return (IDataTransfer)result;
        }

        public void Deserialize(PropertyInfo property, object instance, XElement element)
        {
            var fileName = (string)element.Attribute("FileName");
            var width = (double)element.Attribute("Width");
            var height = (double)element.Attribute("Height");
            var size = new Size(width, height);
            var imageBox = Resolver(fileName, size);
            property.SetValue(instance, imageBox);
        }
        public IDataTransfer DeserializeList(XElement xml, Type target)
        {
            var genericType = typeof(ListDataTransfer<>).MakeGenericType(target.GetGenericArguments());
            var result = Activator.CreateInstance(genericType);
            var listProperty = genericType.GetProperty("List");
            var list = (IList)listProperty.GetValue(result);

            var contentType = typeof(TypeContentDataTransfer<>).MakeGenericType(target.GetGenericArguments());
            var records = xml.Elements("Item")
                .Select(
                el => new
                {
                    Type = (string)el.Element("Type"),
                    DataTransferType = (string)el.Element("DataTransferType"),
                    Content = el.Element("Content")
                })
                .Select(
                    rec => new
                    {
                        Instance = Activator.CreateInstance(contentType),
                        rec.Type,
                        rec.DataTransferType,
                        Content = Deserialize(rec.Content, Types.Single(t => t.Name == rec.DataTransferType))
                    })
                .ToList();
            records.ForEach(rec =>
            {
                contentType.GetProperty("Type").SetValue(rec.Instance, rec.Type);
                contentType.GetProperty("DataTransferType").SetValue(rec.Instance, rec.DataTransferType);
                contentType.GetProperty("Content").SetValue(rec.Instance, rec.Content);
                list.Add(rec.Instance);
            });
            return (IDataTransfer)result;
        }
    }
}
