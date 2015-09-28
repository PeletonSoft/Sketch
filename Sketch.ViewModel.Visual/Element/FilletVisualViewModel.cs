using PeletonSoft.Sketch.ViewModel.Element;
using PeletonSoft.Sketch.ViewModel.Interface.Visual;
using PeletonSoft.Sketch.ViewModel.Visual.Element.Custom;

namespace PeletonSoft.Sketch.ViewModel.Visual.Element
{
    public sealed class FilletVisualViewModel : ElementVisualViewModel, IElementVisualViewModel<FilletViewModel>
    {
        public FilletVisualViewModel(VisualOptions visualOptions, FilletViewModel element) 
            : base(visualOptions, element)
        {
        }

        public new FilletViewModel Element
        {
            get
            {
                return (FilletViewModel)base.Element;
            }
        }
    }
}
