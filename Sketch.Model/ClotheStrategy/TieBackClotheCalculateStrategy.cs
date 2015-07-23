using PeletonSoft.Sketch.Model.Element;
using PeletonSoft.Sketch.Model.Interface.Element;

namespace PeletonSoft.Sketch.Model.ClotheStrategy
{
    public class TieBackClotheCalculateStrategy : IClotheCalculateStrategy
    {
        private TieBack Element { get; set; }
        public void Calculate(IClothe clothe)
        {
            clothe.Width = 2.5*Element.Length;
            clothe.Height = Element.Depth;
        }

        public TieBackClotheCalculateStrategy(TieBack element)
        {
            Element = element;
        }
    }
}
