using System;
using System.Collections.Generic;
using System.Linq;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewMode.Memento.Service
{
    public class WorkModeMementoService : IMementoService<IWorkModeViewModel>
    {
        private static IDictionary<Type, Func<IMemento<IWorkModeViewModel>>> _items;

        public IDictionary<Type, Func<IMemento<IWorkModeViewModel>>> Items
        {
            get
            {
                return _items ??
                       (_items = new Dictionary<Type, Func<IMemento<IWorkModeViewModel>>>());
            }
        }

        public void Register(Type type, Func<IMemento<IWorkModeViewModel>> creator)
        {
            if (Items.All(x => x.Key != type))
            {
                Items.Add(type, creator);
            }
        }
    }
}
