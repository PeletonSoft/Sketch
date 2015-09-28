using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Windows;
using PeletonSoft.Sketch.Model.Element;
using PeletonSoft.Sketch.ViewModel.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Element.Primitive;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Tools.Model.Logic;
using PeletonSoft.Tools.Model.ObjectEvent.NotifyChanged;

namespace PeletonSoft.Sketch.ViewModel.Element
{
    public sealed class HardPelmetViewModel : AlignableElementViewModel, IViewModel<HardPelmet>
    {
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
