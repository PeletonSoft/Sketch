using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using PeletonSoft.Sketch.ViewModel.Interface.Element;
using PeletonSoft.Sketch.ViewModel.Interface.Layout;
using PeletonSoft.Sketch.ViewModel.Interface.Visual;
using PeletonSoft.Tools.Model;
using PeletonSoft.Tools.Model.NotifyChanged;

namespace PeletonSoft.Sketch.ViewModel.Visual.Element.Layout
{
    public class LayoutVisualViewModel : ILayoutVisualViewModel
    {

        #region implement INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            this.OnPropertyChanged(PropertyChanged, propertyName);
        }

        private bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            Action notificator = () => OnPropertyChanged(propertyName);
            return notificator.SetField(ref field, value);
        }
        #endregion

        public double Width
        {
            get
            {
                return Layout != null 
                    ? PixelPerUnit.Transform(Layout.Width) : 0;
            }
        }
        public double Height
        {
            get
            {
                return Layout != null 
                    ? PixelPerUnit.Transform(Layout.Height) : 0;
            }
        }
        public double Top
        {
            get
            {
                return Layout != null 
                    ?  PixelPerUnit.Transform(Layout.Top) : 0;
            }
        }
        public double Left
        {
            get
            {
                return  Layout != null 
                    ? PixelPerUnit.Transform(Layout.Left) : 0;
            }
        }

        public LayoutVisualViewModel(VisualOptions visualOptions, ILayoutViewModel layout)
        {
            VisualOptions = visualOptions;
            PixelPerUnit =VisualOptions.PixelPerUnit;
            if (layout != null)
            {
                Element = layout.Element;
                Element.PropertyChanged += ElementOnPropertyChanged;
                SubscribeLayout();
            }
        }

        private VisualOptions VisualOptions { get; set; }

        private void ElementOnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "Layout")
            {
                SubscribeLayout();

                OnPropertyChanged("Width");
                OnPropertyChanged("Height");
                OnPropertyChanged("Top");
                OnPropertyChanged("Left");
                OnPropertyChanged("Rect");
                OnPropertyChanged("OpacityMask");
            }
        }

        private ILayoutViewModel Layout { get; set; }
        private void SubscribeLayout()
        {
            if (Layout != null)
            {
                Layout.PropertyChanged -= LayoutOnPropertyChanged;
            }

            Layout = Element.Layout;

            if (Layout != null)
            {
                Layout.PropertyChanged += LayoutOnPropertyChanged;
            }
        }

        private void LayoutOnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            switch (args.PropertyName)
            {
                case "Width" :
                    OnPropertyChanged("Width");
                    OnPropertyChanged("SizeRect");
                    break;
                case "Height":
                    OnPropertyChanged("Height");
                    OnPropertyChanged("SizeRect");
                    break;
                case "Top":
                    OnPropertyChanged("Top");
                    break;
                case "Left":
                    OnPropertyChanged("Left");
                    break;
                case "OpacityMask":
                    OnPropertyChanged("OpacityMask");
                    break;
                case "Rect":
                    OnPropertyChanged("Rect");
                    break;

            }
        }

        private PixelPerUnit PixelPerUnit { get; set; }
        private IElementViewModel Element { get; set; }

        public IEnumerable<IEnumerable<Point>> OpacityMask
        {
            get { return PixelPerUnit.Transform(Layout.OpacityMask); }
        }

        public Rect Rect
        {
            get { return Layout != null ? PixelPerUnit.Transform(Layout.Rect) : new Rect(); }
        }
    }
}
