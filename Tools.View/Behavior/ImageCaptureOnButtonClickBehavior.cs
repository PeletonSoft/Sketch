using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Interactivity;
using PeletonSoft.Tools.Model.File;

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
            if (BeforeCommand != null)
            {
                if (BeforeCommand.CanExecute(null))
                {
                    BeforeCommand.Execute(null);
                }
            }

            var element = CurrentControl.Current;
            if (element != null)
            {
                
                ImageBox = new PngImageBox(
                    element.ExportToPng(),
                    (int) element.ActualWidth,
                    (int) element.ActualHeight);
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


        public ImageBox ImageBox
        {
            get
            {
                return (ImageBox)GetValue(ImageBoxProperty);
            }
            set
            {
                SetValue(ImageBoxProperty, value);
            }
        }

        public static readonly DependencyProperty ImageBoxProperty = DependencyProperty.Register(
          "ImageBox", typeof(ImageBox), typeof(ImageCaptureOnButtonClickBehavior), new PropertyMetadata(null));

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
