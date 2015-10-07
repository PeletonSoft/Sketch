using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using PeletonSoft.Sketch.ViewModel.Element.Primitive;
using PeletonSoft.Sketch.ViewModel.Geometry.DecorativeBorder;
using PeletonSoft.Sketch.ViewModel.Interface.Draw;
using PeletonSoft.Tools.Model.Dragable;
using PeletonSoft.Tools.Model.Draw;
using PeletonSoft.Tools.Model.Logic;
using PeletonSoft.Tools.Model.ObjectEvent.NotifyChanged;
using static PeletonSoft.Tools.Model.ObjectEvent.NotifyChanged.NotifyPropertyChangedHelper;

namespace PeletonSoft.Sketch.ViewModel.Visual.Element.Primitive
{
    public sealed class DecorativeBorderVisualViewModel : INotifyVisualViewModel<DecorativeBorderViewModel>
    {
        #region implement INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName) =>
            this.OnPropertyChanged(PropertyChanged, propertyName);

        private void SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null) =>
            SetFieldValue(() => OnPropertyChanged(propertyName), ref field, value);
        #endregion

        public DecorativeBorderVisualViewModel(VisualOptions visualOptions, DecorativeBorderViewModel element)
        {
            VisualOptions = visualOptions;
            Element = element;
            Chains = CalculateChains(Element.Points);

            Element
                .SetPropertyChanged(nameof(Element.Width), () => OnPropertyChanged(nameof(Width)))
                .SetPropertyChanged(nameof(Element.Height), () => OnPropertyChanged(nameof(Height)))
                .SetPropertyChanged(nameof(Element.Points), () => Chains = CalculateChains(Element.Points));

            var commandFactory = VisualOptions.CommandFactory;

            SaveCommand =
                commandFactory.CreateCommand(() =>
                {
                    Element.Points = CalculatePoints(Chains);
                });

            CancelCommand =
                commandFactory.CreateCommand(() =>
                {
                    Chains = CalculateChains(Element.Points);
                });

            ResetCommand =
                commandFactory.CreateCommand(() => Element.ResetChains());

        }
        

        public double Width
        {
            get
            {
                var pixelPerUnit = VisualOptions.PixelPerUnit;
                return pixelPerUnit.Transform(Element.Width);
            }
        }
        public double Height
        {
            get
            {
                var pixelPerUnit = VisualOptions.PixelPerUnit;
                return pixelPerUnit.Transform(Element.Height);
            }
        }

        public DecorativeBorderViewModel Element { get; private set; }
        private VisualOptions VisualOptions { get; set; }

        private IList<IDrawViewModel> _chains;
        public IList<IDrawViewModel> Chains
        {
            get { return _chains; }
            set { SetField(ref _chains, value); }
        }

        public IList<IDrawViewModel> CalculateChains(IEnumerable<Point> points)
        {
            var pixelPerUnit = VisualOptions.PixelPerUnit;
            var pointArray = points.ToArray();
            if (!pointArray.Any())
            {
                return null;
            }

            var first = pointArray.First();
            var pixelFirst = pixelPerUnit.Transform(first);

            var finish = new PointViewModel(pixelFirst.X, pixelFirst.Y, Delete, VisualOptions.CommandFactory);
            var chains = new ObservableCollection<IDrawViewModel> { finish };

            foreach (var point in pointArray)
            {
                if (point == first)
                {
                    continue;
                }
                var start = finish;
                var pixelPoint = pixelPerUnit.Transform(point);
                finish = new PointViewModel(pixelPoint.X, pixelPoint.Y, Delete, VisualOptions.CommandFactory);
                chains.Insert(0, finish);
                chains.Add(new LineViewModel(start, finish, Insert, VisualOptions.CommandFactory));
            }
            return chains;
        }

        public IEnumerable<Point> CalculatePoints(IList<IDrawViewModel> chains)
        {
            var pixelPerUnit = VisualOptions.PixelPerUnit;
            var points = new List<Point>();

            var finishs = chains
                .OfType<LineViewModel>()
                .Select(line => line.Finish)
                .ToList();

            var start = chains
                .OfType<LineViewModel>()
                .Select(line => line.Start)
                .FirstOrDefault(point => !finishs.Contains(point));

            if (start == null)
            {
                return null;
            }

            while (true)
            {
                var current = chains
                    .OfType<LineViewModel>()
                    .FirstOrDefault(line => line.Start == start);

                points.Add(new Point(
                    start.X / pixelPerUnit.Value,
                    start.Y / pixelPerUnit.Value));

                if (current == null)
                {
                    break;
                }

                start = current.Finish;
            }
            return points;
        }

        public void Insert(PointTransit<ILineViewModel> pointTransit)
        {
            var line = pointTransit.Area;
            var point = pointTransit.Point;

            var current = new PointViewModel(point.X + line.X, point.Y + line.Y, Delete, VisualOptions.CommandFactory);
            var start = new LineViewModel(line.Start, current, Insert, VisualOptions.CommandFactory);
            var finish = new LineViewModel(current, line.Finish, Insert, VisualOptions.CommandFactory);

            Chains.Remove(line);
            Chains.Insert(Chains.Count - 1, current);
            Chains.Insert(0, start);
            Chains.Insert(0, finish);
        }

        public void Delete(IPointViewModel point)
        {
            var start = Chains
                .OfType<LineViewModel>()
                .FirstOrDefault(line => line.Start == point);
            var finish = Chains
                .OfType<LineViewModel>()
                .FirstOrDefault(line => line.Finish == point);

            if (start != null && finish != null)
            {
                var line = new LineViewModel(finish.Start, start.Finish, Insert, VisualOptions.CommandFactory);
                Chains.Remove(start);
                Chains.Remove(finish);
                Chains.Remove(point);
                Chains.Insert(0, line);
            }
        }

        public ICommand SaveCommand { get; set; }
        public ICommand ResetCommand { get; set; }
        public ICommand CancelCommand { get; set; }

    }
}
