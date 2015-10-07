using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;
using PeletonSoft.Tools.Model.Dragable;

namespace PeletonSoft.Tools.View.Behavior
{
    public class PointOnMouseUpBehavior : Behavior<UIElement>
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
            InsertCommand?.Execute(new PointTransit(new Point(X, Y), Area));
        }

        public double X
        {
            get { return (double)GetValue(XProperty); }
            set { SetValue(XProperty, value); }
        }

        public static readonly DependencyProperty XProperty = DependencyProperty.Register(
          nameof(X), typeof(double), typeof(PointOnMouseUpBehavior), new PropertyMetadata(0.0));

        public double Y
        {
            get { return (double)GetValue(YProperty); }
            set { SetValue(YProperty, value); }
        }

        public static readonly DependencyProperty YProperty = DependencyProperty.Register(
          nameof(Y), typeof(double), typeof(PointOnMouseUpBehavior), new PropertyMetadata(0.0));


        public object Area
        {
            get { return GetValue(AreaProperty); }
            set { SetValue(AreaProperty, value); }
        }

        public static readonly DependencyProperty AreaProperty = DependencyProperty.Register(
          nameof(Area), typeof(object), typeof(PointOnMouseUpBehavior), new PropertyMetadata(null));

        public ICommand InsertCommand
        {
            get { return (ICommand)GetValue(InsertCommandProperty); }
            set { SetValue(InsertCommandProperty, value); }
        }

        public static readonly DependencyProperty InsertCommandProperty = DependencyProperty.Register(
          nameof(InsertCommand), typeof(ICommand), typeof(PointOnMouseUpBehavior), new PropertyMetadata(null));
    }
}
