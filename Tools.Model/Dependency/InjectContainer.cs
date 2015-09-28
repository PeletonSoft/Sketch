using System;
using System.Collections.Generic;
using PeletonSoft.Tools.Model.Logic;

namespace PeletonSoft.Tools.Model.Dependency
{
    public class InjectContainer
    {
        readonly IDictionary<Type, Func<IViewModel, IVisualViewModel>> _container;

        public InjectContainer()
        {
            _container = new Dictionary<Type, Func<IViewModel, IVisualViewModel>>();
        }

        public InjectContainer Register<T>(Func<T, IVisualViewModel> factory)
        {
            _container[typeof(T)] = o => factory((T)o);
            return this;
        }

        public object Resolve<T>(IViewModel param)
        {
            return _container[typeof(T)](param);
        }

        public object Resolve(Type type, IViewModel param)
        {
            try
            {
                return _container[type](param);
            }
            catch (Exception)
            {
                return null;
            }
            
        }

        public object Resolve(IViewModel param)
        {
            return _container[param.GetType()](param);
        }

    }
}
