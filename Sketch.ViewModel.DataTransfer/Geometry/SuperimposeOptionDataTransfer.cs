using System;
using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewModel.DataTransfer.Geometry
{
    [Serializable]
    public sealed class SuperimposeOptionDataTransfer : IDataTransfer
    {
        public double MarkerRadius { get; set; }
        public double MarkerOpacity { get; set; }
        public double ForegroundOpacity { get; set; }
        public double BackgroundOpacity { get; set; }

    }
}
