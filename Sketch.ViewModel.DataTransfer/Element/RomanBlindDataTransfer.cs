using System;
using PeletonSoft.Sketch.ViewModel.DataTransfer.Element.Custom;
using PeletonSoft.Sketch.ViewModel.DataTransfer.Element.Primitive;

namespace PeletonSoft.Sketch.ViewModel.DataTransfer.Element
{
    [Serializable]
    public sealed class RomanBlindDataTransfer : AlignableElementDataTransfer
    {
        public DecorativeBorderDataTransfer DecorativeBorder { get; set; }
        public int WaveCount { get; set; }
        public double DenseHeight { get; set; }
        public double CoulisseThickness { get; set; }
    }
}
