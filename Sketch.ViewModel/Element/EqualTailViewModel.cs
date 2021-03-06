﻿using PeletonSoft.Sketch.Model.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Interface;

namespace PeletonSoft.Sketch.ViewModel.Element
{
    public sealed class EqualTailViewModel : EqualSwagTailViewModel
    {
        public EqualTailViewModel(IWorkspaceBit workspaceBit, SwagTail model)
            : base(workspaceBit, model)
        {
        }
    }
}
