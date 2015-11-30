using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Xml.Linq;
using PeletonSoft.Tools.Model.File;

namespace PeletonSoft.Tools.Model.Memento
{
    public sealed class XmlDeserializer
    {
        public Type[] Types { get; }
        public Func<string,Size,ImageBox> Resolver { get; }

        public XmlDeserializer(Type[] types, Func<string, Size, ImageBox> resolver)
        {
            Types = types;
            Resolver = resolver;
        }

        public IDataTransfer Deserialize(XElement xml, Type target)
        {
            var isList = target.IsGenericType && target.GetGenericTypeDefinition() == typeof (IListDataTransfer<>);

            object result = null;

            if (!isList)
            {
                if (target.IsInterface)
                {
                    result = Types
                        .Where(t => t.GetInterfaces().Contains(target) && t.IsClass)
                        .Select(Activator.CreateInstance)
                        .FirstOrDefault(); 
                }
                else
                {
                    result = Activator.CreateInstance(target);
                }
            }

            foreach (var property in target.GetProperties())
            {
                var name = property.Name;
                var element = isList ? xml : xml.Element(name);

                if (element == null)
                {
                    continue;
                }

                if (property.PropertyType == typeof (int))
                {
                    property.SetValue(result, (int) element);
                }
                else if (property.PropertyType == typeof (string))
                {
                    property.SetValue(result, (string) element);
                }
                else if (property.PropertyType == typeof (double))
                {
                    property.SetValue(result, (double) element);
                }
                else if (property.PropertyType == typeof (bool))
                {
                    property.SetValue(result, (bool) element);
                }
                else if (property.PropertyType == typeof (double?))
                {
                    property.SetValue(result, (double?) element);
                }
                else if (property.PropertyType.IsEnum)
                {
                    property.SetValue(result, Enum.Parse(property.PropertyType, (string) element));
                }
                else if (property.PropertyType.GetInterfaces().Contains(typeof (IDataTransfer)))
                {
                    property.SetValue(result, Deserialize(element, property.PropertyType));
                }
                else if (name == "List" && isList)
                {
                    var genericType = typeof (ListDataTransfer<>).MakeGenericType(target.GetGenericArguments());
                    result = Activator.CreateInstance(genericType);
                    var listProperty = genericType.GetProperty("List");
                    var list = (IList) listProperty.GetValue(result);

                    var contentType = typeof (TypeContentDataTransfer<>).MakeGenericType(target.GetGenericArguments());
                    var records = xml.Elements("Item")
                        .Select(
                        el => new
                        {
                            Type = (string) el.Element("Type"),
                            DataTransferType = (string)el.Element("DataTransferType"),
                            Content = el.Element("Content")
                        })
                        .Select(
                            rec => new
                            {
                                Instance = Activator.CreateInstance(contentType),
                                rec.Type, rec.DataTransferType,
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
                }
                else if (property.PropertyType == typeof(Point))
                {
                    var point = new Point()
                    {
                        X = (double) element.Attribute(nameof(Point.X)),
                        Y = (double) element.Attribute(nameof(Point.Y))
                    };
                    property.SetValue(result, point);
                }
                else if (property.PropertyType == typeof(List<Point>))
                {
                    var list = (IList) property.GetValue(result);
                    element.Elements(nameof(Point))
                        .Select(el =>
                            new Point()
                            {
                                X = (double) el.Attribute(nameof(Point.X)),
                                Y = (double) el.Attribute(nameof(Point.Y))
                            })
                        .ToList()
                        .ForEach(p => list.Add(p));
                }
                else if (property.PropertyType == typeof(ImageBox))
                {
                    var fileName = (string)element.Attribute("FileName");
                    var width = (double)element.Attribute("Width");
                    var height = (double)element.Attribute("Height");
                    var size = new Size(width, height);
                    var imageBox = Resolver(fileName, size);
                    property.SetValue(result, imageBox);
                }

            }
            return (IDataTransfer)result;
        }
    }
}
