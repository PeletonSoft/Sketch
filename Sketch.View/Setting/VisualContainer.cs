using PeletonSoft.Sketch.ViewModel;
using PeletonSoft.Sketch.ViewModel.Element;
using PeletonSoft.Sketch.ViewModel.Element.Primitive;
using PeletonSoft.Sketch.ViewModel.Visual;
using PeletonSoft.Sketch.ViewModel.Visual.Element;
using PeletonSoft.Sketch.ViewModel.Visual.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Visual.Element.Primitive;
using PeletonSoft.Tools.Model;

namespace PeletonSoft.Sketch.View.Setting
{
    public class VisualContainer : InjectContainer
    {
        public VisualOptions Options { get; set; }
        public VisualContainer()
        {
            this
                .Register<DecorativeBorderViewModel>(el => new DecorativeBorderVisualViewModel(Options, el))
                .Register<ScreenViewModel>(sc => new ScreenVisualViewModel(Options, sc))

                .Register<ApplicationViewModel>(el => new ApplicationVisualViewModel(Options, el))
                .Register<DeJabotViewModel>(el => new DeJabotVisualViewModel(Options, el))
                .Register<EqualSwagViewModel>(el => new SwagTailVisualViewModel(Options, el))
                .Register<EqualTailViewModel>(el => new SwagTailVisualViewModel(Options, el))
                .Register<ScaleneSwagViewModel>(el => new SwagTailVisualViewModel(Options, el))
                .Register<ScaleneTailViewModel>(el => new SwagTailVisualViewModel(Options, el))
                .Register<FilletViewModel>(el => new FilletVisualViewModel(Options, el))
                .Register<HardPelmetViewModel>(el => new HardPelmetVisualViewModel(Options, el))
                .Register<OverlayViewModel>(el => new OverlayVisualViewModel(this, Options, el))
                .Register<PanelViewModel>(el => new PanelVisualViewModel(Options, el))
                .Register<PortiereViewModel>(el => new PortiereVisualViewModel(Options, el))
                .Register<ScanViewModel>(el => new ScanVisualViewModel(Options, el))
                .Register<TieBackViewModel>(el => new TieBackVisualViewModel(Options, el))
                .Register<TulleViewModel>(el => new TulleVisualViewModel(Options, el))
                .Register<RomanBlindViewModel>(el => new RomanBlindVisualViewModel(Options, el));
        }
    }
}
