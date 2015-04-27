using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using PeletonSoft.Sketch.ViewModel.Element.Layout;
using PeletonSoft.Sketch.ViewModel.Element.Primitive;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Sketch.ViewModel.Interface.Element;
using PeletonSoft.Sketch.ViewModel.Interface.Layout;
using PeletonSoft.Tools.Model;
using PeletonSoft.Tools.Model.Draw;
using PeletonSoft.Tools.Model.Draw.Wave;
using PeletonSoft.Tools.Model.Memento;
using PeletonSoft.Tools.Model.NotifyChanged;
using PeletonSoft.Tools.Model.SketchMath;
using PeletonSoft.Tools.Model.SketchMath.ConnectStrategy;
using PeletonSoft.Tools.Model.SketchMath.Wave;
using PeletonSoft.Tools.Model.SketchMath.Wave.WavyBorderBuilder;
using PeletonSoft.Tools.Model.SketchMath.Wave.WavyBorderBuilder.ExtraStrategy;

namespace PeletonSoft.Sketch.ViewModel.Element
{
    public sealed class TieBackViewModel : IElementViewModel, IOriginator
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

        #endregion

        #region implement IOriginator
        public void RestoreDefault()
        {
        }
        #endregion

        public IClotheViewModel Clothe
        {
            get
            {
                return null;
            }
        }
        public ICommand MoveToElementCommand { get; set; }
        public IList<IElementViewModel> Below
        {
            get
            {
                return WorkspaceBit.GetBelowElements(this);
            }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { SetField(ref _description, value); }
        }

        private bool _visibility;
        public bool Visibility
        {
            get { return _visibility; }
            set { SetField(ref _visibility, value); }
        }

        private double _opacity;
        public double Opacity
        {
            get { return _opacity; }
            set { SetField(ref _opacity, value); }
        }

        public TieBackSideViewModel LeftSide { get; private set; }
        public TieBackSideViewModel RightSide { get; private set; }

        public void AfterInsert()
        {
            ChangeSheet();
            if (Sheet != null)
            {
                DenseWidth = Math.Min(
                    0.6*Sheet.Height,
                    0.6*Sheet.Width);
                OffsetY = 0.6*Sheet.Height;
            }
        }

        public void BeforeDelete()
        {
            if (Sheet != null)
            {
                _sheet.PropertyChanged -= SheetOnPropertyChanged;
            }
        }

        public TieBackViewModel(IWorkspaceBit workspaceBit, int pointCount)
        {
            WorkspaceBit = workspaceBit;
            PointCount = pointCount;
            LeftSide = new TieBackSideViewModel {Weight = 0.1};
            RightSide = new TieBackSideViewModel {Weight = 0.5};

            Visibility = true;
            Opacity = 1;
            Length = 0.25;
            Depth = 0.05;
            DropHeight = 0;
            WaveCount = 5;

            Layout = new TieBackLayoutViewModel(this);
            Alignment = ElementAlignment.Left;

            workspaceBit.ElementListChanged +=
                (sender, args) => ChangeSheet();
            this.SetPropertyChanged("Visibility", NotifyRenderChanged);
            this.SetPropertyChanged(
                new[] {"Alignment", "Length", "OffsetX", "OffsetY", "Depth", "DropHeight", "Protrusion"},
                () => OnPropertyChanged("Lane"));
            this.SetPropertyChanged(
                new[] {"OffsetX", "OffsetY", "Alignment", "Protrusion"},
                () => OnPropertyChanged("Rect"));
            this.SetPropertyChanged(
                new[]
                {
                    "Sheet", "Alignment", "DenseWidth", "WaveCount", "Protrusion",
                    "Length", "OffsetX", "OffsetY", "Depth", "DropHeight"
                },
                () => OnPropertyChanged("WavySurface"));
            new[] {LeftSide, RightSide}.SetPropertyChanged(
                new[] {"Weight", "TailScatter"},
                () => OnPropertyChanged("WavySurface"));
        }

        public int PointCount { get; private set; }

        public IWorkspaceBit WorkspaceBit { get; private set; }

