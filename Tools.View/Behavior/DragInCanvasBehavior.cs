using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace PeletonSoft.Tools.View.Behavior
{
    public class DragInCanvasBehavior : Behavior<UIElement>
    {

        private Canvas _canvas;
        private UIElement _object;
        private bool _isDragging;
        private Point _mouseOffset;



        protected override void OnAttached()
        {
            base.OnAttached();

            DependencyObject obj = AssociatedObject;
            while (true)
            {
                var cnv = VisualTreeHelper.GetParent(obj);

                if (cnv is Canvas)
                {
                    _canvas = (Canvas)cnv;
                    _object = (UIElement)obj;
                    break;
                }

                if (cnv == null)
                {
                    break;
                }

                obj = cnv;
            }

            if (_canvas != null)
            {
                _object.PreviewMouseLeftButtonDown += MouseLeftButtonDown;
                _object.PreviewMouseMove += MouseMove;
                _object.PreviewMouseLeftButtonUp += MouseLeftButtonUp;
            }

        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            if (_canvas != null)
            {
                _object.PreviewMouseLeftButtonDown += MouseLeftButtonDown;
                _object.PreviewMouseMove += MouseMove;
                _object.PreviewMouseLeftButtonUp += MouseLeftButtonUp;
            }

        }

        void MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            if (_isDragging)
            {
                AssociatedObject.ReleaseMouseCapture();
                _isDragging = false;
            }

        }



        void MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDragging)
            {
                var p = e.GetPosition(_canvas);
                _object.SetValue(Canvas.TopProperty, p.Y - _mouseOffset.Y);
                _object.SetValue(Canvas.LeftProperty, p.X - _mouseOffset.X);
            }
        }



        void MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _isDragging = true;
            _mouseOffset = e.GetPosition(_object);
            AssociatedObject.CaptureMouse();
        }

    }


}
