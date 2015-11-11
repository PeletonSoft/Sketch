using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using PeletonSoft.Sketch.ViewModel.DataTransfer.Geometry;
using PeletonSoft.Tools.Model.Memento;
using PeletonSoft.Tools.Model.ObjectEvent.NotifyChanged;
using PeletonSoft.Tools.Model.SketchMath;
using static PeletonSoft.Tools.Model.ObjectEvent.EventAction;
using static PeletonSoft.Tools.Model.ObjectEvent.NotifyChanged.NotifyPropertyChangedHelper;


namespace PeletonSoft.Sketch.ViewModel.Geometry
{
    public class VertexViewModel : INotifyPropertyChanged, IOriginator, IPoint, IOriginator<VertexDataTransfer>
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

        private double _x;
        public double X
        {
            get { return _x; }
            set { SetField(ref _x, value); }
        }

        private double _y;
        public double Y
        {
            get { return _y; }
            set { SetField(ref _y, value); }
        }

        public VertexViewModel(double x, double y)
        {
            X = x;
            Y = y;
        }

        public VertexViewModel(Point point)
        {
            Point = point;
        }
        public Point Point
        {
            get { return new Point(X, Y); }
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }

        public VertexDataTransfer CreateState() => new VertexDataTransfer();

        public void Save(VertexDataTransfer state)
        {
            state.X = X;
            state.Y = Y;
        }

        public void Restore(VertexDataTransfer state)
        {
            X = state.X;
            Y = state.Y;
        }
    }

}
