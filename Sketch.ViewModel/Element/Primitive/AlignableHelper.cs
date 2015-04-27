using System.Windows;
using PeletonSoft.Sketch.ViewModel.Interface.Element;

namespace PeletonSoft.Sketch.ViewModel.Element.Primitive
{
    public static class AlignableHelper
    {
        public static Rect GetRect(this IAlignable alignable)
        {
            return new Rect(0, 0, alignable.Width, alignable.Height);
        }
    }
}
