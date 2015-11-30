using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Sketch.ViewModel.Interface.Container;
using PeletonSoft.Sketch.ViewModel.Memento.Element.Service;
using PeletonSoft.Tools.Model.File;
using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewModel.Memento
{
    public class ElementListMemento : IMemento<IElementListViewModel>
    {
        public IList<ElementMementoRecord> List { get; set; }
        public void GetState(IElementListViewModel originator)
        {
            List = new List<ElementMementoRecord>();
            foreach (var element in originator.Items)
            {
                var factoryRecord = ElementMementoFactoryService.Items
                    .FirstOrDefault(x => x.ElementType == element.Type);
                if (factoryRecord == null)
                {
                    continue;
                }

                var elementMemento = factoryRecord.ElementMementoFactoryMethod();
                elementMemento.GetState(element.Value);

                var record = new ElementMementoRecord()
                {
                    Element = elementMemento,
                    ElementType = element.Type.Name
                };

                List.Add(record);
            }
        }

        public void SetState(IElementListViewModel originator)
        {
            originator.RestoreDefault();

            foreach (var record in List)
            {
                var factoryRecord = ElementMementoFactoryService.Items
                    .FirstOrDefault(x => x.ElementType.Name == record.ElementType);
                if (factoryRecord == null)
                {
                    continue;
                }

                var factory = originator.Factories
                    .FirstOrDefault(x => x.GetType() == factoryRecord.FactoryType);

                if (factory == null)
                {
                    continue;
                }

                var element = originator.AppendElement(factory);
                record.Element.SetState(element);
            }
        }

        public IEnumerable<IFileBox> GetFiles()
        {
            var filess = List
                .Select(x => x.Element.GetFiles());
            return filess.GetFiles(); 
        }

        public XElement GetXml(Dictionary<string, IFileBox> files)
        {
            return new XElement("root",
                new XElement("Elements", List
                    .Select(record => new XElement("Element",
                        new XElement("ElementType", record.ElementType),
                        new XElement("Content", record.Element.GetXml(files).Elements())))
                    )
                );
        }

        public void SetXml(XElement xml, string path)
        {
            List = new List<ElementMementoRecord>();

            var xElements = xml.Element("Elements");
            if (xElements != null)
            {
                foreach (var xElement in xElements.Elements("Element"))
                {
                    var elementType = (string) xElement.Element("ElementType");

                    var factoryRecord = ElementMementoFactoryService.Items
                    .FirstOrDefault(x => x.ElementType.Name == elementType);
                    if (factoryRecord == null)
                    {
                        continue;
                    }

                    var elementMemento = factoryRecord.ElementMementoFactoryMethod();
                    elementMemento.SetXml(xElement.Element("Content"),path);

                    var record = new ElementMementoRecord()
                    {
                        Element = elementMemento,
                        ElementType = elementType
                    };

                    List.Add(record);
                }
                
            }

        }
    }
}
