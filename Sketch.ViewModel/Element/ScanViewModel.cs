using System.Windows.Input;
using PeletonSoft.Sketch.Model.Element;
using PeletonSoft.Sketch.ViewModel.DataTransfer.Element;
using PeletonSoft.Sketch.ViewModel.DataTransfer.Interface;
using PeletonSoft.Sketch.ViewModel.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Element.Primitive;
using PeletonSoft.Sketch.ViewModel.Geometry;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Tools.Model.File;
using PeletonSoft.Tools.Model.Logic;
using PeletonSoft.Tools.Model.Memento;
using PeletonSoft.Tools.Model.ObjectEvent.NotifyChanged;

namespace PeletonSoft.Sketch.ViewModel.Element
{
    public sealed class ScanViewModel : AlignableElementViewModel, INotifyViewModel<Scan>, IOriginator<ScanDataTransfer>
    {
        public new Scan Model => (Scan) base.Model;

        public ScanViewModel(IWorkspaceBit workspaceBit, Scan model)
            : base(workspaceBit, model)
        {
            CropVisible = false;

            var commandFactory = WorkspaceBit.CommandFactory;
            OpenFileCommand = commandFactory.CreateCommand(parameter => OpenFile((ImageBox) parameter));
            CancelRectangeCommand = commandFactory.CreateCommand(CancelRectange);

            this
                .SetPropertyChanged(nameof(ImageBox), CancelRectange);

            Transformation
                .SetPropertyChanged(
                    new[]
                    {
                        nameof(Transformation.Rotation),
                        nameof(Transformation.Reflection)
                    },
                    () => OnPropertyChanged(nameof(Transformation)));

            Rectangle
                .SetPropertyChanged(
                    new []
                    {
                        nameof(Rectangle.TopLeft),
                        nameof(Rectangle.TopRight),
                        nameof(Rectangle.BottomLeft),
                        nameof(Rectangle.BottomRight)
                    },
                    () => OnPropertyChanged(nameof(Rectangle)));
        }
        
        public ImageBox ImageBox
        {
            get { return Model.ImageBox; }
            set { SetField(() => Model.ImageBox, v => Model.ImageBox = v, value); }
        }

        private bool _cropVisible;
        public bool CropVisible
        {
            get { return _cropVisible; }
            set { SetField(ref _cropVisible, value && ImageBox != null); }
        }

        public TransformationViewModel Transformation { get; } = new TransformationViewModel();
        public ScanRectangleViewModel Rectangle { get; } = new ScanRectangleViewModel(0, 0);
        public SuperimposeOptionViewModel SuperimposeOption { get; } = new SuperimposeOptionViewModel();

        public ICommand OpenFileCommand { get; private set; }
        public ICommand CancelRectangeCommand { get; private set; }

        private void OpenFile(ImageBox imageBox)
        {
            ImageBox = imageBox;
            CropVisible = true;
        }

        private void CancelRectange()
        {
            if (ImageBox != null)
            {
                Rectangle.Default(ImageBox.Width, ImageBox.Height);
                OnPropertyChanged(nameof(Rectangle));
            }
        }

        #region implement IOriginator
        ScanDataTransfer IOriginator<ScanDataTransfer>.CreateState() => new ScanDataTransfer();
        void IOriginator<ScanDataTransfer>.Save(ScanDataTransfer state)
        {
            base.Save(state);
            state.ImageBox = ImageBox;
            state.Transformation = Transformation.Save();
            state.SuperimposeOption = SuperimposeOption.Save();
            state.Rectangle = Rectangle.Save();
        }
        void IOriginator<ScanDataTransfer>.Restore(ScanDataTransfer state)
        {
            base.Restore(state);
            ImageBox = state.ImageBox;
            Transformation.Restore(state.Transformation);
            SuperimposeOption.Restore(state.SuperimposeOption);
            Rectangle.Restore(state.Rectangle);
        }
        public override IElementDataTransfer CreateState() => 
            (this as IOriginator<ScanDataTransfer>).CreateState();
        public override void Save(IElementDataTransfer state) => 
            (this as IOriginator<ScanDataTransfer>).Save((ScanDataTransfer) state);
        public override void Restore(IElementDataTransfer state) => 
            (this as IOriginator<ScanDataTransfer>).Restore((ScanDataTransfer)state);
        #endregion
    }
}
