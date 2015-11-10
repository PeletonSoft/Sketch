using System;
using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewModel.DataTransfer.Geometry
{
    [Serializable]
    public sealed class TransformationDataTransfer : IDataTransfer
    {
        public string Rotation { get; set; }
        public string Reflection { get; set; }

    }
}
