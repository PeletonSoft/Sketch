using PeletonSoft.Sketch.Model.ClotheStrategy;
using PeletonSoft.Sketch.Model.Element.Primitive;
using PeletonSoft.Sketch.Model.Interface.Element;

namespace PeletonSoft.Sketch.Model.Element.Custom
{
    public abstract class AlignableElement : Element, IAlignableElement
    {
        public double Width { get; set; }
        public double Height { get; set; }
        public double OffsetX { get; set; }
        public double OffsetY { get; set; }
        public IClothe Clothe { get; private set; }

        protected AlignableElement()
        {
            Clothe = new Clothe(new ClotheCalculateStrategy(this));
        }
    }
}
