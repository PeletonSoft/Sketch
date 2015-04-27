using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using PeletonSoft.Tools.Model;
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

        #endregion

        #region implement IOriginator

        public void RestoreDefault()
        {

        }

        #endregion

        private IPointViewModel _start;

        public IPointViewModel Start
        {
            get
            {
                return _start;
            }
            set
            {
                if (value != _start)
                {
                    _start = value;
                    OnPropertyChanged("Start");
                    OnPropertyChanged("Offset");
                    OnPropertyChanged("X");
                    OnPropertyChanged("Y");
                    _start.PropertyChanged +=
                        (sender, args) =>
                        {
                            OnPropertyChanged("X");
                            OnPropertyChanged("Y");
                            OnPropertyChanged("Offset");
                        };
                }
            }
        }

        private IPointViewModel _finish;
        public IPointViewModel Finish
        {
            get
            {
                return _finish;
            }
            set
            {
                if (value != _finish)
                {
                    _finish = value;
                    OnPropertyChanged("Finish");
                    OnPropertyChanged("Offset");
                    _finish.PropertyChanged +=
                        (sender, args) =>
                        {
                            OnPropertyChanged("Offset");
                            OnPropertyChanged("X");
                            OnPropertyChanged("Y");
                        };
                }
            }
        }

        public LineViewModel(IPointViewModel start, IPointViewModel finish, 
            Action<InsertPointTransit> insertAction, ICommandFactory commandFactory)
        {
            Start = start;
            Finish = finish;
            InsertCommand = commandFactory.CreateCommand(o=>insertAction((InsertPointTransit)o));
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
