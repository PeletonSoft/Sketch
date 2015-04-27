using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using PeletonSoft.Tools.Model.Draw;
using PeletonSoft.Tools.Model.Memento;
using PeletonSoft.Tools.Model.NotifyChanged;
using PeletonSoft.Tools.Model.SketchMath.Wave;

namespace PeletonSoft.Sketch.ViewModel.Element.Primitive
{
    public class TieBackSideViewModel : INotifyPropertyChanged, IOriginator
    {
        #region implement INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            this.OnPropertyChanged(PropertyChanged, propertyName);
        }

        private void SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            Action notificator = () => OnPropertyChanged(propertyName);
            notificator.SetField(ref field, value);
        }

        #endregion

        private double _tailScatter;
        public double TailScatter
        {
            get { return _tailScatter; }
            set { SetField(ref _tailScatter, value); }
        }

        private double _weight;
        public double Weight
        {
            get { return _weight; }
            set { SetField(ref _weight, value); }
        }

        public TieBackSideViewModel()
        {
            TailScatter = 0;
            Weight = 0.1;
        }

        public double GetLength(IWave<IEnumerable<Point>> wave, Point point)
        {
            var chain = wave.Peak.ToList();
            var min = chain.First().DistanceTo(point);
            var max = chain.Length();
            return min + Weight*(max - min);
        }

        public void RestoreDefault()
        {
        }
    }
}
