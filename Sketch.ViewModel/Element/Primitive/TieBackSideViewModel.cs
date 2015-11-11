using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using PeletonSoft.Sketch.Model.Element.Primitive;
using PeletonSoft.Sketch.ViewModel.DataTransfer.Element.Primitive;
using PeletonSoft.Tools.Model.Logic;
using PeletonSoft.Tools.Model.Memento;
using PeletonSoft.Tools.Model.ObjectEvent.NotifyChanged;
using static PeletonSoft.Tools.Model.ObjectEvent.EventAction;
using static PeletonSoft.Tools.Model.ObjectEvent.NotifyChanged.NotifyPropertyChangedHelper;

namespace PeletonSoft.Sketch.ViewModel.Element.Primitive
{
    public sealed class TieBackSideViewModel : INotifyViewModel<TieBackSide>, IOriginator, IOriginator<TieBackSideDataTransfer>
    {
        #region implement INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName) => 
            this.OnPropertyChanged(PropertyChanged, propertyName);

        private void SetField<T>(Func<T> getValue, Action<T> setValue, T value, [CallerMemberName] string propertyName = null) => 
            SetFieldValue(() => OnPropertyChanged(propertyName), getValue, setValue, value);

        #endregion

        #region implement IOriginator
        public void RestoreDefault() => DoNothing();
        #endregion

        #region implement IViewModel
        public TieBackSide Model { get; }
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

        public TieBackSideDataTransfer CreateState() => new TieBackSideDataTransfer();
        public void Save(TieBackSideDataTransfer state)
        {
            state.Weight = Weight;
            state.TailScatter = TailScatter;
        }
        public void Restore(TieBackSideDataTransfer state)
        {
            Weight = state.Weight;
            TailScatter = state.TailScatter;
        }
    }
}
