using System;
using System.Linq.Expressions;
using System.Windows.Input;
using PeletonSoft.Sketch.Model.Element;
using PeletonSoft.Sketch.ViewModel.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Element.Primitive;
using PeletonSoft.Sketch.ViewModel.Geometry;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Tools.Model.File;
using PeletonSoft.Tools.Model.Logic;
using PeletonSoft.Tools.Model.NotifyChanged;

namespace PeletonSoft.Sketch.ViewModel.Element
{
    public class ScanViewModel : AlignableElementViewModel, INotifyViewModel<Scan>
    {
        private void OnPropertyChanged<T>(Expression<Func<ScanViewModel, T>> expression)
        {
            expression.OnPropertyChanged(OnPropertyChanged);
        }

        public new Scan Model
        {
            get { return (Scan) base.Model; }
        }

        public ScanViewModel(IWorkspaceBit workspaceBit, Scan model)
            : base(workspaceBit, model)
        {
            CropVisible = false;
            Transformation = new TransformationViewModel();
            Rectangle = new ScanRectangleViewModel(0, 0);

            var commandFactory = WorkspaceBit.CommandFactory;
            OpenFileCommand = commandFactory.CreateCommand(parameter => OpenFile((ImageBox) parameter));
            CancelRectangeCommand = commandFactory.CreateCommand(CancelRectange);
            SuperimposeOption = new SuperimposeOptionViewModel();

            this
                .SetPropertyChanged(el => el.ImageBox, CancelRectange);

            Transformation
                .SetPropertyChanged(
                    new[]
                    {
                        Transformation.GetPropertyName(t => t.Rotation),
                        Transformation.GetPropertyName(t => t.Reflection)
                    },
                    () => OnPropertyChanged(el => el.Transformation));

            Rectangle
                .SetPropertyChanged(
                    new Expression<Func<RectangleViewModel, VertexViewModel>>[]
                    {
                        el => el.TopLeft, el => el.TopRight,
                        el => el.BottomLeft, el => el.BottomRight
                    },
                    () => OnPropertyChanged(el => el.Rectangle));
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

        public TransformationViewModel Transformation { get; private set; }
        public ScanRectangleViewModel Rectangle { get; private set; }
        public SuperimposeOptionViewModel SuperimposeOption { get; private set; }

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
                OnPropertyChanged(el => el.Rectangle);
            }
        }
    }
}
