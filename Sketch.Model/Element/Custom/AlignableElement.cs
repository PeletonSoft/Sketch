using PeletonSoft.Sketch.Model.Interface.Element;

namespace PeletonSoft.Sketch.Model.Element.Custom
{
    public class AlignableElement : Element, IAlignableElement
    {
        public double Width { get; set; }
        public double Height { get; set; }
        public double OffsetX { get; set; }
        public double OffsetY { get; set; }
    }
}
