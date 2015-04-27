using PeletonSoft.Sketch.ViewModel.Interface.Element;

namespace PeletonSoft.Sketch.ViewModel.Element.Clothe
{
    public class ClotheCalculateStrategy : IClotheCalculateStrategy
    {
        public ClotheCalculateStrategy(ILayoutable element)
        {
            Element = element;
        }

        private ILayoutable Element { get; set; }

        public void Calculate(IClotheViewModel clothe)
        {
            clothe.Width = Element.Layout.Width;
            clothe.Height = Element.Layout.Height;
        }
    }
}
