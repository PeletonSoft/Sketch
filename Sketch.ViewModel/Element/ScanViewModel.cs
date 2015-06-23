using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using PeletonSoft.Sketch.Model.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Element.Primitive;
using PeletonSoft.Sketch.ViewModel.Interface;

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
            SaveRectangeCommand = commandFactory.CreateCommand(SaveRectange);
            CancelRectangeCommand = commandFactory.CreateCommand(CancelRectange);
        }

        public ICommand OpenFileCommand { get; set; }

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
                    OnPropertyChanged("FileName");
                }
            }
        }

        private void OpenFile(object parameters)
        {
            try
            {
                FileName = (string) parameters;
                CropVisible = true;
            }
            catch (Exception)
            {
            }
        }

        private bool _cropVisible;

        public bool CropVisible
        {
            get
            {
                return _cropVisible;
            }
            set
            {
                if (value != _cropVisible)
                {
                    _cropVisible = value && FileName != null;
                    OnPropertyChanged("CropVisible");
                }
            }
        }

        private double _imageWidth;
        public double ImageWidth
        {
            get
            {
                return _imageWidth;
            }
            set
            {
                if (value > 0 && value != _imageWidth)
                {
                    _imageWidth = value;
                    _rectangle = null;
                    OnPropertyChanged("ImageWidth");
                    OnPropertyChanged("Rectangle");
                }
            }
        }

        private double _imageHeight;
        public double ImageHeight
        {
            get
            {
                return _imageHeight;
            }
            set
            {
                if (value > 0 && value != _imageHeight)
                {
                    _imageHeight = value;
                    _rectangle = null;
                    OnPropertyChanged("ImageHeight");
                    OnPropertyChanged("Rectangle");
                }
            }
        }

        private ScanRectangleViewModel _rectangle;

        public ScanRectangleViewModel Rectangle
        {
            get
            {
                if (_rectangle == null)
                {
                    _rectangle = new ScanRectangleViewModel(ImageWidth, ImageHeight);
                    _rectangle.BottomLeft.PropertyChanged += RectangleChange;
                    _rectangle.BottomRight.PropertyChanged += RectangleChange;
                    _rectangle.TopLeft.PropertyChanged += RectangleChange;
                    _rectangle.TopRight.PropertyChanged += RectangleChange;
                    SaveRectange();
                }
                return _rectangle;
                
            }
        }

        private void RectangleChange(object sender, PropertyChangedEventArgs args)
        {
            OnPropertyChanged("Rectangle");    
        }

        private double _rectangleAngle;
        public double RectangleAngle {
            get
            {
                return _rectangleAngle;
            }
            set
            {
                if (value != _rectangleAngle)
                {
                    _rectangleAngle = value;
                    OnPropertyChanged("RectangleAngle");
                }
            }
        }

        private Point _rectangleCenter;

        public Point RectangleCenter
        {
            get
            {
                return _rectangleCenter;
            }
            set
            {
                if (value != RectangleCenter)
                {
                    _rectangleCenter = value;
                    OnPropertyChanged("RectangleCenter");
                }
            }
        }

        private Point _rectangleSize;
        public Point RectangleSize {
            get
            {
                return _rectangleSize;
            }
            set
            {
                if (value != RectangleSize)
                {
                    _rectangleSize = value;
                    OnPropertyChanged("RectangleSize");
                }
            }
        }


        public ICommand SaveRectangeCommand { get; set; }

        public void SaveRectange()
        {
            RectangleCenter = new Point(Rectangle.TopLeft.X, Rectangle.TopLeft.Y);
            RectangleAngle = Math.Atan2(
                Rectangle.TopRight.Y - Rectangle.TopLeft.Y,
                Rectangle.TopRight.X - Rectangle.TopLeft.X);
            var a  =
                Math.Pow(Rectangle.TopRight.X - Rectangle.TopLeft.X, 2) +
                Math.Pow(Rectangle.TopRight.Y - Rectangle.TopLeft.Y, 2);
            var b = 
                Math.Pow(Rectangle.BottomRight.X - Rectangle.TopRight.X, 2) +
                Math.Pow(Rectangle.BottomRight.Y - Rectangle.TopRight.Y, 2);
            RectangleSize = new Point(Math.Sqrt(a), Math.Sqrt(b));

            OnPropertyChanged("RectangleClip");
        }

        public ICommand CancelRectangeCommand { get; set; }

        private void CancelRectange()
        {
            var d = Math.Sqrt(Math.Pow(RectangleSize.X, 2) + Math.Pow(RectangleSize.Y, 2));
            var b = RectangleAngle + Math.Atan2(RectangleSize.Y, RectangleSize.X);

            var rectangle = new ScanRectangleViewModel(ImageWidth, ImageHeight)
            {
                TopLeft =
                {
                    X = RectangleCenter.X,
                    Y = RectangleCenter.Y
                },
                BottomRight =
                {
                    X = RectangleCenter.X + d*Math.Cos(b),
                    Y = RectangleCenter.Y + d*Math.Sin(b)
                },
                TopRight =
                {
                    X = RectangleCenter.X + RectangleSize.X*Math.Cos(RectangleAngle),
                    Y = RectangleCenter.Y + RectangleSize.X*Math.Sin(RectangleAngle)
                }
            };


            rectangle.BottomRight.X = RectangleCenter.X + d * Math.Cos(b);
            rectangle.BottomRight.Y = RectangleCenter.Y + d * Math.Sin(b);

            _rectangle = rectangle;
            OnPropertyChanged("Rectangle");
            OnPropertyChanged("RectangleClip");

        }

    }
}
