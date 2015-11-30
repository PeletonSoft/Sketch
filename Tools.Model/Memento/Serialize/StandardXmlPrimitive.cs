using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Xml.Linq;

namespace PeletonSoft.Tools.Model.Memento.Serialize
{
    public static class StandardXmlPrimitive
    {
        public static IDictionary<Type, XmlPrimitive> Primitives { get; } =
            new Dictionary<Type, XmlPrimitive>
            {
                [typeof (int)] = new XmlPrimitive(
                    (name, value) => new XElement(name, (int) value),
                    (property, instance, element) => property.SetValue(instance, (int) element)),
                [typeof (string)] = new XmlPrimitive(
                    (name, value) => new XElement(name, (string) value),
                    (property, instance, element) => property.SetValue(instance, (string) element)),
                [typeof (bool)] = new XmlPrimitive(
                    (name, value) => new XElement(name, (bool) value),
                    (property, instance, element) => property.SetValue(instance, (bool) element)),
                [typeof (double)] = new XmlPrimitive(
                    (name, value) => new XElement(name, (double) value),
                    (property, instance, element) => property.SetValue(instance, (double) element)),
                [typeof (double?)] = new XmlPrimitive(
                    (name, value) => new XElement(name, (double?) value),
                    (property, instance, element) => property.SetValue(instance, (double?) element)),
                [typeof (Point)] = new XmlPrimitive(
                    (name, value) =>
                    {
                        var point = (Point) value;
                        return new XElement(name,
                            new XAttribute(nameof(point.X), point.X),
                            new XAttribute(nameof(point.Y), point.Y));
                    },
                    (property, instance, element) =>
                    {
                        var point = new Point()
                        {
                            X = (double) element.Attribute(nameof(Point.X)),
                            Y = (double) element.Attribute(nameof(Point.Y))
                        };
                        property.SetValue(instance, point);
                    }),
                [typeof (List<Point>)] = new XmlPrimitive(
                    (name, value) =>
                    {
                        var points = (List<Point>) value;
                        return new XElement(name,
                            points.Select(
                                p => new XElement(
                                    nameof(Point),
                                    new XAttribute(nameof(p.X), p.X),
                                    new XAttribute(nameof(p.Y), p.Y))));
                    },
                    (property, instance, element) =>
                    {
                        var list = (IList) property.GetValue(instance);
                        list.Clear();
                        element.Elements(nameof(Point))
                            .Select(el =>
                                new Point()
                                {
                                    X = (double) el.Attribute(nameof(Point.X)),
                                    Y = (double) el.Attribute(nameof(Point.Y))
                                })
                            .ToList()
                            .ForEach(p => list.Add(p));
                    })
            };
    }
}
