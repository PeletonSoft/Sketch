using System;
using System.Windows;
using System.Windows.Input;
using PeletonSoft.Sketch.ViewModel.Element.Primitive;
using PeletonSoft.Sketch.ViewModel.Geometry;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Tools.Model.File;

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

        public Point ScreenScale
        {
            get
            {
                return ImageBox != null
                    ? new Point(
                        ImageBox.Width/Workspace.Screen.Width, 
                        ImageBox.Height/Workspace.Screen.Width)
                    : new Point(0, 0);
            }
        }


        public double Ratio
        {
            get { return ImageBox != null ? ImageBox.Height/(double) ImageBox.Width : 1; }
        }

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

        public SuperimposeOptionViewModel SuperimposeOption { get; }


    }
}
