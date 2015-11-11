using System;
using PeletonSoft.Sketch.ViewModel.DataTransfer.Element.Custom;
using PeletonSoft.Sketch.ViewModel.DataTransfer.Element.Primitive;

namespace PeletonSoft.Sketch.ViewModel.DataTransfer.Element
{
    [Serializable]
    public sealed class TieBackDataTransfer : PleatableDataTransfer
    {
        public double Length { get; set; }
        public double Depth { get; set; }
        public double DropHeight { get; set; }
        public double Protrusion { get; set; }
        public double OffsetX { get; set; }
        public double OffsetY { get; set; }
        public TieBackSideDataTransfer LeftSide { get; set; }
        public TieBackSideDataTransfer RightSide { get; set; }
    }
}
