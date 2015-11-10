using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using PeletonSoft.Sketch.Model.Element.Primitive;
using PeletonSoft.Sketch.ViewModel.DataTransfer.Element.Primitive;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Tools.Model.Logic;
using PeletonSoft.Tools.Model.Memento;
using PeletonSoft.Tools.Model.ObjectEvent.NotifyChanged;
using static PeletonSoft.Tools.Model.ObjectEvent.NotifyChanged.NotifyPropertyChangedHelper;


namespace PeletonSoft.Sketch.ViewModel.Element.Primitive
{
    public sealed class DecorativeBorderViewModel : IOriginator, INotifyViewModel<DecorativeBorder>, IOriginator<DecorativeBorderDataTransfer>
    {
        #region implement INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName) =>
            this.OnPropertyChanged(PropertyChanged, propertyName);

        private void SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null) =>
            SetFieldValue(() => OnPropertyChanged(propertyName), ref field, value);

        private void SetField<T>(Func<T> getValue, Action<T> setValue, T value, [CallerMemberName] string propertyName = null) =>
            SetFieldValue(() => OnPropertyChanged(propertyName), getValue, setValue, value);
        #endregion

        #region implement IOriginator
        public void RestoreDefault()
        {
            ResetChains();
        }
        #endregion

        #region implement IViewModel
        public DecorativeBorder Model { get; }
        #endregion

        #region implement IOriginator
        public DecorativeBorderDataTransfer CreateState() => new DecorativeBorderDataTransfer();
        public void Save(DecorativeBorderDataTransfer state)
        {
            state.Width = Width;
            state.Height = Height;
            state.Points.Clear();
            state.Points.AddRange(Points);
        }

        public void Restore(DecorativeBorderDataTransfer state)
        {
            Width = state.Width;
            Height = state.Height;
            Points = new List<Point>(state.Points);
        }
        #endregion
        public DecorativeBorderViewModel(IWorkspaceBit workspaceBit, DecorativeBorder model)
        {
            WorkspaceBit = workspaceBit;
            Model = model;
            IsEdited = false;
        }
         
        private IWorkspaceBit WorkspaceBit { get; set; }

        public double Width
        {
            get { return Model.Width; }
            set { SetField(() => Model.Width, v => Model.Width = v, value); }
        }

        public double Height
        {
            get { return Model.Height; }
            set { SetField(() => Model.Height, v => Model.Height = v, value); }
        }

        private bool _isEdited;
        public bool IsEdited
        {
            get { return _isEdited; }
            set { SetField(ref _isEdited, value); }
        }

        public IEnumerable<Point> Points
        {
            get { return Model.Points; }
            set { SetField(() => Model.Points, v => Model.Points = v, value); }
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

