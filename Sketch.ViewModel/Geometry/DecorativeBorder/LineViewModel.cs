using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using PeletonSoft.Tools.Model.Dependency;
using PeletonSoft.Tools.Model.Dragable;
using PeletonSoft.Tools.Model.Draw;
using PeletonSoft.Tools.Model.NotifyChanged;

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

        private bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            Action notificator = () => OnPropertyChanged(propertyName);
            return notificator.SetField(ref field, value);
        }

        private void OnPropertyChanged<T>(Expression<Func<LineViewModel, T>> expression)
        {
            expression.OnPropertyChanged(OnPropertyChanged);
        }
        #endregion

        #region implement IOriginator

        public void RestoreDefault()
        {

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
                    OnPropertyChanged(l => l.Start);
                    OnPropertyChanged(l => l.Offset);
                    OnPropertyChanged(l => l.X);
                    OnPropertyChanged(l => l.Y);
                    _start.SetPropertyChanged(
                        new[]
                        {
                            _start.GetPropertyName(p => p.X),
                            _start.GetPropertyName(p => p.Y),
                        },
                        () =>
                        {
                            OnPropertyChanged(l => l.X);
                            OnPropertyChanged(l => l.Y);
                            OnPropertyChanged(l => l.Offset);
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
                    OnPropertyChanged(l => l.Finish);
                    OnPropertyChanged(l => l.Offset);
                    _finish.SetPropertyChanged(
                        new[]
                        {
                            _finish.GetPropertyName(p => p.X),
                            _finish.GetPropertyName(p => p.Y),
                        },
                        () =>
                        {
                            OnPropertyChanged(l => l.X);
                            OnPropertyChanged(l => l.Y);
                            OnPropertyChanged(l => l.Offset);
                        });
                }
            }
        }

        public LineViewModel(IPointViewModel start, IPointViewModel finish,
            Action<InsertPointTransit> insertAction, ICommandFactory commandFactory)
        {
            Start = start;
            Finish = finish;
            InsertCommand = commandFactory.CreateCommand(o => insertAction((InsertPointTransit) o));
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

        public VertexViewModel Offset
        {
            get { return new VertexViewModel(Finish.X - Start.X, Finish.Y - Start.Y); }
        }

        public ICommand InsertCommand { get; private set; }

    }

}
