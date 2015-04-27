using System;
using System.Windows.Input;
using PeletonSoft.Sketch.ViewModel.Element.Primitive;
using PeletonSoft.Sketch.ViewModel.Interface;

namespace PeletonSoft.Sketch.ViewModel.Present
{
    public class PreviewPresentViewModel : CustomPresentViewModel
    {
        private readonly Lazy<ICommand> _openFileCommand;
        public ICommand OpenFileCommand
        {
            get { return _openFileCommand.Value; }
        }

        public PreviewPresentViewModel(IWorkspaceViewModel workspace)
            : base(workspace)
        {
            Workspace = workspace;
            _openFileCommand = new Lazy<ICommand>(
                () => Workspace.CommandFactory.CreateCommand(
                    (parameters) => FileName = (string) parameters));
        }

        private string _fileName;

        public string FileName
        {
            get
            {
                return _fileName;
                
            }
            set
            {
                if (value != _fileName)
                {
                    _fileName = value;
                    _quadrangle = null;
                    OnPropertyChanged("FileName");
                    OnPropertyChanged("Quadrangle");
                }
            }
        }

        private double _width;
        public double Width
        {
            get
            {
                return _width;

            }
            set
            {
                if (value != _width && _quadrangle == null)
                {
                    _width = value;
                    OnPropertyChanged("Width");
                    OnPropertyChanged("ScreenScaleX");
                    OnPropertyChanged("Ratio");
                    OnPropertyChanged("Quadrangle");
                }
            }
        }

        public double ScreenScaleX
        {
            get
            {
                return Width/Workspace.Screen.Width;
            }
        }

        public double ScreenScaleY
        {
            get
            {
                return Height / Workspace.Screen.Width;
            }
        }

        private double _height;
        public double Height
        {
            get
            {
                return _height;

            }
            set
            {
                if (value != _height && _quadrangle == null)
                {
                    _height = value;
                    OnPropertyChanged("Height");
                    OnPropertyChanged("ScreenScaleY");
                    OnPropertyChanged("Ratio");
                    OnPropertyChanged("Quadrangle");
                }
            }
        }

        public double Ratio
        {
            get
            {
                return _width != 0 ? Height/Width : 1;
            }
        }

        private PresentQuadrangleViewModel _quadrangle;
        public PresentQuadrangleViewModel Quadrangle
        {
            get
            {
                if (_quadrangle == null)
                {
                    if (Width > 0 && Height > 0)
                    {
                        _quadrangle = new PresentQuadrangleViewModel(Width);
                    }
                }
                return _quadrangle;

            }
        }


    }
}
