using PeletonSoft.Sketch.Model.Interface.Element;

namespace PeletonSoft.Sketch.Model.ClotheStrategy
{
    public class ClotheCalculateStrategy : IClotheCalculateStrategy
    {
        private IAlignableElement Element { get; set; }
        public ClotheCalculateStrategy(IAlignableElement element)
        {
            Element = element;
        }
        public void Calculate(IClothe clothe)
        {
            clothe.Width = Element.Width;
            clothe.Height = Element.Height;
        }
    }
}
