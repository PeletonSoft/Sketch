using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using PeletonSoft.Sketch.ViewModel.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Sketch.ViewModel.Interface.Element;
using PeletonSoft.Sketch.ViewModel.Interface.Layout;
using PeletonSoft.Tools.Model.NotifyChanged;

namespace PeletonSoft.Sketch.ViewModel.Element.Layout
{
    public sealed class PleatableLayoutViewModel : ILayoutViewModel
    {
        #region implement INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            this.OnPropertyChanged(PropertyChanged, propertyName);
        }

        private void SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            Action notificator = () => OnPropertyChanged(propertyName);
            notificator.SetField(ref field, value);
        }
        #endregion

        public void RestoreDefault()
        {
        }

        public double Width => Rect.Width;
        public double Height => Rect.Height;
        public double Left => Transform(Element.Rect).X;
        public double Top => Transform(Element.Rect).Y;

        public Rect Transform(Rect rect)
        {
            var layout = Element.Sheet.Layout;
            return layout.Transform(rect);
        }

        public Point LocalTransform(Point point)
        {
            var layout = Element.Sheet.Layout;
            return layout.LocalTransform(point);
        }

        private IEnumerable<IEnumerable<Point>> _opacityMask;
        public IEnumerable<IEnumerable<Point>> OpacityMask
        {
            get { return _opacityMask; }
            set { SetField(ref _opacityMask, value); }
        }

        public Rect Rect => new Rect(new Point(0, 0), Element.Rect.Size);

        public PleatableLayoutViewModel(IWorkspaceBit workspaceBit, PleatableViewModel element)

        {
            Element = element;
            WorkspaceBit = workspaceBit;
            var dispatcher = WorkspaceBit.RenderChangedDispatcher;
            dispatcher.RenderChanged +=
                (sender, args) =>
                {
                    if (sender == Element)
                    {
                        OpacityMask = args.RenderData;
                    }
                };

            Element
                .SetPropertyChanged(
                    new[]
                    {
                        nameof(Element.Sheet),
                        nameof(Element.Rect)
                    }, () =>
                    {
                        OnPropertyChanged(nameof(Rect));
                        OnPropertyChanged(nameof(Width));
                        OnPropertyChanged(nameof(Height));
                        OnPropertyChanged(nameof(Top));
                        OnPropertyChanged(nameof(Left));
                    });

        }

        public IWorkspaceBit WorkspaceBit { get; set; }

        private PleatableViewModel Element { get; }

        IElementViewModel ILayoutViewModel.Element => Element;
    }
}
