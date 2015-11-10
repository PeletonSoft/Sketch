using PeletonSoft.Sketch.ViewModel.DataTransfer.Interface;

namespace PeletonSoft.Sketch.ViewModel.DataTransfer.Element.Custom
{
    public abstract class AlignableElementDataTransfer : IElementDataTransfer
    {
        public string Description { get; set; }
        //public ClotheMemento Clothe { get; set; }
        public bool Visibility { get; set; }
        public double Opacity { get; set; }
        public string Layout { get; set; }

        public double OffsetY { get; set; }
        public double OffsetX { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
    }
}
