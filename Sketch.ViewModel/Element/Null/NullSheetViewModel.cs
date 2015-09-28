using PeletonSoft.Sketch.Model.Element.Null;
using PeletonSoft.Sketch.Model.Interface.Element;
using PeletonSoft.Sketch.ViewModel.Element.Layout;
using PeletonSoft.Sketch.ViewModel.Interface.Element;
using PeletonSoft.Sketch.ViewModel.Interface.Layout;
using PeletonSoft.Tools.Model.Collection;
using PeletonSoft.Tools.Model.Memento.Container;

namespace PeletonSoft.Sketch.ViewModel.Element.Null
{
    public class NullSheetViewModel : NullElementViewModel, ISheetElementViewModel
    {
        public double Width
        {
            get { return 0; }
            set { }
        }
        public double Height
        {
            get { return 0; }
            set { }
        }
        public double OffsetX
        {
            get { return 0; }
            set { }
        }
        public double OffsetY
        {
            get { return 0; }
            set { }
        }

        public new ILayoutViewModel Layout
        {
            get { return Layouts.Default; }
            set { }
        }

        public IContainerOriginator<ILayoutViewModel> Layouts { get; }

        public NullSheetViewModel()
        {
            Model = new NullSheet();
            Layouts = new ContainerOriginator<ILayoutViewModel>(
                new[]
                {
                    new ContainerRecord<ILayoutViewModel>("Null", typeof (NullLayoutViewModel),
                        new NullLayoutViewModel())
                });
        }

        public ISheet Model { get; }

    }
}
