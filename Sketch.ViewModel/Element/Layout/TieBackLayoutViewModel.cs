using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using PeletonSoft.Sketch.ViewModel.Interface.Element;
using PeletonSoft.Sketch.ViewModel.Interface.Layout;
using PeletonSoft.Tools.Model.NotifyChanged;

namespace PeletonSoft.Sketch.ViewModel.Element.Layout
{
    public sealed class TieBackLayoutViewModel : ILayoutViewModel
    {
        #region implement INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            this.OnPropertyChanged(PropertyChanged, propertyName);
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            Action notificator = () => OnPropertyChanged(propertyName);
            return notificator.SetField(ref field, value);
        }

        #endregion
        public void RestoreDefault()
        {
        }

        public double Width
        {
            get { return Rect.Width; }
        }

        public double Height
        {
            get { return Rect.Height; }
        }

        public double Left
        {
            get { return Transform(Rect).X; }
        }

        public double Top
        {
            get { return Transform(Rect).Y; }
        }

        public Rect Transform(Rect rect)
        {
            if (Element.Sheet == null)
            {
                return new Rect();
            }

            var layout = (Element.Sheet as ILayoutable).Layout;
            return layout.Transform(rect);
        }

        public Point LocalTransform(Point point)
        {
            if (Element.Sheet == null)
            {
                return new Point();
            }

            var layout = (Element.Sheet as ILayoutable).Layout;
            return layout.LocalTransform(point);
        }

        private IEnumerable<IEnumerable<Point>> _opacityMask;
        public IEnumerable<IEnumerable<Point>> OpacityMask
        {
            get { return _opacityMask; }
            set { SetField(ref _opacityMask, value); }
        }

        public Rect Rect
        {
            get { return Element.Rect; } 
        }

        public TieBackLayoutViewModel(TieBackViewModel element)
        {
            Element = element;
            element.WorkspaceBit.RenderChangedDispatcher.RenderChanged +=
                (sender, args) =>
                {
                    if (sender == Element)
                    {
                        OpacityMask = args.RenderData;
                    }
                };

            element.PropertyChanged += ElementOnPropertyChanged;
        }

        private void ElementOnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            switch (args.PropertyName)
            {
                case "Sheet":
                case "Rect":
                    OnPropertyChanged("Rect");
                    OnPropertyChanged("Top");
                    OnPropertyChanged("Width");
                    OnPropertyChanged("Height");
                    OnPropertyChanged("Left");
                    break;

            }
        }

        public TieBackViewModel Element { get; private set; }

        IElementViewModel ILayoutViewModel.Element
        {
            get { return Element; }
        }
    }
}
