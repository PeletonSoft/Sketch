﻿using System;
using System.Linq.Expressions;
using PeletonSoft.Sketch.ViewModel.Element;
using PeletonSoft.Sketch.ViewModel.Visual.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Visual.Element.Primitive;
using PeletonSoft.Tools.Model.NotifyChanged;

namespace PeletonSoft.Sketch.ViewModel.Visual.Element
{
    public class DeJabotVisualViewModel : ElementVisualViewModel
    {
        private void OnPropertyChanged<T>(Expression<Func<DeJabotVisualViewModel, T>> expression)
        {
            expression.OnPropertyChanged(OnPropertyChanged);
        }
        public DeJabotVisualViewModel(VisualOptions visualOptions, DeJabotViewModel element)
            : base(visualOptions, element)
        {
            Element.SetPropertyChanged(el => el.WavySurface, () => OnPropertyChanged(v => v.WavySurface));
        }

        private new DeJabotViewModel Element
        {
            get { return (DeJabotViewModel) base.Element; }
        }

        public WavySurfaceVisualViewModel WavySurface
        {
            get
            {
                if (Element.WavySurface == null)
                {
                    return null;
                }
                
                return new WavySurfaceVisualViewModel(VisualOptions, Element.WavySurface);
            }
        }
    }
}