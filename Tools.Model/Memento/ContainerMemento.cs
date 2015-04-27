﻿using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace PeletonSoft.Tools.Model.Memento
{
    public class ContainerMemento<T> where T:IOriginator
    {
        public ContainerMemento(IMementoService<T> service, string typeName)
        {
            _service = service;
            _typeName = typeName;
        }

        public IDictionary<string, IMemento<T>> Mementoes;
        private readonly IMementoService<T> _service;
        private readonly string _typeName;

        public void GetState(IContainerOriginator<T> originator)
        {
            Mementoes = new Dictionary<string, IMemento<T>>();

            foreach (var present in originator.Items)
            {
                var type = present.GetType();
                var memento = _service.Items[type]();
                memento.GetState(present);
                Mementoes.Add(type.Name,memento);
            }
        }

        public void SetState(IContainerOriginator<T> originator)
        {
            foreach (var present in originator.Items)
            {
                present.RestoreDefault();
            }

            foreach (var present in originator.Items)
            {
                var type = present.GetType();
                var memento = Mementoes[type.Name];
                memento.SetState(present);
            }
        }

        public IEnumerable<string> GetFiles()
        {
            var filess = Mementoes
                .Select(x => x.Value.GetFiles());
            return filess.GetFiles();
        }

        public XElement GetXml(Dictionary<string, string> files)
        {
            return new XElement("root",
                Mementoes.Select(x => new XElement(_typeName,
                    new XElement("Type", x.Key),
                    new XElement("Content", x.Value.GetXml(files).Elements())))
                );
        }

        public void SetXml(XElement xml, string path)
        {
            Mementoes = new Dictionary<string, IMemento<T>>();

            foreach (var xElement in xml.Elements(_typeName))
            {
                var typeName = (string) xElement.Element("Type");
                var xContent = xElement.Element("Content");
                var mementoCreator = _service.Items
                    .FirstOrDefault(x => x.Key.Name == typeName)
                    .Value;
                var memento = mementoCreator();
                memento.SetXml(xContent, path);
                Mementoes.Add(typeName, memento);
            }
        }
    }
}
