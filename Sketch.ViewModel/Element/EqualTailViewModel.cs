﻿using PeletonSoft.Sketch.Model.Element.Custom;
using PeletonSoft.Sketch.ViewModel.DataTransfer.Interface;
using PeletonSoft.Sketch.ViewModel.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Interface;

namespace PeletonSoft.Sketch.ViewModel.Element
{
    public class EqualTailViewModel : EqualSwagTailViewModel
    {
        public EqualTailViewModel(IWorkspaceBit workspaceBit, SwagTail model)
            : base(workspaceBit, model)
        {
        }

        public override IElementDataTransfer CreateState()
        {
            throw new System.NotImplementedException();
        }
    }
}
