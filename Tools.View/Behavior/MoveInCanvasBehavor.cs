using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace PeletonSoft.Tools.View.Behavior
{
    public class MoveInCanvasBehavor : Behavior<UIElement>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.MouseMove += MouseMove;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.MouseMove -= MouseMove;
            base.OnDetaching();
        }

        private UIElement Control { get; set; }
        private Canvas Canvas { get; set; }

        private void MouseMove(object sender, MouseEventArgs e)
        {
            DependencyObject obj = AssociatedObject;
            if (Canvas == null)
            {
                while (true)
                {
                    var cnv = VisualTreeHelper.GetParent(obj);

                    if (cnv is Canvas)
                    {
                        Canvas = (Canvas)cnv;
                        Control = (UIElement)obj;
                        break;
                    }

                    if (cnv == null)
                    {
                        break;
                    }

                    obj = cnv;
                }
            }

            if (Canvas != null)
            {
                var p = e.GetPosition(Canvas);
                X = p.X - (double)Control.GetValue(Canvas.LeftProperty);
                Y = p.Y - (double)Control.GetValue(Canvas.TopProperty);
            }

        }

        public double X
        {
            get { return (double)GetValue(XProperty); }
            set { SetValue(XProperty, value); }
        }

        public static readonly DependencyProperty XProperty = DependencyProperty.Register(
          nameof(X), typeof(double), typeof(MoveInCanvasBehavor), new PropertyMetadata(0.0));

        public double Y
        {
            get { return (double)GetValue(YProperty); }
            set { SetValue(YProperty, value); }
        }

        public static readonly DependencyProperty YProperty = DependencyProperty.Register(
          nameof(Y), typeof(double), typeof(MoveInCanvasBehavor), new PropertyMetadata(0.0));

    }
}
