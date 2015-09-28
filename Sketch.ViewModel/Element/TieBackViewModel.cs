using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Windows;
using PeletonSoft.Sketch.Model.Element;
using PeletonSoft.Sketch.ViewModel.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Element.Primitive;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Sketch.ViewModel.Interface.Element;
using PeletonSoft.Tools.Model.Logic;
using PeletonSoft.Tools.Model.NotifyChanged;

namespace PeletonSoft.Sketch.ViewModel.Element
{
    public sealed class TieBackViewModel : PleatableViewModel, INotifyViewModel<TieBack>, IClotheableViewModel
    {
        #region implement INotifyPropertyChanged

        private void OnPropertyChanged<T>(Expression<Func<TieBackViewModel, T>> expression)
        {
            expression.OnPropertyChanged(OnPropertyChanged);
        }

        #endregion

        #region implment ICollectionItem

        public override void AfterInsert()
        {
            base.AfterInsert();
            DenseWidth = Math.Min(0.6*Sheet.Height, 0.6*Sheet.Width);
            OffsetY = 0.6*Sheet.Height;
        }

        #endregion

        #region implement IViewModel

        public new TieBack Model
        {
            get { return (TieBack) base.Model; }
        }

        #endregion

        #region implement IClotheableViewModel

        public IClotheViewModel Clothe { get; private set; }

        #endregion

        public TieBackViewModel(IWorkspaceBit workspaceBit, TieBack model) : base(workspaceBit, model)
        {
            Clothe = new ClotheViewModel(WorkspaceBit, Model.Clothe);

            LeftSide = new TieBackSideViewModel(Model.LeftSide) {Weight = 0.1};
            RightSide = new TieBackSideViewModel(Model.RightSide) {Weight = 0.5};


            Length = 0.25;
            Depth = 0.05;
            DropHeight = 0;

            this
                .SetPropertyChanged(
                    new[]
                    {
                        this.GetPropertyName(el => el.OffsetX),
                        this.GetPropertyName(el => el.OffsetY),
                        this.GetPropertyName(el => el.Protrusion),
                    },
                    () => OnPropertyChanged(el => el.WavySurface))
                .SetPropertyChanged(
                    new[]
                    {
                        this.GetPropertyName(el => el.OffsetX),
                        this.GetPropertyName(el => el.OffsetY),
                        this.GetPropertyName(el => el.Alignment),
                        this.GetPropertyName(el => el.Protrusion),
                        this.GetPropertyName(el => el.Sheet)
                    },
                    () =>
                    {
                        OnPropertyChanged(el => el.Rect);
                        OnPropertyChanged(el => el.Lane);
                    })
                .SetPropertyChanged(
                    new[]
                    {
                        this.GetPropertyName(el => el.Length),
                        this.GetPropertyName(el => el.Depth),
                        this.GetPropertyName(el => el.DropHeight)
                    },
                    () =>
                    {
                        OnPropertyChanged(el => el.Lane);
                        OnPropertyChanged(el => el.WavySurface);
                    })
                .PropertyIterate(
                    new Expression<Func<TieBackViewModel, TieBackSideViewModel>>[]
                    {el => el.LeftSide, el => el.RightSide},
                    (side, propertyName) => side.SetPropertyChanged(
                        new[]
                        {
                            side.GetPropertyName(s => s.Weight),
                            side.GetPropertyName(s => s.TailScatter)
                        },
                        () => OnPropertyChanged(el => el.WavySurface)));

        }

        public TieBackSideViewModel LeftSide { get; private set; }
        public TieBackSideViewModel RightSide { get; private set; }

        public double Length
        {
            get { return Model.Length; }
            set { SetField(() => Model.Length, v => Model.Length = v, value); }
        }

        public double Depth
        {
            get { return Model.Depth; }
            set { SetField(() => Model.Depth, v => Model.Depth = v, value); }
        }

        public double DropHeight
        {
            get { return Model.DropHeight; }
            set { SetField(() => Model.DropHeight, v => Model.DropHeight = v, value); }
        }

        public double OffsetX
        {
            get { return Model.OffsetX; }
            set { SetField(() => Model.OffsetX, v => Model.OffsetX = v, value); }
        }

        public double OffsetY
        {
            get { return Model.OffsetY; }
            set { SetField(() => Model.OffsetY, v => Model.OffsetY = v, value); }
        }

        public double Protrusion
        {
            get { return Model.Protrusion; }
            set { SetField(() => Model.Protrusion, v => Model.Protrusion = v, value); }
        }

        public IEnumerable<Point> Lane
        {
            get { return Model.GetLane(); }
        }
    }
}
