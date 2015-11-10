using System;
using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewModel.DataTransfer.Geometry
{
    [Serializable]
    public sealed class VertexDataTransfer : IDataTransfer
    {
        public double X { get; set; }
        public double Y { get; set; }
    }
}
