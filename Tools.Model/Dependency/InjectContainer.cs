using System;
using System.Collections.Generic;

namespace PeletonSoft.Tools.Model.Dependency
{
    public class InjectContainer
    {
        readonly IDictionary<Type, Func<object, object>> _container;

        public InjectContainer()
        {
            _container = new Dictionary<Type, Func<object, object>>();
        }

        public InjectContainer Register<T>(Func<T, object> factory)
        {
            _container[typeof(T)] = o => factory((T)o);
            return this;
        }

        public object Resolve<T>(T param)
        {
            return _container[typeof(T)](param);
        }

        public object Resolve(Type type, object param)
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

        public object Resolve(object param)
        {
            return _container[param.GetType()](param);
        }

    }
}
