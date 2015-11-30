using System;
using System.Windows;
using System.Windows.Input;
using PeletonSoft.Sketch.ViewModel.DataTransfer.Interface;
using PeletonSoft.Sketch.ViewModel.DataTransfer.Present;
using PeletonSoft.Sketch.ViewModel.Element.Primitive;
using PeletonSoft.Sketch.ViewModel.Geometry;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Sketch.ViewModel.Interface.Element;
using PeletonSoft.Tools.Model.File;
using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewModel.Present
{
    public class PreviewPresentViewModel : CustomPresentViewModel
    {

        private readonly Lazy<ICommand> _lazyOpenFileCommand;
        public ICommand OpenFileCommand => _lazyOpenFileCommand.Value;

        private readonly Lazy<ICommand> _lazyCancelQuadrangleCommand;
        public ICommand CancelQuadrangleCommand => _lazyCancelQuadrangleCommand.Value;

        public PreviewPresentViewModel(IWorkspaceViewModel workspace)
            : base(workspace)
        {
            Workspace = workspace;
            SuperimposeOption = new SuperimposeOptionViewModel {ForegroundOpacity = 0.9};

            _lazyCancelQuadrangleCommand = new Lazy<ICommand>(
                () => Workspace.CommandFactory.CreateCommand(CancelQuadrangle));
            _lazyOpenFileCommand = new Lazy<ICommand>(
                () => Workspace.CommandFactory.CreateCommand(
                    parameter => OpenFile((ImageBox)parameter)));
        }

        private void OpenFile(ImageBox imageBox)
        {
            ImageBox = imageBox;
            CancelQuadrangle();
        }

        private void CancelQuadrangle()
        {
            _quadrangle = null;
            OnPropertyChanged(nameof(Quadrangle));
            OnPropertyChanged(nameof(ScreenScale));
            OnPropertyChanged(nameof(Ratio));
        }

        private ImageBox _imageBox;
        public ImageBox ImageBox
        {
            get { return _imageBox; }
            set { SetField(ref _imageBox, value); }
        }

        public Point ScreenScale => ImageBox != null
            ? new Point(
                ImageBox.Width/Workspace.Screen.Width, 
                ImageBox.Height/Workspace.Screen.Width)
            : new Point(0, 0);


        public double Ratio => ImageBox != null ? ImageBox.Height/(double) ImageBox.Width : 1;

        private PresentQuadrangleViewModel _quadrangle;
        public PresentQuadrangleViewModel Quadrangle 
        {
            get
            {
                if (_quadrangle == null)
                {
                    if (ImageBox != null)
                    {
                        _quadrangle = new PresentQuadrangleViewModel(ImageBox.Width, SuperimposeOption);
                    }
                }
                return _quadrangle;
            }
        }

        public ISuperimposeOptionViewModel SuperimposeOption { get; }

        public override IPresentDataTransfer CreateState() => new PreviewPresentDataTransfer();

        public override void Save(IPresentDataTransfer state)
        {
            Save((PreviewPresentDataTransfer)state);
        }

        public override void Restore(IPresentDataTransfer state)
        {
            Restore((PreviewPresentDataTransfer)state);
        }

        public void Save(PreviewPresentDataTransfer state)
        {
            base.Save(state);
            state.ImageBox = ImageBox;
            state.SuperimposeOption = SuperimposeOption.Save();
            state.Quadrangle = Quadrangle.Save();
        }

        public void Restore(PreviewPresentDataTransfer state)
        {
            base.Restore(state);
            ImageBox = state.ImageBox;
            SuperimposeOption.Restore(state.SuperimposeOption);
            Quadrangle?.Restore(state.Quadrangle);
        }

    }
}
