﻿using System;
using PeletonSoft.Sketch.ViewModel.DataTransfer.Element.Custom;
using PeletonSoft.Sketch.ViewModel.DataTransfer.Element.Primitive;

namespace PeletonSoft.Sketch.ViewModel.DataTransfer.Element
{
    [Serializable]
    public sealed class HardPelmetDataTransfer : AlignableElementDataTransfer
    {
        public DecorativeBorderDataTransfer DecorativeBorder { get; set; }
    }
}
