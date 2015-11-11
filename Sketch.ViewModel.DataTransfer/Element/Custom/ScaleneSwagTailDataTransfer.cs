using System;
using PeletonSoft.Sketch.ViewModel.DataTransfer.Element.Primitive;

namespace PeletonSoft.Sketch.ViewModel.DataTransfer.Element.Custom
{
    [Serializable]
    public class ScaleneSwagTailDataTransfer : SwagTailDataTransfer
    {
        public ShoulderDataTransfer LeftShoulder { get; set; }
        public ShoulderDataTransfer RightShoulder { get; set; }

    }
}
