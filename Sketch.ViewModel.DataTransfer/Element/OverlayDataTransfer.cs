using System;
using PeletonSoft.Sketch.ViewModel.DataTransfer.Interface;

namespace PeletonSoft.Sketch.ViewModel.DataTransfer.Element
{
    [Serializable]
    public sealed class OverlayDataTransfer : IElementDataTransfer
    {
        public int SelectedIndex { get; set; }
        public string Description { get; set; }
        public double OverOffsetY { get; set; }
        public double OverOffsetX { get; set; }
        public double OverHeight { get; set; }
        public double OverWidth { get; set; }

    }
}
