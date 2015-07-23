using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Windows;
using PeletonSoft.Sketch.Model.Element;
using PeletonSoft.Sketch.ViewModel.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Tools.Model.NotifyChanged;

namespace PeletonSoft.Sketch.ViewModel.Element
{
    public sealed class LatticeViewModel : AlignableElementViewModel
    {
        private void OnPropertyChanged<T>(Expression<Func<LatticeViewModel, T>> expression)
        {
            expression.OnPropertyChanged(OnPropertyChanged);
        }

        private new Lattice Model
        {
            get { return (Lattice) base.Model; }
        }

        public double CellWidth
        {
            get { return Model.CellWidth; }
            set { SetField(() => Model.CellWidth, v => Model.CellWidth = v, value); }
        }
        public double CellHeight
        {
            get { return Model.CellHeight; }
            set { SetField(() => Model.CellHeight, v => Model.CellHeight = v, value); }
        }

        public IEnumerable<Rect> Lines
        {
            get { return Model.GetLines(); }
        }

        public LatticeViewModel(IWorkspaceBit workspaceBit, Lattice model)
            : base(workspaceBit, model)
        {
            CellHeight = 0.5;
            CellWidth = 0.5;
            this.SetPropertyChanged(
                new[]
                {
                    this.GetPropertyName(el => el.CellWidth),
                    this.GetPropertyName(el => el.CellHeight),
                    this.GetPropertyName(el => el.Width),
                    this.GetPropertyName(el => el.Height)
                },
                () => OnPropertyChanged(el => el.Lines));
        }
    }

}
