using System;
using System.Collections.Generic;
using System.Windows;
using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewModel.DataTransfer.Element.Primitive
{
    [Serializable]
    public sealed class DecorativeBorderDataTransfer : IDataTransfer
    {
        public double Width { get; set; }
        public double Height { get; set; }
        public List<Point> Points { get; } = new List<Point>();
    }
}
