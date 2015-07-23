﻿using System;
using System.Linq.Expressions;
using System.Windows;
using PeletonSoft.Sketch.ViewModel.Element;
using PeletonSoft.Sketch.ViewModel.Interface.Visual;
using PeletonSoft.Sketch.ViewModel.Visual.Element.Custom;
using PeletonSoft.Tools.Model;
using PeletonSoft.Tools.Model.Dependency;
using PeletonSoft.Tools.Model.NotifyChanged;

namespace PeletonSoft.Sketch.ViewModel.Visual.Element
{
    public class OverlayVisualViewModel : ElementVisualViewModel
    {
        private void OnPropertyChanged<T>(Expression<Func<OverlayVisualViewModel, T>> expression)
        {
            expression.OnPropertyChanged(OnPropertyChanged);
        }

        public OverlayVisualViewModel(InjectContainer visualContainer, VisualOptions visualOptions,
            OverlayViewModel element)
            : base(visualOptions, element)
        {
            VisualContainer = visualContainer;
            Element
                .SetPropertyChanged(el => el.Layout, () => OnPropertyChanged(v => v.Layout))
                .SetPropertyChanged(el => el.OverRect, () => OnPropertyChanged(v => v.OverRect))
                .SetPropertyChanged(el => el.SelectedItem,
                    () =>
                    {
                        OnPropertyChanged(v => v.OverlayElement);
                        OnPropertyChanged(v => v.Layout);
                    });
        }

        private InjectContainer VisualContainer { get; set; }
        private new OverlayViewModel Element
        {
            get { return (OverlayViewModel) base.Element; }
        }

        public IElementVisualViewModel OverlayElement
        {
            get
            {
                var element = Element.SelectedItem;
                return (IElementVisualViewModel) VisualContainer.Resolve(element.GetType(), element);
            }
        }

        public Rect OverRect
        {
            get { return VisualOptions.PixelPerUnit.Transform(Element.OverRect); }
        }
    }
}
