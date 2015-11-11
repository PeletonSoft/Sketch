using System;
using PeletonSoft.Sketch.ViewModel.DataTransfer.Element.Primitive;

namespace PeletonSoft.Sketch.ViewModel.DataTransfer.Element.Custom
{
    [Serializable]
    public class EqualSwagTailDataTransfer : SwagTailDataTransfer
    {
        public ShoulderDataTransfer Shoulder { get; set; }
    }
}
