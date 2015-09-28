namespace PeletonSoft.Sketch.Model.Interface.Element
{
    public interface IVisibleElement : IElement
    {
        double Opacity { get; set; }
        bool Visibility { get; set; }
    }
}
