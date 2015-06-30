using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Sketch.ViewModel.Interface.Element;
using PeletonSoft.Sketch.ViewModel.Interface.Layout;
using PeletonSoft.Tools.Model.NotifyChanged;

namespace PeletonSoft.Sketch.ViewModel.Element.Primitive
{
    public abstract class LayoutViewModel : ILayoutViewModel
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

        public double Width
        {
            get { return Element.Width; }
        }

        public double Height
        {
            get { return Element.Height; }
        }

        public double Left
        {
            get { return Transform(Rect).X; }
        }

        public double Top
        {
            get { return Transform(Rect).Y; }
        }

        public virtual Rect Transform(Rect rect)
        {
            return new Rect
            {
                X = Element.OffsetX + rect.X,
                Y = Element.OffsetY + rect.Y,
                Width = rect.Width,
                Height = rect.Height
            };
        }

        public virtual Point LocalTransform(Point point)
        {
            return point;
        }

        public LayoutViewModel(IWorkspaceBit workspaceBit, IAlignableElementViewModel element)
        {
            WorkspaceBit = workspaceBit;
            Element = element;

            element.PropertyChanged += ElementOnPropertyChanged;
            WorkspaceBit.RenderChangedDispatcher.RenderChanged +=
                (sender, args) =>
                {
                    if (sender == Element)
                    {
                        OpacityMask = args.RenderData;
                    }
                };
        }

        protected virtual void ElementOnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            switch (args.PropertyName)
            {
                case "Width":
                    OnPropertyChanged("Width");
                    OnPropertyChanged("Rect");
                    break;
                case "Height":
                    OnPropertyChanged("Height");
                    OnPropertyChanged("Rect");
                    break;
                case "OffsetX":
                    OnPropertyChanged("Left");
                    break;
                case "OffsetY":
                    OnPropertyChanged("Top");
                    break;

            }
        }

        protected IWorkspaceBit WorkspaceBit { get; private set; }

        IElementViewModel ILayoutViewModel.Element
        {
            get { return Element; }
        }

        protected IAlignableElementViewModel Element { get; private set; }

        public void RestoreDefault()
        {
        }

        private IEnumerable<IEnumerable<Point>> _opacityMask;
        public IEnumerable<IEnumerable<Point>> OpacityMask
        {
            get { return _opacityMask; }
            set { SetField(ref _opacityMask, value); }
        }

        public Rect Rect
        {
            get { return Element.GetRect(); } 
        }
    }
}
