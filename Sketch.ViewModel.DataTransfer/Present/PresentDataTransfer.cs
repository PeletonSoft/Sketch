using System;
using PeletonSoft.Sketch.ViewModel.DataTransfer.Interface;

namespace PeletonSoft.Sketch.ViewModel.DataTransfer.Present
{
    [Serializable]
    public class PresentDataTransfer : IPresentDataTransfer
    {
        public double Zoom { get; set; }
    }
}
