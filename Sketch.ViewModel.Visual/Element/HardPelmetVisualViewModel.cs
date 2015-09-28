﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Windows;
using PeletonSoft.Sketch.ViewModel.Element;
using PeletonSoft.Sketch.ViewModel.Interface.Visual;
using PeletonSoft.Sketch.ViewModel.Visual.Element.Custom;
using PeletonSoft.Tools.Model.Logic;
using PeletonSoft.Tools.Model.NotifyChanged;

namespace PeletonSoft.Sketch.ViewModel.Visual.Element
{
    public sealed class HardPelmetVisualViewModel : ElementVisualViewModel, IElementVisualViewModel<HardPelmetViewModel>
    {
        private void OnPropertyChanged<T>(Expression<Func<HardPelmetVisualViewModel, T>> expression)
        {
            expression.OnPropertyChanged(OnPropertyChanged);
        }

        public HardPelmetVisualViewModel(VisualOptions visualOptions, HardPelmetViewModel element) 
            : base(visualOptions, element)
        {
            Element.SetPropertyChanged(el => el.Points, () => OnPropertyChanged(v => v.Points));
        }

        public new HardPelmetViewModel Element
        {
            get { return (HardPelmetViewModel)base.Element; }
        }

        public IEnumerable<Point> Points
        {
            get
            {
                var pixelPerUnit = VisualOptions.PixelPerUnit;
                var points = pixelPerUnit.Transform(Element.Points);
                return points;
            }
        }
    }
}
