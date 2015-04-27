using PeletonSoft.Sketch.ViewModel.Element;
using PeletonSoft.Sketch.ViewModel.Visual.Element.Custom;

namespace PeletonSoft.Sketch.ViewModel.Visual.Element
{
    public class FilletVisualViewModel : ElementVisualViewModel
    {
        public FilletVisualViewModel(VisualOptions visualOptions, FilletViewModel element) 
            : base(visualOptions, element)
        {
        }

        private new FilletViewModel Element
        {
            get
            {
                return (FilletViewModel)base.Element;
            }
        }
    }
}
