using System;
using System.Reflection;
using System.Xml.Linq;

namespace PeletonSoft.Tools.Model.Memento.Serialize
{
    public sealed class XmlPrimitive
    {
        public Func<string, object, XElement> Serializer { get; }
        public Action<PropertyInfo,object, XElement> Deserializer { get; }

        public XmlPrimitive(Func<string, object, XElement> serializer, Action<PropertyInfo, object, XElement> deserializer)
        {
            Serializer = serializer;
            Deserializer = deserializer;
        }
    }
}
 