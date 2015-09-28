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

        public double? Height
        {
            get { return 0; } 
            set { }
        }

        public double? Width
        {
            get { return 0; }
            set { }
        }
        public double? Area
        {
            get { return 0; }
            set { }
        }
        public ICommand CalculateCommand { get; }
    }
}
