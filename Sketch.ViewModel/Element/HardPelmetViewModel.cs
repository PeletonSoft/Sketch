using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Windows;
using PeletonSoft.Sketch.Model.Element;
using PeletonSoft.Sketch.ViewModel.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Element.Primitive;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Tools.Model.Logic;
using PeletonSoft.Tools.Model.NotifyChanged;

namespace PeletonSoft.Sketch.ViewModel.Element
{
    public sealed class HardPelmetViewModel : AlignableElementViewModel, IViewModel<HardPelmet>
    {
        private void OnPropertyChanged<T>(Expression<Func<HardPelmetViewModel, T>> expression)
        {
            expression.OnPropertyChanged(OnPropertyChanged);
        }

        public new HardPelmet Model
        {
            get { return (HardPelmet) base.Model; }
        }
        public DecorativeBorderViewModel DecorativeBorder { get; private set; }

        public IEnumerable<Point> Points
        {
            get { return Model.GetPoints(); }
        }


        public HardPelmetViewModel(IWorkspaceBit workspaceBit, HardPelmet model)
            : base(workspaceBit, model)
        {
            DecorativeBorder = new DecorativeBorderViewModel(workspaceBit, Model.DecorativeBorder);

            this
                .SetPropertyChanged(el => el.Width,
                    () =>
                    {
                        DecorativeBorder.Width = Width;
                        OnPropertyChanged(el => el.Points);
                    })
                .SetPropertyChanged(el => el.Height, () => OnPropertyChanged(el => el.Points));

            DecorativeBorder
                .SetPropertyChanged(el => el.Width, () => Width = DecorativeBorder.Width)
                .SetPropertyChanged(el => el.Points, () => OnPropertyChanged(el => el.Points));

            Width = Screen.Width;
            Height = 0.5*Screen.Height;

            DecorativeBorder.Width = Width;
            DecorativeBorder.Height = 0;
            DecorativeBorder.ResetChains();
        }


    }
}
