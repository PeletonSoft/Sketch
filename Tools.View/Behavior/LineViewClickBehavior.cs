using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;
using PeletonSoft.Tools.Model.Dragable;

namespace PeletonSoft.Tools.View.Behavior
{
    public class LineViewClickBehavior : Behavior<UserControl>
    {
        private ILineView LineView { get; set; }

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
            nameof(Command), typeof (ICommand), typeof (LineViewClickBehavior), new PropertyMetadata(default(ICommand)));


        protected override void OnAttached()
        {
            base.OnAttached();
            LineView = AssociatedObject as ILineView;
            if (LineView != null)
            {
                LineView.ThumbClick += OnThumbClick;
            }
        }

        private void OnThumbClick(object sender, RoutedEventArgs routedEventArgs)
        {
            if (Command != null)
            {
                var point = new Point(LineView.ThumbX, LineView.ThumbY);
                var line = LineView.GetDataContext();
                var param = new InsertPointTransit {Line = line, Point = point};
                Command.Execute(param);
            }
        }

        protected override void OnDetaching()
        {
            LineView.ThumbClick -= OnThumbClick;
            base.OnDetaching();
        }


    }

}
