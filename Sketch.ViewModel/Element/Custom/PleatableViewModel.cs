using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using PeletonSoft.Sketch.Model.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Element.Layout;
using PeletonSoft.Sketch.ViewModel.Element.Null;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Sketch.ViewModel.Interface.Element;
using PeletonSoft.Sketch.ViewModel.Interface.Layout;
using PeletonSoft.Tools.Model;
using PeletonSoft.Tools.Model.Logic;
using PeletonSoft.Tools.Model.ObjectEvent;
using PeletonSoft.Tools.Model.ObjectEvent.NotifyChanged;
using PeletonSoft.Tools.Model.SketchMath.Wave;
using static PeletonSoft.Tools.Model.ObjectEvent.EventAction;
using static PeletonSoft.Tools.Model.ObjectEvent.NotifyChanged.NotifyPropertyChangedHelper;


namespace PeletonSoft.Sketch.ViewModel.Element.Custom
{
    public abstract class PleatableViewModel : IElementViewModel, INotifyViewModel<Pleatable>
    {
        #region implement INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName) =>
            this.OnPropertyChanged(PropertyChanged, propertyName);

        protected void SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null) =>
            SetFieldValue(() => OnPropertyChanged(propertyName), ref field, value);

        protected void SetField<T>(Func<T> getValue, Action<T> setValue, T value, [CallerMemberName] string propertyName = null) =>
            SetFieldValue(() => OnPropertyChanged(propertyName), getValue, setValue, value);

        #endregion

        #region implement IViewModel
        public Pleatable Model { get; }
        #endregion

        #region implement IOriginator
        public void RestoreDefault() => DoNothing();
        #endregion

        #region implment ICollectionItem
        public virtual void AfterInsert()
        {
            ChangeSheet();
            DenseWidth = Math.Min(0.6 * Sheet.Height, 0.6 * Sheet.Width);
        }

        public virtual void BeforeDelete()
        {
            WorkspaceBit.RenderChangedDispatcher.Unsubscribe(this, Sheet);
            Sheet = NullSheet;
        }
        #endregion

        #region implement IElementViewModel
        public string Description
        {
            get { return Model.Description; }
            set { SetField(() => Description, v => Model.Description = v, value); }
        }
        public bool Visibility
        {
            get { return Model.Visibility; }
            set { SetField(() => Visibility, v => Model.Visibility = v, value); }
        }
        public double Opacity
        {
            get { return Model.Opacity; }
            set { SetField(() => Opacity, v => Model.Opacity = v, value); }
        }

        public ICommand MoveToElementCommand { get; set; }

        public IReadOnlyList<IElementViewModel> Below => WorkspaceBit.GetBelowElements(this);
        #endregion

        protected IWorkspaceBit WorkspaceBit { get; }

        private ISheetElementViewModel NullSheet { get; } = new NullSheetViewModel();
        private ISheetElementViewModel GetSheet()
        {
            var sheet = Below
                .OfType<ISheetElementViewModel>()
                .Reverse()
                .FirstOrDefault();
            return sheet ?? NullSheet;
        }

        private ISheetElementViewModel _sheet;
        public ISheetElementViewModel Sheet
        {
            get { return _sheet; }
            private set { SetField(ref _sheet, value); }
        }

        private void ChangeSheet()
        {
            var newSheet = GetSheet();

            if (Sheet != newSheet)
            {
                WorkspaceBit.RenderChangedDispatcher.Unsubscribe(this, Sheet);
                Sheet = newSheet;
                NotifyRenderChangedAction = WorkspaceBit.RenderChangedDispatcher
                    .Subscribe(this, Sheet, () => Model.GetRenderArea());
            }
        }

        private Action NotifyRenderChangedAction { get; set; }
        private void NotifyRenderChanged()
        {
            NotifyRenderChangedAction?.Invoke();
        }

        public IWavyBorder<IEnumerable<Point>> WavySurface => Model.GetWavySurface();

        public ElementAlignment Alignment
        {
            get { return Model.Alignment; }
            set { SetField(() => Model.Alignment, v => Model.Alignment = v, value); }
        }

        public double DenseWidth
        {
            get { return Model.DenseWidth; }
            set { SetField(() => Model.DenseWidth, v => Model.DenseWidth = v, value); }
        }
        public int WaveCount
        {
            get { return Model.WaveCount; }
            set { SetField(() => Model.WaveCount, v => Model.WaveCount = v, value); }
        }

        protected PleatableViewModel(IWorkspaceBit workspaceBit, Pleatable model)
        {
            WorkspaceBit = workspaceBit;
            Model = model;

            this.SetPropertyChanged(nameof(Sheet), () => Model.Sheet = Sheet.Model);
            Sheet = NullSheet;
            Layout = new PleatableLayoutViewModel(WorkspaceBit, this);

            Visibility = true;
            Opacity = 1;
            Alignment = ElementAlignment.Left;
            WaveCount = 5;

            workspaceBit.ItemChanged += (sender, args) => ChangeSheet();

            Action sheetChange =
                () =>
                {
                    OnPropertyChanged(nameof(WavySurface));
                    OnPropertyChanged(nameof(Rect));
                    NotifyRenderChanged();
                };

            this
                .SetPropertyChanged(nameof(Visibility), NotifyRenderChanged)
                .SetPropertyChanged(
                    new[]
                    {
                        nameof(Sheet), nameof(Alignment),
                        nameof(DenseWidth), nameof(WaveCount)
                    },
                    () => OnPropertyChanged(nameof(WavySurface)))
                .SetPropertyChanged(
                    this.ExtractGetter(nameof(Sheet), el => el.Sheet),
                    new[]
                    {
                        nameof(Sheet.Layout), nameof(Sheet.Width),
                        nameof(Sheet.OffsetX), nameof(Sheet.OffsetY)
                    },
                    sheetChange);
        }

        public ILayoutViewModel Layout { get; }
        public Rect Rect => Model.GetRect();
    }
}
