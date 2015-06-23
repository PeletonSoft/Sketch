using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace PeletonSoft.Tools.View.Behavior
{
    public class ImageCaptureOnButtonClickBehavior : Behavior<ButtonBase>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Click += AssociatedObjectOnClick;
        }

        private void AssociatedObjectOnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            var element = CurrentControl.Current;
            if (element != null)
            {
                if (BeforeCommand != null)
                {
                    if (BeforeCommand.CanExecute(null))
                    {
                        BeforeCommand.Execute(null);
                    }
                }
                ImageData = element.ExportToPng();
            }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.Click -= AssociatedObjectOnClick;
        }
        public CurrentControl CurrentControl
        {
            get
            {
                return (CurrentControl)GetValue(CurrentControlProperty);
            }
            set
            {
                SetValue(CurrentControlProperty, value);
            }
        }

        public static readonly DependencyProperty CurrentControlProperty = DependencyProperty.Register(
          "CurrentControl", typeof(CurrentControl), typeof(ImageCaptureOnButtonClickBehavior), new PropertyMetadata(null));


        public byte[] ImageData
        {
            get
            {
                return (byte[])GetValue(ImageDataProperty);
            }
            set
            {
                SetValue(ImageDataProperty, value);
            }
        }

        public static readonly DependencyProperty ImageDataProperty = DependencyProperty.Register(
          "ImageData", typeof(byte[]), typeof(ImageCaptureOnButtonClickBehavior), new PropertyMetadata(null));

        public ICommand BeforeCommand
        {
            get
            {
                return (ICommand)GetValue(BeforeCommandProperty);
            }
            set
            {
                SetValue(BeforeCommandProperty, value);
            }
        }

        public static readonly DependencyProperty BeforeCommandProperty = DependencyProperty.Register(
          "BeforeCommand", typeof(ICommand), typeof(ImageCaptureOnButtonClickBehavior), new PropertyMetadata(null));

    }
}
