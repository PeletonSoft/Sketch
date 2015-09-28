namespace PeletonSoft.Sketch.Model.Interface.Element
{
    public interface IClothe
    {
        double? Width { get; set; }
        double? Height { get; set; }
        double? Area { get;  }
        void Calculate();
    }
}