        void SheetOnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            switch (args.PropertyName)
            {
                case "Width":
                case "Height":
                case "OffsetX":
                case "OffsetY":
                    OnPropertyChanged("Rect");
                    OnPropertyChanged("WavySurface");
                    NotifyRenderChanged();
                    break;

            }
        }

        private ISheetElementViewModel _sheet;
        public ISheetElementViewModel Sheet
        {
            get
            {
                return Below
                    .OfType<ISheetElementViewModel>()
                    .Reverse()
                    .FirstOrDefault();
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
        private void ChangeSheet()
        {
            var newSheet = Sheet;

            if (newSheet != _sheet)
            {
                if (_sheet != null)
                {
                    _sheet.PropertyChanged -= SheetOnPropertyChanged;
                    WorkspaceBit.RenderChangedDispatcher.Unsubscribe(this,_sheet);
                    _notifyRenderChanged = null;
                }

                _sheet = newSheet;

                if (_sheet != null)
                {
                    _sheet.PropertyChanged += SheetOnPropertyChanged;
                    _notifyRenderChanged = WorkspaceBit.RenderChangedDispatcher
                        .Subscribe(this, _sheet, () => RenderArea);
                }

                OnPropertyChanged("Sheet");
                OnPropertyChanged("Rect");
            }
        }

        public IEnumerable<Point> RenderArea
        {
            get
            {
                return Sheet != null && Visibility
                    ? new[]
                    {
                        new Point(0, 0),
                        new Point(Sheet.Width, 0),
                        new Point(Sheet.Width, Sheet.Height),
                        new Point(0, Sheet.Height)
                    }
                    : null;
            }
        }

        private double _length;
        public double Length
        {
            get { return _length; }
            set { SetField(ref _length, value); }
        }

        private double _depth;
        public double Depth
        {
            get { return _depth; }
            set { SetField(ref _depth, value); }
        }

        private double _dropHeight;
        public double DropHeight
        {
            get { return _dropHeight; }
            set { SetField(ref _dropHeight, value); }
        }

        private ElementAlignment _alignment;
        public ElementAlignment Alignment
        {
            get { return _alignment; }
            set { SetField(ref _alignment, value); }
        }

        private double _denseWidth;
        public double DenseWidth
        {
            get { return _denseWidth; }
            set { SetField(ref _denseWidth, value); }
        }

        private int _waveCount;
        public int WaveCount
        {
            get { return _waveCount; }
            set { SetField(ref _waveCount, value); }
        }


        private double _offsetY;
        public double OffsetY
        {
            get { return _offsetY; }
            set { SetField(ref _offsetY, value); }
        }

        private double _offsetX;
        public double OffsetX
        {
            get { return _offsetX; }
            set { SetField(ref _offsetX, value); }
        }

        private double _protrusion;
        public double Protrusion
        {
            get { return _protrusion; }
            set { SetField(ref _protrusion, value); }
        }

        public ILayoutViewModel Layout { get; private set; }

        public Rect Rect
        {
            get
            {
                return Sheet == null
                    ? new Rect()
                    : Alignment == ElementAlignment.Left
                        ? new Rect(-Protrusion, 0, Sheet.Width + Protrusion, Sheet.Height)
                        : new Rect(0, 0, Sheet.Width + Protrusion, Sheet.Height);
            }
        }

        private Func<Point, Point> AlignmentTransform
        {
            get
            {
                return point =>
                    Alignment == ElementAlignment.Left || Sheet == null
                        ? new Point(point.X + Protrusion, point.Y)
                        : new Point(Sheet.Width - point.X, point.Y);
            }
        }

        private Point LaneUpF(double t)
        {
            var width = Math.Sqrt(Math.Pow(Length, 2) - Math.Pow(DropHeight, 2));
            return new Point(
                OffsetX + Depth/2*DropHeight/Length + t*width,
                OffsetY - Depth/2*width/Length + t*DropHeight);
        }

        private Point LaneDownF(double t)
        {
            var width = Math.Sqrt(Math.Pow(Length, 2) - Math.Pow(DropHeight, 2));
            return new Point(
                OffsetX - Depth / 2 * DropHeight / Length + t * width,
                OffsetY + Depth / 2 * width / Length + t * DropHeight);
        }

        public IEnumerable<Point> Lane
        {
            get
            {
                var lane = new[] {LaneUpF(0), LaneUpF(1), LaneDownF(1), LaneDownF(0)};
                return lane.Select(AlignmentTransform);
            }
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
            get
            {
                if (Sheet == null)
                {
                    return null;
                }

                var wavySurface = ParentWavySurface;
                var upBorder = wavySurface.Start();
                var border = upBorder.Normalize();

                var laneUpBorder = border.Transform(LaneUpF);
                var laneDownBorder = border.Transform(LaneDownF);
                var lengths = wavySurface.Transform(points => points.Length());

                var leftLength = LeftSide.GetLength(ParentWavySurface.Waves.First(), LaneUpF(0));
                var rightLength = RightSide.GetLength(ParentWavySurface.Waves.Last(), LaneUpF(1));

                var connectStrategy = border
                    .Transform(t => (IConnectStrategy) new CatenaryLengthConnectStrategy
                        (leftLength + t*(rightLength - leftLength), PointCount, true));
                Func<double, double> angleF =
                    t => ((LeftSide.TailScatter + RightSide.TailScatter)*t - LeftSide.TailScatter)/180*Math.PI;
                var tailBorder = border
                    .Zip(laneDownBorder, lengths,
                        (t, p, l) => new Point(
                            p.X + l*Math.Sin(angleF(t)),
                            p.Y + l*Math.Cos(angleF(t))));

                return upBorder
                    .Connect(laneUpBorder, connectStrategy)
                    .Connect(laneDownBorder, new LineConnectStrategy())
                    .Connect(tailBorder, new LineConnectStrategy())
                    .Cut(lengths)
                    .Transform(points => points.Select(AlignmentTransform));
            }
        }
    }
}
