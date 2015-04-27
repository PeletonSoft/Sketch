﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Tools.Model.Memento;
using PeletonSoft.Tools.Model.NotifyChanged;

namespace PeletonSoft.Sketch.ViewModel.Element.Primitive
{
    public sealed class DecorativeBorderViewModel : INotifyPropertyChanged, IOriginator
    {
        #region implement INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            this.OnPropertyChanged(PropertyChanged, propertyName);
        }

        private bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            Action notificator = () => OnPropertyChanged(propertyName);
            return notificator.SetField(ref field, value);
        }
        #endregion

        #region implement IOriginator
        public void RestoreDefault()
        {
            ResetChains();
        }
        #endregion

        public DecorativeBorderViewModel(IWorkspaceBit workspaceBit)
        {
            WorkspaceBit = workspaceBit;
            IsEdited = false;
        }

        public IWorkspaceBit WorkspaceBit { get; private set; }

        private double _width;
        public double Width
        {
            get { return _width; }
            set { SetField(ref _width, value); }
        }

        private double _height;
        public double Height
        {
            get { return _height; }
            set { SetField(ref _height, value); }
        }

        private bool _isEdited;
        public bool IsEdited
        {
            get { return _isEdited; }
            set { SetField(ref _isEdited, value); }
        }

        private IEnumerable<Point> _points;
        public IEnumerable<Point> Points
        {
            get { return _points; }
            set { SetField(ref _points, value); }
        }

        public void ResetChains()
        {
            Points = new List<Point>
            {
                new Point(0, 0),
                new Point(Width, Height)
            };
        }
    }
}

