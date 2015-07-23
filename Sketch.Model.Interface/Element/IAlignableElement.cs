namespace PeletonSoft.Sketch.Model.Interface.Element
{
    public interface IAlignableElement : IVisibleElement, IClothable
    {
        double Width { get; set; }
        double Height { get; set; }
        double OffsetX { get; set; }
        double OffsetY { get; set; }
    }
}