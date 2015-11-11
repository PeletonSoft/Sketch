using System;
using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewModel.DataTransfer.Element.Primitive
{
    [Serializable]
    public sealed class ShoulderDataTransfer : IDataTransfer
    {
        public double Length { get; set; }
        public double OffsetY { get; set; }
        public double WaveHeight { get; set; }
        public double Slope { get; set; }

    }
}
