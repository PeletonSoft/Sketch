using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace PeletonSoft.Tools.Model.NotifyChanged
{
    public class Getter<TS,TP> where TS : class, INotifyPropertyChanged
    {
        public Func<TS,TP> GetterValue { get; }
        public string PropertyName { get; }

        public Getter(string propertyName, Func<TS, TP> getterValue)
        {
            PropertyName = propertyName;
            GetterValue = getterValue;
        }

        public Getter(Expression<Func<TS, TP>> expression)
            : this(expression.GetPropertyName(), expression.Compile())
        {
        }


    }
}
