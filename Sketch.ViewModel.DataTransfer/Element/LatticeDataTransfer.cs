using System;
using PeletonSoft.Sketch.ViewModel.DataTransfer.Element.Custom;

namespace PeletonSoft.Sketch.ViewModel.DataTransfer.Element
{
    [Serializable]
    public sealed class LatticeDataTransfer : AlignableElementDataTransfer
    {
        public double CellHeight { get; set; }
        public double CellWidth { get; set; }
    }
}
