using System.ComponentModel;
using System.Windows.Input;
using PeletonSoft.Sketch.ViewModel.Interface.Element;

namespace PeletonSoft.Sketch.ViewModel.Element.Primitive
{
    public class NullClotheViewModel : IClotheViewModel
    {
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
        public ICommand CalculateCommand { get; private set; }
    }
}
