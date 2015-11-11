using System;
using System.ComponentModel;
using System.Windows.Input;
using PeletonSoft.Sketch.ViewModel.Interface.Draw;
using PeletonSoft.Tools.Model.Dependency;
using PeletonSoft.Tools.Model.Dragable;
using PeletonSoft.Tools.Model.ObjectEvent.NotifyChanged;

namespace PeletonSoft.Sketch.ViewModel.Geometry.DecorativeBorder
{
    public class LineViewModel : ILineViewModel
    {
        #region implement INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            this.OnPropertyChanged(PropertyChanged, propertyName);
        }
        #endregion

        private IPointViewModel _start;
        public IPointViewModel Start
        {
            get { return _start; }
            set
            {
                if (value != _start)
                {
                    _start = value;
                    OnPropertyChanged(nameof(Start));
                    OnPropertyChanged(nameof(Offset));
                    OnPropertyChanged(nameof(X));
                    OnPropertyChanged(nameof(Y));
                    _start.SetPropertyChanged(
                        new[] {nameof(_start.X), nameof(_start.Y)},
                        () =>
                        {
                            OnPropertyChanged(nameof(X));
                            OnPropertyChanged(nameof(Y));
                            OnPropertyChanged(nameof(Offset));
                        });
                }
            }
        }

        private IPointViewModel _finish;
        public IPointViewModel Finish
        {
            get { return _finish; }
            set
            {
                if (value != _finish)
                {
                    _finish = value;
                    OnPropertyChanged(nameof(Finish));
                    OnPropertyChanged(nameof(Offset));
                    _finish.SetPropertyChanged(
                        new[] {nameof(_finish.X), nameof(_finish.Y)},
                        () =>
                        {
                            OnPropertyChanged(nameof(X));
                            OnPropertyChanged(nameof(Y));
                            OnPropertyChanged(nameof(Offset));
                        });
                }
            }
        }

        public LineViewModel(IPointViewModel start, IPointViewModel finish,
            Action<PointTransit<ILineViewModel>> insertAction, ICommandFactory commandFactory)
        {
            Start = start;
            Finish = finish;
            InsertCommand = commandFactory.CreateCommand(o => insertAction(((PointTransit) o).Cast<ILineViewModel>()));
        }


        public double X
        {
            get { return Start.X; }
            set { }
        }

        public double Y
        {
            get { return Start.Y; }
            set { }
        }

        public VertexViewModel Offset => new VertexViewModel(Finish.X - Start.X, Finish.Y - Start.Y);

        public ICommand InsertCommand { get; private set; }

    }

}
