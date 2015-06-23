using PeletonSoft.Sketch.Model.Interface.Element;

namespace PeletonSoft.Sketch.Model.Element.Custom
{
    public class Element : IElement
    {
        public string Description { get; set; }
        public double Opacity { get; set; }
        public bool Visibility { get; set; }
    }
}
