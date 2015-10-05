using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Sketch.ViewModel.Interface.Element;
using PeletonSoft.Sketch.ViewModel.Interface.Layout;
using PeletonSoft.Tools.Model.ObjectEvent.NotifyChanged;
using static PeletonSoft.Tools.Model.ObjectEvent.NotifyChanged.NotifyPropertyChangedHelper;


namespace PeletonSoft.Sketch.ViewModel.Element.Primitive
{
    public abstract class LayoutViewModel : ILayoutViewModel
    {
        #region implement INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName) =>
                    this.OnPropertyChanged(PropertyChanged, propertyName);

        protected void SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null) =>
            SetFieldValue(() => OnPropertyChanged(propertyName), ref field, value);

        #endregion

        public double Width => Element.Width;
        public double Height => Element.Height;
        public double Left => Transform(Rect).X;
        public double Top => Transform(Rect).Y;

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

        public virtual Point LocalTransform(Point point) => point;

        protected LayoutViewModel(IWorkspaceBit workspaceBit, IAlignableElementViewModel element)
        {
            WorkspaceBit = workspaceBit;
            Element = element;

            Element
                .SetPropertyChanged(nameof(Element.Width),
                    () =>
                    {
                        OnPropertyChanged(nameof(Width));
                        OnPropertyChanged(nameof(Rect));
                    })
                .SetPropertyChanged(nameof(Element.Height),
                    () =>
                    {
                        OnPropertyChanged(nameof(Height));
                        OnPropertyChanged(nameof(Rect));
                    })
                .SetPropertyChanged(nameof(Element.OffsetX), () => OnPropertyChanged(nameof(Left)))
                .SetPropertyChanged(nameof(Element.OffsetY), () => OnPropertyChanged(nameof(Top)));

            WorkspaceBit
                .RenderChangedDispatcher.RenderChanged +=
                (sender, args) =>
                {
                    if (sender == Element)
                    {
                        OpacityMask = args.RenderData;
                    }
                };
        }


        protected IWorkspaceBit WorkspaceBit { get; }

        IElementViewModel ILayoutViewModel.Element => Element;

        protected IAlignableElementViewModel Element { get; }

        public void RestoreDefault()
        {
        }

        private IEnumerable<IEnumerable<Point>> _opacityMask;
        public IEnumerable<IEnumerable<Point>> OpacityMask
        {
            get { return _opacityMask; }
            private set { SetField(ref _opacityMask, value); }
        }

        public Rect Rect => Element.GetRect();
    }
}
