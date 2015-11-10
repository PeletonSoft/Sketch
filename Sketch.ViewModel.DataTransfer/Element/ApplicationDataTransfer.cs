using PeletonSoft.Sketch.ViewModel.DataTransfer.Element.Custom;

namespace PeletonSoft.Sketch.ViewModel.DataTransfer.Element
{
    public sealed class ApplicationDataTransfer : AlignableElementDataTransfer
    {
        public double Thickness { get; set; }
        public string Reflection { get; set; }
        public string Outline { get; set; }
    }
}
