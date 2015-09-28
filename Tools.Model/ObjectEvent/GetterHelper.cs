using System;

namespace PeletonSoft.Tools.Model.ObjectEvent
{
    public static class GetterHelper
    {
        public static Getter<TS, TP> ExtractGetter<TS, TP>(
            this TS sender, string propertyName,
            Func<TS, TP> getterValue) where TS : class
        {
            return new Getter<TS, TP>(propertyName, getterValue);
        }

    }
}
