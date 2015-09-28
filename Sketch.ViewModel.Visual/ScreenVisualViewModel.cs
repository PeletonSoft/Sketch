using System.ComponentModel;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Sketch.ViewModel.Interface.Visual;
using PeletonSoft.Tools.Model.ObjectEvent.NotifyChanged;

namespace PeletonSoft.Sketch.ViewModel.Visual
{
    public sealed class ScreenVisualViewModel : IScreenVisualViewModel
    {
        #region implement INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            this.OnPropertyChanged(PropertyChanged, propertyName);
        }
        #endregion


        public ScreenVisualViewModel(VisualOptions visualOptions, ScreenViewModel element)
        {
            VisualOptions = visualOptions;
            Element = element;
            Element
                .SetPropertyChanged(nameof(Element.Width), () => OnPropertyChanged(nameof(Width)))
                .SetPropertyChanged(nameof(Element.Height), () => OnPropertyChanged(nameof(Height)));
        }

        private VisualOptions VisualOptions { get; }
        public double Width => VisualOptions.PixelPerUnit.Transform(Element.Width);
        public double Height => VisualOptions.PixelPerUnit.Transform(Element.Height);
        public IScreenViewModel Element { get; set; }

    }
}
