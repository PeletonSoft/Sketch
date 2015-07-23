using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using PeletonSoft.Sketch.Model.ClotheStrategy;
using PeletonSoft.Sketch.Model.Element;
using PeletonSoft.Sketch.Model.Element.Primitive;
using PeletonSoft.Sketch.ViewModel.Element.Layout;
using PeletonSoft.Sketch.ViewModel.Element.Null;
using PeletonSoft.Sketch.ViewModel.Element.Primitive;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Sketch.ViewModel.Interface.Element;
using PeletonSoft.Sketch.ViewModel.Interface.Layout;
using PeletonSoft.Tools.Model;
using PeletonSoft.Tools.Model.Draw.Wave;
using PeletonSoft.Tools.Model.Logic;
using PeletonSoft.Tools.Model.NotifyChanged;
using PeletonSoft.Tools.Model.SketchMath.ConnectStrategy;
using PeletonSoft.Tools.Model.SketchMath.Wave;
using PeletonSoft.Tools.Model.SketchMath.Wave.WavyBorderBuilder;
using PeletonSoft.Tools.Model.SketchMath.Wave.WavyBorderBuilder.ExtraStrategy;

namespace PeletonSoft.Sketch.ViewModel.Element
{
    public sealed class TieBackViewModel : IElementViewModel, INotifyViewModel<TieBack>, IClothableViewModel
    {
        #region implement INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            this.OnPropertyChanged(PropertyChanged, propertyName);
        }

