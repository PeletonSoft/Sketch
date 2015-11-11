using System;
using PeletonSoft.Sketch.ViewModel.DataTransfer.Element.Primitive;
using PeletonSoft.Sketch.ViewModel.DataTransfer.Interface;

namespace PeletonSoft.Sketch.ViewModel.DataTransfer.Element.Custom
{
    [Serializable]
    public class AlignableElementDataTransfer : IElementDataTransfer
    {
        public string Description { get; set; }
        public IClotheDataTransfer Clothe { get; set; }
        public bool Visibility { get; set; }
        public double Opacity { get; set; }
        public string Layout { get; set; }

        public double OffsetY { get; set; }
        public double OffsetX { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
    }
}
