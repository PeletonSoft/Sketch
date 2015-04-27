namespace PeletonSoft.SketchModel.Interface.Element
{
    public interface IAlignableElement : IElement
    {
        double Width { get; set; }
        double Height { get; set; }
        double OffsetX { get; set; }
        double OffsetY { get; set; }
    }
}