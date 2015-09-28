using System.ComponentModel;
using System.Windows.Input;
using PeletonSoft.Sketch.ViewModel.Interface.Element;

namespace PeletonSoft.Sketch.ViewModel.Element.Null
{
    public class NullClotheViewModel : IClotheViewModel
    {
        public NullClotheViewModel(ICommand calculateCommand)
        {
            CalculateCommand = calculateCommand;
        }

        public NullClotheViewModel()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void RestoreDefault()
        {
        }

        public double? Height { get; set; } = 0;

        public double? Width { get; set; } = 0;
        public double? Area { get; set; } = 0;

        public ICommand CalculateCommand { get; }
    }
}
