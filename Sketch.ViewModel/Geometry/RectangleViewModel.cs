using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using PeletonSoft.Sketch.ViewModel.DataTransfer.Geometry;
using PeletonSoft.Tools.Model.Memento;
using PeletonSoft.Tools.Model.ObjectEvent;
using PeletonSoft.Tools.Model.ObjectEvent.NotifyChanged;
using static PeletonSoft.Tools.Model.ObjectEvent.EventAction;
using static PeletonSoft.Tools.Model.ObjectEvent.NotifyChanged.NotifyPropertyChangedHelper;

namespace PeletonSoft.Sketch.ViewModel.Geometry
{
    public abstract class RectangleViewModel : INotifyPropertyChanged, IOriginator, IOriginator<RectangleDataTransfer>
    {

        #region implement INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName) =>
            this.OnPropertyChanged(PropertyChanged, propertyName);

        protected void SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null) =>
            SetFieldValue(() => OnPropertyChanged(propertyName), ref field, value);
        #endregion

        #region implement IOriginator
        public virtual void RestoreDefault() => DoNothing();

        #endregion

        public VertexViewModel TopLeft { get; }
        public VertexViewModel TopRight { get; }
        public VertexViewModel BottomLeft { get; }
        public VertexViewModel BottomRight { get; }


        public IEnumerable<VertexViewModel> Vertices => new[] {TopLeft, TopRight, BottomRight, BottomLeft};

        protected RectangleViewModel()
        {
            TopLeft = new VertexViewModel(0, 0);
            BottomRight = new VertexViewModel(0, 0);
            TopRight = new VertexViewModel(0, 0);
            BottomLeft = new VertexViewModel(0, 0);

            this.PropertyIterate(
                new[]
                {
                    this.ExtractGetter(nameof(TopLeft), el => el.TopLeft),
                    this.ExtractGetter(nameof(TopRight), el => el.TopRight),
                    this.ExtractGetter(nameof(BottomLeft), el => el.BottomLeft),
                    this.ExtractGetter(nameof(BottomRight), el => el.BottomRight)
                },
                (vertex, propertyName) =>
                    vertex.SetPropertyChanged(
                        new[] {nameof(vertex.X), nameof(vertex.Y)},
                        () => OnPropertyChanged(propertyName)));
        }


        public RectangleDataTransfer CreateState() => new RectangleDataTransfer();

        public void Save(RectangleDataTransfer state)
        {
            state.TopLeft = TopLeft.Save();
            state.TopRight = TopRight.Save();
            state.BottomLeft = BottomLeft.Save();
            state.BottomRight = BottomRight.Save();
        }

        public void Restore(RectangleDataTransfer state)
        {
            TopLeft.Restore(state.TopLeft);
            BottomRight.Restore(state.BottomRight);
            TopRight.Restore(state.TopRight);
            BottomLeft.Restore(state.BottomLeft);
        }
    }
}