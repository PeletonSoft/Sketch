﻿using PeletonSoft.Sketch.ViewModel.Interface.Geometry;
using PeletonSoft.Tools.Model.Collection;
using PeletonSoft.Tools.Model.Memento.Container;

namespace PeletonSoft.Sketch.ViewModel.Design
{
    public class RotableViewModel : IRotableViewModel
    {
        public RotableViewModel(IContainer<IRotationViewModel> rotations)
        {
            Rotations = rotations;
        }

        public IRotationViewModel Rotation { get; set; }
        public IContainer<IRotationViewModel> Rotations { get; }
    }
}
