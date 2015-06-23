namespace PeletonSoft.Sketch.Model.Interface.Element
{
    public interface IElement
    {
        string Description { get; set; }
        double Opacity { get; set; }
        bool Visibility { get; set; }
    }
}