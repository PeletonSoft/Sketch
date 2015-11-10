using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using PeletonSoft.Sketch.ViewModel.DataTransfer.Geometry;
using PeletonSoft.Tools.Model.Memento;
using PeletonSoft.Tools.Model.ObjectEvent.NotifyChanged;
using static PeletonSoft.Tools.Model.ObjectEvent.EventAction;
using static PeletonSoft.Tools.Model.ObjectEvent.NotifyChanged.NotifyPropertyChangedHelper;

namespace PeletonSoft.Sketch.ViewModel.Geometry
{
    public class SuperimposeOptionViewModel : IOriginator, INotifyPropertyChanged, IOriginator<SuperimposeOptionDataTransfer>
    {
        #region implement INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName) =>
            this.OnPropertyChanged(PropertyChanged, propertyName);

        private void SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null) =>
            SetFieldValue(() => OnPropertyChanged(propertyName), ref field, value);

        #endregion

        #region implement IOriginator

        public void RestoreDefault() => DoNothing();
        #endregion

        private double _foregroundOpacity;
        public double ForegroundOpacity
        {
            get { return _foregroundOpacity; }
            set { SetField(ref _foregroundOpacity, value); }
        }

        private double _backgroundOpacity;
        public double BackgroundOpacity
        {
            get { return _backgroundOpacity; }
            set { SetField(ref _backgroundOpacity, value); }
        }

        private double _markerOpacity;
        public double MarkerOpacity
        {
            get { return _markerOpacity; }
            set { SetField(ref _markerOpacity, value); }
        }

        private double _markerRadius;
        public double MarkerRadius
        {
            get { return _markerRadius; }
            set { SetField(ref _markerRadius, value); }
        }

        public SuperimposeOptionViewModel()
        {
            ForegroundOpacity = 0.2;
            BackgroundOpacity = 0.9;
            MarkerOpacity = 0.3;
            MarkerRadius = 8;
        }

        public SuperimposeOptionDataTransfer CreateState() => new SuperimposeOptionDataTransfer();

        public void Save(SuperimposeOptionDataTransfer state)
        {
            state.BackgroundOpacity = BackgroundOpacity;
            state.ForegroundOpacity = ForegroundOpacity;
            state.MarkerOpacity = MarkerOpacity;
            state.MarkerRadius = MarkerRadius;
        }

        public void Restore(SuperimposeOptionDataTransfer state)
        {
            BackgroundOpacity = state.BackgroundOpacity;
            ForegroundOpacity = state.ForegroundOpacity;
            MarkerOpacity = state.MarkerOpacity;
            MarkerRadius = state.MarkerRadius;
        }
    }
}
