using PeletonSoft.Tools.Model;
using PeletonSoft.Tools.Model.Dependency;

namespace PeletonSoft.Sketch.ViewModel.Visual
{
    public class VisualOptions
    {
        public PixelPerUnit PixelPerUnit { get; set; }
        public ICommandFactory CommandFactory { get; set; }
    }
}