        private void SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            Action notificator = () => OnPropertyChanged(propertyName);
            notificator.SetField(ref field, value);
        }

        private bool SetField<T>(Func<T> getValue, Action<T> setValue, T value, [CallerMemberName] string propertyName = null)
        {
            Action notificator = () => OnPropertyChanged(propertyName);
            return notificator.SetField(getValue, setValue, value);
        }

        private void OnPropertyChanged<T>(Expression<Func<TieBackViewModel, T>> expression)
        {
            expression.OnPropertyChanged(OnPropertyChanged);
        }

        #endregion

        #region implement IOriginator
        public void RestoreDefault()
        {
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

        public IList<IElementViewModel> Below
        {
            get { return WorkspaceBit.GetBelowElements(this); }
        }

        public void AfterInsert()
        {
            ChangeSheet();

            DenseWidth = Math.Min(
                0.6*Sheet.Height,
                0.6*Sheet.Width);
            OffsetY = 0.6*Sheet.Height;

        }

        public void BeforeDelete()
        {
            WorkspaceBit.RenderChangedDispatcher.Unsubscribe(this, Sheet);
            Sheet = _nullSheet;
        }
        #endregion

        #region implement IViewModel
        public TieBack Model { get; private set; }
        #endregion

        #region IClothableViewModel
        public IClotheViewModel Clothe { get; private set; }

        #endregion

        public TieBackViewModel(IWorkspaceBit workspaceBit, TieBack tieBack)
        {
            WorkspaceBit = workspaceBit;
            Model = tieBack;

            this.SetPropertyChanged(el => el.Sheet, () => Model.Sheet = Sheet.Model);
            _nullSheet = new NullSheetViewModel();
            Sheet = _nullSheet;
            Clothe = new ClotheViewModel(WorkspaceBit, Model.Clothe);

            LeftSide = new TieBackSideViewModel(Model.LeftSide) {Weight = 0.1};
            RightSide = new TieBackSideViewModel(Model.RightSide) {Weight = 0.5};
            Layout = new TieBackLayoutViewModel(this);


            Visibility = true;
            Opacity = 1;
            Length = 0.25;
            Depth = 0.05;
            DropHeight = 0;
            WaveCount = 5;
            Alignment = ElementAlignment.Left;

            workspaceBit.ElementListChanged += (sender, args) => ChangeSheet();

            Action sheetChange =
                () =>
                {
                    OnPropertyChanged(el => el.Rect);
                    OnPropertyChanged(el => el.WavySurface);
                    NotifyRenderChanged();
                };

            this
                .SetPropertyChanged(el => el.Visibility, NotifyRenderChanged)
                .SetPropertyChanged(
                    new[]
                    {
                        this.GetPropertyName(el => el.OffsetX),
                        this.GetPropertyName(el => el.OffsetY),
                        this.GetPropertyName(el => el.Alignment),
                        this.GetPropertyName(el => el.Protrusion),
                        this.GetPropertyName(el => el.Sheet)
                    },
                    () =>
                    {
                        OnPropertyChanged(el => el.Rect);
                        OnPropertyChanged(el => el.Lane);
                        OnPropertyChanged(el => el.WavySurface);
                    })
                .SetPropertyChanged(
                    new[]
                    {
                        this.GetPropertyName(el => el.Length),
                        this.GetPropertyName(el => el.Depth),
                        this.GetPropertyName(el => el.DropHeight)
                    },
                    () =>
                    {
                        OnPropertyChanged(el => el.Lane);
                        OnPropertyChanged(el => el.WavySurface);
                    })
                .SetPropertyChanged(
                    new[]
                    {
                        this.GetPropertyName(el => el.DenseWidth),
                        this.GetPropertyName(el => el.WaveCount)
                    },
                    () => OnPropertyChanged(el => el.WavySurface))
                .SetPropertyChanged(el => el.Sheet, sh => sh.Width, sheetChange)
                .SetPropertyChanged(el => el.Sheet, sh => sh.Height, sheetChange)
                .SetPropertyChanged(el => el.Sheet, sh => sh.OffsetX, sheetChange)
                .SetPropertyChanged(el => el.Sheet, sh => sh.OffsetY, sheetChange)
                .SetPropertyChanged(el => el.Sheet, sh => sh.Layout, sheetChange)
                .PropertyIterate(
                    new Expression<Func<TieBackViewModel, TieBackSideViewModel>>[]
                    {el => el.LeftSide, el => el.RightSide},
                    (side, propertyName) => side.SetPropertyChanged(
                        new[]
                        {
                            side.GetPropertyName(s => s.Weight),
                            side.GetPropertyName(s => s.TailScatter)
                        },
                        () => OnPropertyChanged(el => el.WavySurface)));

        }

        public TieBackSideViewModel LeftSide { get; private set; }
        public TieBackSideViewModel RightSide { get; private set; }

        public IWorkspaceBit WorkspaceBit { get; private set; }

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

        public double Length
        {
            get { return Model.Length; }
            set { SetField(() => Model.Length, v => Model.Length = v, value); }
        }
        public double Depth
        {
            get { return Model.Depth; }
            set { SetField(() => Model.Depth, v => Model.Depth = v, value); }
        }
        public double DropHeight
        {
            get { return Model.DropHeight; }
            set { SetField(() => Model.DropHeight, v => Model.DropHeight = v, value); }
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
        public double OffsetX
        {
            get { return Model.OffsetX; }
            set { SetField(() => Model.OffsetX, v => Model.OffsetX = v, value); }
        }
        public double OffsetY
        {
            get { return Model.OffsetY; }
            set { SetField(() => Model.OffsetY, v => Model.OffsetY = v, value); }
        }
        public double Protrusion
        {
            get { return Model.Protrusion; }
            set { SetField(() => Model.Protrusion, v => Model.Protrusion = v, value); }
        }

        public ILayoutViewModel Layout { get; private set; }

        public Rect Rect
        {
            get { return Model.GetRect(); }
        }

        public IEnumerable<Point> Lane
        {
            get { return Model.GetLane(); }
        }

        public IWavyBorder<IEnumerable<Point>> ParentWavySurface
        {
            get
            {
                var borderBuilder = new UprightWavyBorderBuilder(
                    new WavyBorderParameters(DenseWidth, 1, WaveCount),
                    new FixedExtraStrategy());
                var borderUp = borderBuilder.WavyBorder.Transform(pos => new Point(pos.X, 0));
                var borderDown = borderBuilder.WavyBorder.Transform(pos => new Point(pos.X, Sheet.Height));
                return borderUp.Connect(borderDown, new LineConnectStrategy());
            }
        }
        public IWavyBorder<IEnumerable<Point>> WavySurface
        {
            get { return Model.GetWavySurface(ParentWavySurface); }
        }


    }
}
