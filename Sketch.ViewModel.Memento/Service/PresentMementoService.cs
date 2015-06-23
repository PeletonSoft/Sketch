using System;
using System.Collections.Generic;
using System.Linq;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewModel.Memento.Service
{
    public class PresentMementoService : IMementoService<IPresentViewModel>
    {
        private static IDictionary<Type, Func<IMemento<IPresentViewModel>>> _items;
        public IDictionary<Type, Func<IMemento<IPresentViewModel>>> Items
        {
            get
            {
                return _items ?? 
                    (_items = new Dictionary<Type, Func<IMemento<IPresentViewModel>>>());
            }
        }

        public void Register(Type type,  Func<IMemento<IPresentViewModel>> creator)
        {
            if (Items.All(x => x.Key != type))
            {
                Items.Add(type, creator);
            }
        }
    }
}
