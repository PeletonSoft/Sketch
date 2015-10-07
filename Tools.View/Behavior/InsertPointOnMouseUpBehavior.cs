using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;
using PeletonSoft.Tools.Model.Dragable;
using PeletonSoft.Tools.Model.Draw;

namespace PeletonSoft.Tools.View.Behavior
{
    public class InsertPointOnMouseUpBehavior : Behavior<UIElement>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.MouseUp += MouseUp;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.MouseUp -= MouseUp;
            base.OnDetaching();
        }

        private void MouseUp(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            InsertCommand?.Execute(new InsertPointTransit(new Point(X, Y), Line));
        }

        public double X
        {
            get { return (double)GetValue(XProperty); }
            set { SetValue(XProperty, value); }
        }

        public static readonly DependencyProperty XProperty = DependencyProperty.Register(
          nameof(X), typeof(double), typeof(InsertPointOnMouseUpBehavior), new PropertyMetadata(0.0));

        public double Y
        {
            get { return (double)GetValue(YProperty); }
            set { SetValue(YProperty, value); }
        }

        public static readonly DependencyProperty YProperty = DependencyProperty.Register(
          nameof(Y), typeof(double), typeof(InsertPointOnMouseUpBehavior), new PropertyMetadata(0.0));


        public ILineViewModel Line
        {
            get { return (ILineViewModel)GetValue(LineProperty); }
            set { SetValue(LineProperty, value); }
        }

        public static readonly DependencyProperty LineProperty = DependencyProperty.Register(
          nameof(Line), typeof(ILineViewModel), typeof(InsertPointOnMouseUpBehavior), new PropertyMetadata(null));

        public ICommand InsertCommand
        {
            get { return (ICommand)GetValue(InsertCommandProperty); }
            set { SetValue(InsertCommandProperty, value); }
        }

        public static readonly DependencyProperty InsertCommandProperty = DependencyProperty.Register(
          nameof(InsertCommand), typeof(ICommand), typeof(InsertPointOnMouseUpBehavior), new PropertyMetadata(null));
    }
}
