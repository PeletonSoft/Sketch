using System;
using PeletonSoft.Sketch.ViewModel.Interface.Element;
using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewMode.Memento.Element.Service
{
    public class ElementMementoFactoryRecord
    {
        public delegate TR GetMemento<out TR>() where TR : IMemento<IElementViewModel>;
        public Type FactoryType { get; private set; }
        public Type ElementType { get; private set; }
        public Type MementoType { get; private set; }
        public Func<IMemento<IElementViewModel>> ElementMementoFactoryMethod { get; private set; }

        public ElementMementoFactoryRecord
            (Type factoryType, Type elementType, Type mementoType, Func<IMemento<IElementViewModel>> elementMementoFactoryMethod)
        {
            FactoryType = factoryType;
            ElementType = elementType;
            MementoType = mementoType;
            ElementMementoFactoryMethod = elementMementoFactoryMethod;
        }
    }
}
