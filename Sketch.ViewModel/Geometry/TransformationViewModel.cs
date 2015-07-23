using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using PeletonSoft.Sketch.ViewModel.Container;
using PeletonSoft.Sketch.ViewModel.Interface.Geometry;
using PeletonSoft.Tools.Model.Collection;
using PeletonSoft.Tools.Model.Memento;
using PeletonSoft.Tools.Model.Memento.Container;
using PeletonSoft.Tools.Model.NotifyChanged;

namespace PeletonSoft.Sketch.ViewModel.Geometry
{
    public class TransformationViewModel : INotifyPropertyChanged, IOriginator, IReflectableViewModel, IRotableViewModel
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

        }
        #endregion

        private IRotationViewModel _rotation;
        public IRotationViewModel Rotation
        {
            get { return _rotation; }
            set { SetField(ref _rotation, value); }
        }

        private IReflectionViewModel _reflection;

        public IReflectionViewModel Reflection
        {
            get { return _reflection; }
            set { SetField(ref _reflection, value); }
        }

        private readonly Lazy<RotationViewModels> _lazyRotations =
            new Lazy<RotationViewModels>(() => new RotationViewModels());

        private readonly Lazy<ReflectionViewModels> _lazyReflections =
            new Lazy<ReflectionViewModels>(() => new ReflectionViewModels());

        public IContainer<IRotationViewModel> Rotations
        {
            get { return _lazyRotations.Value; }
        }

        public IContainer<IReflectionViewModel> Reflections
        {
            get { return _lazyReflections.Value; }
        }

        public TransformationViewModel()
        {
            Rotation = Rotations.Default;
            Reflection = Reflections.Default;
        }
    }
}
