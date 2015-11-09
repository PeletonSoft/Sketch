using System;
using PeletonSoft.Sketch.ViewModel.DataTransfer.Interface;

namespace PeletonSoft.Sketch.ViewModel.DataTransfer
{
    [Serializable]
    public class ScreenDataTransfer : IScreenDataTransfer
    {
        public double Width { get; set; }
        public double Height { get; set; }
    }
}
