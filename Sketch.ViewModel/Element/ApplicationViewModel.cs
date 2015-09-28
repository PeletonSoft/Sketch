using System;
using System.Collections.Generic;
using System.Windows;
using PeletonSoft.Sketch.Model.Element;
using PeletonSoft.Sketch.ViewModel.Container;
using PeletonSoft.Sketch.ViewModel.Element.Custom;
using PeletonSoft.Sketch.ViewModel.Element.Primitive;
using PeletonSoft.Sketch.ViewModel.Interface;
using PeletonSoft.Sketch.ViewModel.Interface.Geometry;
using PeletonSoft.Tools.Model.Collection;
using PeletonSoft.Tools.Model.Logic;
using PeletonSoft.Tools.Model.ObjectEvent.NotifyChanged;

namespace PeletonSoft.Sketch.ViewModel.Element
{
    public sealed class ApplicationViewModel : AlignableElementViewModel, IReflectableViewModel, IViewModel<Application>
    {
        public new Application Model => (Application)base.Model;

        public ApplicationViewModel(IWorkspaceBit workspaceBit, Application model)
            : base(workspaceBit, model)
        {
            Width = Screen.Width*0.3;
            Height = Screen.Height*0.3;
            Thickness = 0.3*Layout.Height;

            Outline = Outlines.Default;
            Reflection = Reflections.Default;

            this
                .SetPropertyChanged(
                    new[]
                    {
                        nameof(Thickness), nameof(Thickness),
                        nameof(Outline), nameof(Reflection),
                        nameof(Width), nameof(Height)
                    },
                    () => OnPropertyChanged(nameof(Points)));
        }

        public double Thickness
        {
            get { return Model.Thickness; }
            set { SetField(() => Thickness, v => Model.Thickness = v, value); }
        }

        private OutlineViewModel _outline;

        public OutlineViewModel Outline
        {
            get { return _outline; }
            set { SetField(ref _outline, value); }
        }

        private IReflectionViewModel _reflection;

        public IReflectionViewModel Reflection
        {
            get { return _reflection; }
            set { SetField(ref _reflection, value); }
        }

        private readonly Lazy<IContainer<OutlineViewModel>> _lazyOutlines =
            new Lazy<IContainer<OutlineViewModel>>(() => new OutlineViewModels());
        public IContainer<OutlineViewModel> Outlines => _lazyOutlines.Value;

        private readonly Lazy<IContainer<IReflectionViewModel>> _lazyTransformations =
            new Lazy<IContainer<IReflectionViewModel>>(() => new ReflectionViewModels());
        public IContainer<IReflectionViewModel> Reflections => _lazyTransformations.Value;

        public IEnumerable<Point> Points
        {
            get
            {
                var points = Outline.GetPoints(new Size(Width, Height), Thickness);
                return Reflection.GetPoints(points, Layout);
            }
        }
    }
}
