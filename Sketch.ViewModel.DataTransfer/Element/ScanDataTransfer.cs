using System;
using PeletonSoft.Sketch.ViewModel.DataTransfer.Element.Custom;
using PeletonSoft.Sketch.ViewModel.DataTransfer.Geometry;
using PeletonSoft.Tools.Model.File;

namespace PeletonSoft.Sketch.ViewModel.DataTransfer.Element
{
    [Serializable]
    public sealed class ScanDataTransfer : AlignableElementDataTransfer
    {
        public RectangleDataTransfer Rectangle { get; set; }
        public ImageBox ImageBox { get; set; }
        public TransformationDataTransfer Transformation { get; set; }
        public SuperimposeOptionDataTransfer SuperimposeOption { get; set; }
    }
}
