using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using PeletonSoft.Tools.Model.Memento;
using PeletonSoft.Tools.Model.NotifyChanged;

namespace PeletonSoft.Sketch.ViewModel.Geometry
{
    public abstract class RectangleViewModel : INotifyPropertyChanged, IOriginator
    {

        #region implement INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            this.OnPropertyChanged(PropertyChanged, propertyName);
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            Action notificator = () => OnPropertyChanged(propertyName);
            return notificator.SetField(ref field, value);
        }

        #endregion

        #region implement IOriginator

        public virtual void RestoreDefault()
        {
        }

        #endregion

        public VertexViewModel TopLeft { get; private set; }
        public VertexViewModel TopRight { get; private set; }
        public VertexViewModel BottomLeft { get; private set; }
        public VertexViewModel BottomRight { get; private set; }


        public IEnumerable<VertexViewModel> Vertices
        {
            get { return new[] {TopLeft, TopRight, BottomRight, BottomLeft}; }
        }

        protected RectangleViewModel()
        {
            TopLeft = new VertexViewModel(0, 0);
            BottomRight = new VertexViewModel(0, 0);
            TopRight = new VertexViewModel(0, 0);
            BottomLeft = new VertexViewModel(0, 0);

            this.PropertyIterate(
                new Expression<Func<RectangleViewModel, VertexViewModel>>[]
                {
                    el => el.TopLeft, el => el.TopRight,
                    el => el.BottomLeft, el => el.BottomRight
                },
                (vertex, propertyName) =>
                    vertex.SetPropertyChanged(
                        new Expression<Func<VertexViewModel, double>>[] {v => v.X, v => v.Y},
                        () => OnPropertyChanged(propertyName)));
        }
    }
}