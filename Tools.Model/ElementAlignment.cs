using System;

namespace PeletonSoft.Tools.Model
{
    public enum ElementAlignment 
    {
        Left,
        Right
    }

    public static class ElementAlignmentHelper
    {
        public static ElementAlignment ToElementAlignment(this string value)
        {
            ElementAlignment result;
            var success = Enum.TryParse<ElementAlignment>(value, out result);
            return success ? result : ElementAlignment.Left;
        }
    }
}
