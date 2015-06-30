using System;
using System.Windows.Input;
using PeletonSoft.Sketch.Model.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Element.Primitive;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Tools.Model.File;
using PeletonSoft.Tools.Model.NotifyChanged;

namespace PeletonSoft.Sketch.ViewModel.Element
{
    public class ScanViewModel : AlignableElementViewModel
    {
        public ScanViewModel(IWorkspaceBit workspaceBit)
            : base(workspaceBit, new AlignableElement())
        {
            CropVisible = false;

            var commandFactory = WorkspaceBit.CommandFactory;
            OpenFileCommand = commandFactory.CreateCommand(OpenFile);
            CancelRectangeCommand = commandFactory.CreateCommand(CancelRectange);
        }

        public ICommand OpenFileCommand { get; set; }

        private void OpenFile(object parameter)
        {
            try
            {
                ImageBox = (ImageBox)parameter;
                CancelRectange();
                CropVisible = true;
            }
            catch (Exception)
            {
            }
        }

        private ImageBox _imageBox;
        public ImageBox ImageBox
        {
            get { return _imageBox; }
            set { SetField(ref _imageBox, value); }
        }

        private bool _cropVisible;
        public bool CropVisible
        {
            get { return _cropVisible; }
            set { SetField(ref _cropVisible, value && ImageBox != null); }
        }


        private ScanRectangleViewModel _rectangle;
        public ScanRectangleViewModel Rectangle
        {
            get
            {
                if (_rectangle == null)
                {
                    _rectangle = new ScanRectangleViewModel(ImageBox.Width, ImageBox.Height);
                    _rectangle.SetPropertyChanged("Vertex", () => OnPropertyChanged("Rectangle"));
                }
                return _rectangle;
            }
        }

        public ICommand CancelRectangeCommand { get; set; }

        private void CancelRectange()
        {
            _rectangle = null;
            OnPropertyChanged("Rectangle");

        }

    }
}
