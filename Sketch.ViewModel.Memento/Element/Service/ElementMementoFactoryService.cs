using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace PeletonSoft.Sketch.ViewMode.Memento.Element.Service
{
    public static class ElementMementoFactoryService
    {
        private static IList<ElementMementoFactoryRecord> _items;
        public static IEnumerable<ElementMementoFactoryRecord> Items
        {
            get
            {
                if (_items == null)
                {
                    _items = new List<ElementMementoFactoryRecord>();
                }
                return new ReadOnlyCollection<ElementMementoFactoryRecord>(_items);
            }
        }

        public static void Register(ElementMementoFactoryRecord record)
        {
            var findFactory = Items.Any(x => x.FactoryType == record.FactoryType);
            var findElement = Items.Any(x => x.ElementType == record.ElementType);
            if (!findElement && !findFactory)
            {
                _items.Add(record);
            }
        }
    }
}
