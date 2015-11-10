using System;
using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewModel.DataTransfer.Geometry
{
    [Serializable]
    public sealed class RectangleDataTransfer : IDataTransfer
    {
        public VertexDataTransfer TopLeft { get; set; }
        public VertexDataTransfer TopRight { get; set; }
        public VertexDataTransfer BottomLeft { get; set; }
        public VertexDataTransfer BottomRight { get; set; }

    }
}
