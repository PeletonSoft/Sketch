using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using PeletonSoft.Sketch.Model.Interface.Element;
using PeletonSoft.Sketch.ViewModel.DataTransfer.Element.Primitive;
using PeletonSoft.Sketch.ViewModel.DataTransfer.Interface;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Sketch.ViewModel.Interface.Element;
using PeletonSoft.Tools.Model.Logic;
using PeletonSoft.Tools.Model.Memento;
using PeletonSoft.Tools.Model.ObjectEvent.NotifyChanged;
using static PeletonSoft.Tools.Model.ObjectEvent.EventAction;
using static PeletonSoft.Tools.Model.ObjectEvent.NotifyChanged.NotifyPropertyChangedHelper;


namespace PeletonSoft.Sketch.ViewModel.Element.Primitive
{
    public sealed class ClotheViewModel : IClotheViewModel, INotifyViewModel<IClothe>, IOriginator<IClotheDataTransfer>
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

        #region implement IViewModel
        public IClothe Model { get; }
        #endregion

        public double? Width
        {
            get { return Model.Width; }
            set { SetField(() => Model.Width, v => Model.Width = v, value); }
        }

        public double? Height
        {
            get { return Model.Height; }
            set { SetField(() => Model.Height, v => Model.Height = v, value); }
        }

        public double? Area => Model.Area;

        public void Calculate()
        {
            Model.Calculate();
            OnPropertyChanged(nameof(Width));
            OnPropertyChanged(nameof(Height));
        }

        public ICommand CalculateCommand { get; }

        public ClotheViewModel(IWorkspaceBit workspaceBit, IClothe model)
        {
            Model = model;
            this.SetPropertyChanged(
                new[]
                {
                    nameof(Width), nameof(Height)
                },
                () => OnPropertyChanged(nameof(Area)));
            var commandFactory = workspaceBit.CommandFactory;
            CalculateCommand = commandFactory.CreateCommand(Calculate);
        }

        public IClotheDataTransfer CreateState() => new ClotheDataTransfer();
        public void Save(IClotheDataTransfer state)
        {
            state.Width = Width;
            state.Height = Height;
        }
        public void Restore(IClotheDataTransfer state)
        {
            Width = state.Width;
            Height = state.Height;
        }
    }
}
