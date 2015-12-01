using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using PeletonSoft.Sketch.Model;
using PeletonSoft.Sketch.ViewModel.DataTransfer;
using PeletonSoft.Sketch.ViewModel.DataTransfer.Interface;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Tools.Model.Logic;
using PeletonSoft.Tools.Model.Memento;
using PeletonSoft.Tools.Model.ObjectEvent.NotifyChanged;
using static PeletonSoft.Tools.Model.ObjectEvent.NotifyChanged.NotifyPropertyChangedHelper;

namespace PeletonSoft.Sketch.ViewModel
{
    public sealed class ScreenViewModel : IScreenViewModel, IViewModel<Screen>, 
        IOriginator<ScreenDataTransfer>
    {
        #region implement INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName) =>
            this.OnPropertyChanged(PropertyChanged, propertyName);

        private void SetField<T>(Func<T> getValue, Action<T> setValue, T value, [CallerMemberName] string propertyName = null) =>
            SetFieldValue(() => OnPropertyChanged(propertyName), getValue, setValue, value);

        #endregion

        public Screen Model { get; }

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

        public ScreenViewModel()
        {
            Model = new Screen();
        }

        public ScreenDataTransfer CreateState()
        {
            return new ScreenDataTransfer();
        }

        public void Save(ScreenDataTransfer state)
        {
            state.Width = Width;
            state.Height = Height;
        }

        public void Restore(ScreenDataTransfer state)
        {
            Width = state.Width;
            Height = state.Height;
        }

        IScreenDataTransfer IOriginator<IScreenDataTransfer>.CreateState()
        {
            return CreateState();
        }

        void IOriginator<IScreenDataTransfer>.Save(IScreenDataTransfer state)
        {
            Save((ScreenDataTransfer)state);
        }

        void IOriginator<IScreenDataTransfer>.Restore(IScreenDataTransfer state)
        {
            Restore((ScreenDataTransfer)state);
        }

    }
}
