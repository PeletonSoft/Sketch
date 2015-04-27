using System.ComponentModel;
using PeletonSoft.Sketch.ViewModel.Element.Primitive;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Tools.Model.NotifyChanged;

namespace PeletonSoft.Sketch.ViewModel.Element.Custom
{
    public abstract class EqualSwagTailViewModel : SwagTailViewModel
    {
        public EqualSwagTailViewModel(IWorkspaceBit workspaceBit)
            : base(workspaceBit)
        {
            Shoulder = new ShoulderViewModel();
            Shoulder.PropertyChanged +=ShoulderOnPropertyChanged;

            Shoulder.Length = 0.25 * Layout.Width;
            Shoulder.WaveHeight = Shoulder.Length / WaveCount * 1.3;

            this.SetPropertyChanged(
                new[] {"WaveHeight", "ShoulderLength"},
                VisualChanged);
        }

        private void ShoulderOnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            switch (args.PropertyName)
            {
                case "Length":
                    LeftShoulder.Length = Shoulder.Length;
                    RightShoulder.Length = Shoulder.Length;
                    break;
                case  "WaveHeight":
                    LeftShoulder.WaveHeight = Shoulder.WaveHeight;
                    RightShoulder.WaveHeight = Shoulder.WaveHeight;
                    break;
                case  "OffsetY":
                    LeftShoulder.OffsetY = Shoulder.OffsetY;
                    RightShoulder.OffsetY = Shoulder.OffsetY;
                    break;
                case "Slope":
                    LeftShoulder.Slope = Shoulder.Slope;
                    RightShoulder.Slope = Shoulder.Slope;
                    break;
            }
        }

        private void VisualChanged()
        {
            if (Width > 0.001 && Height > 0.005 && WaveCount > 0)
            {
                OnPropertyChanged("Circuit");
                OnPropertyChanged("WavySurface");
            }
        }

        public ShoulderViewModel Shoulder { get; private set; }
    }
}
