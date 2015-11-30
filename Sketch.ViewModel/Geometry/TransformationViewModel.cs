using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using PeletonSoft.Sketch.ViewModel.Container;
using PeletonSoft.Sketch.ViewModel.DataTransfer.Geometry;
using PeletonSoft.Sketch.ViewModel.Interface.Geometry;
using PeletonSoft.Tools.Model.Collection;
using PeletonSoft.Tools.Model.Memento;
using PeletonSoft.Tools.Model.ObjectEvent.NotifyChanged;
using static PeletonSoft.Tools.Model.ObjectEvent.EventAction;
using static PeletonSoft.Tools.Model.ObjectEvent.NotifyChanged.NotifyPropertyChangedHelper;

namespace PeletonSoft.Sketch.ViewModel.Geometry
{
    public class TransformationViewModel : INotifyPropertyChanged, IReflectableViewModel, IRotableViewModel, 
        IOriginator<TransformationDataTransfer>
    {
        #region implement INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName) =>
            this.OnPropertyChanged(PropertyChanged, propertyName);

        private void SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null) =>
            SetFieldValue(() => OnPropertyChanged(propertyName), ref field, value);

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

        public IContainer<IRotationViewModel> Rotations => _lazyRotations.Value;

        public IContainer<IReflectionViewModel> Reflections => _lazyReflections.Value;

        public TransformationViewModel()
        {
            Rotation = Rotations.Default;
            Reflection = Reflections.Default;
        }

        public TransformationDataTransfer CreateState() => new TransformationDataTransfer();

        public void Save(TransformationDataTransfer state)
        {
            state.Reflection = Reflections.GetKeyByValue(Reflection);
            state.Rotation = Rotations.GetKeyByValue(Rotation);
        }

        public void Restore(TransformationDataTransfer state)
        {
            Reflection = Reflections.GetValueByKeyOrDefault(state.Reflection);
            Rotation = Rotations.GetValueByKeyOrDefault(state.Rotation);
        }
    }
}
