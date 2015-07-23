﻿using System;
using System.ComponentModel;
using System.Linq.Expressions;
using PeletonSoft.Sketch.ViewModel.Interface.Visual;
using PeletonSoft.Tools.Model.NotifyChanged;

namespace PeletonSoft.Sketch.ViewModel.Visual
{
    public sealed class ScreenVisualViewModel : IScreenVisualViewModel
    {
        #region implement INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            this.OnPropertyChanged(PropertyChanged, propertyName);
        }

        private void OnPropertyChanged<T>(Expression<Func<ScreenVisualViewModel, T>> expression)
        {
            expression.OnPropertyChanged(OnPropertyChanged);
        }
        #endregion


        public ScreenVisualViewModel(VisualOptions visualOptions, ScreenViewModel screen)
        {
            VisualOptions = visualOptions;
            Screen = screen;
            Screen
                .SetPropertyChanged(el => el.Width, () => OnPropertyChanged(v => v.Width))
                .SetPropertyChanged(el => el.Height, () => OnPropertyChanged(v => v.Height));
        }

        private VisualOptions VisualOptions { get; set; }

        public double Width
        {
            get
            {
                return VisualOptions.PixelPerUnit.Transform(Screen.Width);
            }
        }

        public double Height
        {
            get
            {
                return VisualOptions.PixelPerUnit.Transform(Screen.Height);
            }
        }
        private ScreenViewModel Screen { get; set; }

    }
}
