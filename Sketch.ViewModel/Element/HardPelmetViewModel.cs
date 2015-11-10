using System.Collections.Generic;
using System.Windows;
using PeletonSoft.Sketch.Model.Element;
using PeletonSoft.Sketch.ViewModel.DataTransfer.Element;
using PeletonSoft.Sketch.ViewModel.DataTransfer.Interface;
using PeletonSoft.Sketch.ViewModel.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Element.Primitive;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Tools.Model.Logic;
using PeletonSoft.Tools.Model.Memento;
using PeletonSoft.Tools.Model.ObjectEvent.NotifyChanged;

namespace PeletonSoft.Sketch.ViewModel.Element
{
    public sealed class HardPelmetViewModel : AlignableElementViewModel, IViewModel<HardPelmet>, IOriginator<HardPelmetDataTransfer>
    {
        #region implement Ioriginator
        HardPelmetDataTransfer IOriginator<HardPelmetDataTransfer>.CreateState() => new HardPelmetDataTransfer();

        void IOriginator<HardPelmetDataTransfer>.Save(HardPelmetDataTransfer state)
        {
            base.Save(state);
            state.DecorativeBorder = DecorativeBorder.Save();
        }

        void IOriginator<HardPelmetDataTransfer>.Restore(HardPelmetDataTransfer state)
        {
            base.Restore(state);
            DecorativeBorder.Restore(state.DecorativeBorder);
        }

        public override IElementDataTransfer CreateState() =>
            (this as IOriginator<HardPelmetDataTransfer>).CreateState();

        public override void Save(IElementDataTransfer state) =>
            (this as IOriginator<HardPelmetDataTransfer>).Save((HardPelmetDataTransfer)state);

        public override void Restore(IElementDataTransfer state) =>
            (this as IOriginator<HardPelmetDataTransfer>).Restore((HardPelmetDataTransfer)state);
        #endregion

        public new HardPelmet Model => (HardPelmet) base.Model;
        public DecorativeBorderViewModel DecorativeBorder { get; }
        public IEnumerable<Point> Points => Model.GetPoints();


        public HardPelmetViewModel(IWorkspaceBit workspaceBit, HardPelmet model)
            : base(workspaceBit, model)
        {
            DecorativeBorder = new DecorativeBorderViewModel(workspaceBit, Model.DecorativeBorder);

            this
                .SetPropertyChanged(nameof(Width),
                    () =>
                    {
                        DecorativeBorder.Width = Width;
                        OnPropertyChanged(nameof(Points));
                    })
                .SetPropertyChanged(nameof(Height), () => OnPropertyChanged(nameof(Points)));

            DecorativeBorder
                .SetPropertyChanged(nameof(Width), () => Width = DecorativeBorder.Width)
                .SetPropertyChanged(nameof(Points), () => OnPropertyChanged(nameof(Points)));

            Width = Screen.Width;
            Height = 0.5*Screen.Height;

            DecorativeBorder.Width = Width;
            DecorativeBorder.Height = 0;
            DecorativeBorder.ResetChains();
        }



    }
}
