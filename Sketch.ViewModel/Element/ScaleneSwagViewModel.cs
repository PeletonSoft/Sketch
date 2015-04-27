using System.Collections.Generic;
using System.Windows;
using PeletonSoft.Sketch.ViewModel.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Sketch.ViewModel.Interface.Element;

namespace PeletonSoft.Sketch.ViewModel.Element
{
    public class ScaleneSwagViewModel : ScaleneSwagTailViewModel, ISwagViewModel
    {
        public ScaleneSwagViewModel(IWorkspaceBit workspaceBit)
            : base(workspaceBit)
        {
        }

        public override IEnumerable<Point> Circuit
        {
            get { return this.GetCircuit(); }
        }
    }
}
