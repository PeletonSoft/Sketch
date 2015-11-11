using System;
using PeletonSoft.Sketch.ViewModel.DataTransfer.Element.Custom;
using PeletonSoft.Tools.Model;
using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewModel.DataTransfer.Element
{
    [Serializable]
    public sealed class DeJabotDataTransfer : AlignableElementDataTransfer
    {
        public double SmallHeight { get; set; }
        public int WaveCount { get; set; }
        public double WaveHeight { get; set; }
        public ElementAlignment Alignment { get; set; }
        public ElementAlignment WaveAlignment { get; set; }
    }
}
