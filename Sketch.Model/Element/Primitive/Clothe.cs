using PeletonSoft.Sketch.Model.Interface.Element;

namespace PeletonSoft.Sketch.Model.Element.Primitive
{
    public class Clothe : IClothe
    {
        public double? Width { get; set; }
        public double? Height { get; set; }

        public double? Area
        {
            get { return Width != null && Height != null ? Width * Height : null; }
        }

        private IClotheCalculateStrategy Strategy { get; set; }

        public Clothe(IClotheCalculateStrategy strategy)
        {
            Strategy = strategy;
        }

        public void Calculate()
        {
            Strategy.Calculate(this);
        }
    }
}
