using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
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
using PeletonSoft.Tools.Model.NotifyChanged;
using PeletonSoft.Tools.Model.SketchMath.Wave;

namespace PeletonSoft.Sketch.ViewModel.Element.Custom
{
    public abstract class PleatableViewModel : IElementViewModel, INotifyViewModel<Pleatable>
    {
        #region implement INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            this.OnPropertyChanged(PropertyChanged, propertyName);
        }

        private void SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            Action notificator = () => OnPropertyChanged(propertyName);
            notificator.SetField(ref field, value);
        }

        protected void SetField<T>(Func<T> getValue, Action<T> setValue, T value, [CallerMemberName] string propertyName = null)
        {
            Action notificator = () => OnPropertyChanged(propertyName);
            notificator.SetField(getValue, setValue, value);
        }

        private void OnPropertyChanged<T>(Expression<Func<PleatableViewModel, T>> expression)
        {
            expression.OnPropertyChanged(OnPropertyChanged);
        }

        #endregion

        #region implement IViewModel
        public Pleatable Model { get; private set; }
        #endregion

        #region implement IOriginator
        public void RestoreDefault()
        {
        }
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
            Sheet = _nullSheet;
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

        public IReadOnlyList<IElementViewModel> Below
        {
            get { return WorkspaceBit.GetBelowElements(this); }
        }
        #endregion

        protected IWorkspaceBit WorkspaceBit { get; private set; }

        private readonly ISheetElementViewModel _nullSheet;
        private ISheetElementViewModel GetSheet()
        {
            var sheet = Below
                .OfType<ISheetElementViewModel>()
                .Reverse()
                .FirstOrDefault();
            return sheet ?? _nullSheet;
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
                _notifyRenderChanged = WorkspaceBit.RenderChangedDispatcher
                    .Subscribe(this, Sheet, () => Model.GetRenderArea());
            }
        }

        private Action _notifyRenderChanged;
        private void NotifyRenderChanged()
        {
            if (_notifyRenderChanged != null)
            {
                _notifyRenderChanged();
            }
        }

        public IWavyBorder<IEnumerable<Point>> WavySurface
        {
            get { return Model.GetWavySurface(); }
        }

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

        public PleatableViewModel(IWorkspaceBit workspaceBit, Pleatable model)
        {
            WorkspaceBit = workspaceBit;
            Model = model;

            this.SetPropertyChanged(el => el.Sheet, () => Model.Sheet = Sheet.Model);
            _nullSheet = new NullSheetViewModel();
            Sheet = _nullSheet;
            Layout = new PleatableLayoutViewModel(WorkspaceBit, this);

            Visibility = true;
            Opacity = 1;
            Alignment = ElementAlignment.Left;
            WaveCount = 5;

            workspaceBit.ItemChanged += (sender, args) => ChangeSheet();

            Action sheetChange =
                () =>
                {
                    OnPropertyChanged(el => el.WavySurface);
                    OnPropertyChanged(el => el.Rect);
                    NotifyRenderChanged();
                };

            this
                .SetPropertyChanged(el => el.Visibility, NotifyRenderChanged)
                .SetPropertyChanged(
                    new[]
                    {
                        this.GetPropertyName(el => el.Sheet),
                        this.GetPropertyName(el => el.Alignment),
                        this.GetPropertyName(el => el.DenseWidth),
                        this.GetPropertyName(el => el.WaveCount)
                    },
                    () => OnPropertyChanged(el => el.WavySurface))
                .SetPropertyChanged(el => el.Sheet, sh => sh.Width, sheetChange)
                .SetPropertyChanged(el => el.Sheet, sh => sh.Height, sheetChange)
                .SetPropertyChanged(el => el.Sheet, sh => sh.OffsetX, sheetChange)
                .SetPropertyChanged(el => el.Sheet, sh => sh.OffsetY, sheetChange)
                .SetPropertyChanged(el => el.Sheet, sh => sh.Layout, sheetChange);
        }

        public ILayoutViewModel Layout { get; private set; }

        public Rect Rect
        {
            get { return Model.GetRect(); }
        }

    }
}
