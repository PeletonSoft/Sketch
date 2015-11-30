using System;
using PeletonSoft.Sketch.ViewModel.DataTransfer.Geometry;
using PeletonSoft.Tools.Model.File;

namespace PeletonSoft.Sketch.ViewModel.DataTransfer.Present
{
    [Serializable]
    public sealed class PreviewPresentDataTransfer : PresentDataTransfer
    {
        public ImageBox ImageBox { get; set; }
        public RectangleDataTransfer Quadrangle { get; set; }
        public SuperimposeOptionDataTransfer SuperimposeOption { get; set; }

    }
}
