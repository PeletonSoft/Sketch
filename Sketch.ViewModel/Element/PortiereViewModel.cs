﻿using PeletonSoft.Sketch.Model.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Interface;

namespace PeletonSoft.Sketch.ViewModel.Element
{
    public class PortiereViewModel : SheetViewModel
    {

        public PortiereViewModel(IWorkspaceBit workspaceBit, Portiere model)
            : base(workspaceBit, model)
        {
        }

    }
}
