using System;
using PeletonSoft.Sketch.ViewModel.DataTransfer.Interface;
using PeletonSoft.Tools.Model;

namespace PeletonSoft.Sketch.ViewModel.DataTransfer.Element.Custom
{
    [Serializable]
    public class PleatableDataTransfer : IElementDataTransfer
    {
        public string Description { get; set; }
        public bool Visibility { get; set; }
        public double Opacity { get; set; }

        public double DenseWidth { get; set; }
        public int WaveCount { get; set; }
        public ElementAlignment Alignment { get; set; }

    }
}
