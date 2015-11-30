using System;
using PeletonSoft.Sketch.ViewModel.DataTransfer.Interface;
using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewModel.DataTransfer.Element.Primitive
{
    [Serializable]
    public sealed class ClotheDataTransfer : IClotheDataTransfer
    {
        public double? Width { get; set; }
        public double? Height { get; set; }
    }
}
