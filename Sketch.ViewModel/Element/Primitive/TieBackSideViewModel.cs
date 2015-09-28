using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using PeletonSoft.Sketch.Model.Element.Primitive;
using PeletonSoft.Tools.Model.Logic;
using PeletonSoft.Tools.Model.Memento;
using PeletonSoft.Tools.Model.NotifyChanged;

namespace PeletonSoft.Sketch.ViewModel.Element.Primitive
{
    public sealed class TieBackSideViewModel : INotifyPropertyChanged, IOriginator, IViewModel<TieBackSide>
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

        private bool SetField<T>(Func<T> getValue, Action<T> setValue, T value, [CallerMemberName] string propertyName = null)
        {
            Action notificator = () => OnPropertyChanged(propertyName);
            return notificator.SetField(getValue, setValue, value);
        }


        #endregion

        #region implement IOriginator
        public void RestoreDefault()
        {
        }
        #endregion

        #region implement IViewModel
        public TieBackSide Model { get; private set; }
        #endregion

        public double TailScatter
        {
            get { return Model.TailScatter; }
            set { SetField(() => Model.TailScatter, v => Model.TailScatter = v, value); }
        }
        public double Weight
        {
            get { return Model.Weight; }
            set { SetField(() => Model.Weight, v => Model.Weight = v, value); }
        }
        
        public TieBackSideViewModel(TieBackSide model)
        {
            Model = model;
            TailScatter = 0;
            Weight = 0.1;
        }
    }
}
