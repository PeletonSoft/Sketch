using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using PeletonSoft.Tools.Model.Memento;
using PeletonSoft.Tools.Model.NotifyChanged;

namespace PeletonSoft.Sketch.ViewModel.Geometry
{
    public class RectangleViewModel : INotifyPropertyChanged, IOriginator
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

        private VertexViewModel _topLeft;
        public VertexViewModel TopLeft
        {
            get { return _topLeft; }
            set { SetField(ref _topLeft, value); }
        }

        private VertexViewModel _topRight;
        public VertexViewModel TopRight
        {
            get { return _topRight; }
            set { SetField(ref _topRight, value); }
        }

        private VertexViewModel _bottomLeft;
        public VertexViewModel BottomLeft
        {
            get { return _bottomLeft; }
            set { SetField(ref _bottomLeft, value); }
        }

        private VertexViewModel _bottomRight;
        public VertexViewModel BottomRight
        {
            get { return _bottomRight; }
            set { SetField(ref _bottomRight, value); }
        }

        public virtual IEnumerable<Point> Points
        {
            get
            {
                return new[] { TopLeft, TopRight, BottomRight, BottomLeft }
                    .Select(v => v.Point);
            }
        }


    }
}