using System;
using System.Linq.Expressions;
using PeletonSoft.Tools.Model.ObjectEvent.ObjectEventExpression;

namespace PeletonSoft.Tools.Model.ObjectEvent
{
    public class Getter<TS,TP> where TS : class
    {
        public Func<TS,TP> GetterValue { get; }
        public string PropertyName { get; }

        public Getter(string propertyName, Func<TS, TP> getterValue)
        {
            PropertyName = propertyName;
            GetterValue = getterValue;
        }
    }
}
