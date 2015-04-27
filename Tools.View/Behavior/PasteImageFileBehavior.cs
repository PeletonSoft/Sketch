using System;
using System.IO;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media.Imaging;

namespace PeletonSoft.Tools.View.Behavior
{
    public class PasteImageFileBehavior : Behavior<ButtonBase>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Click +=AssociatedObjectOnClick;
        }

        private void AssociatedObjectOnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            try
            {
                var bitmapSource = Clipboard.GetImage();
                var encoder = new JpegBitmapEncoder();
                var outputFrame = BitmapFrame.Create(bitmapSource);
                encoder.Frames.Add(outputFrame);
                encoder.QualityLevel = 80;
                var fileName = Path.GetTempFileName();
                using (var file = File.OpenWrite(fileName))
                {
                    encoder.Save(file);
                }

                var parameter = fileName;
                if (Command.CanExecute(parameter))
                {
                    Command.Execute(parameter);
                }
            }
            catch (Exception)
            {
            }
        }


        public ICommand Command
        {
            get
            {
                return (ICommand)this.GetValue(CommandProperty);
            }
            set
            {
                this.SetValue(CommandProperty, value);
            }
        }

        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
          "Command", typeof(ICommand), typeof(PasteImageFileBehavior), new PropertyMetadata(null));


        protected override void OnDetaching()
        {
            AssociatedObject.Click -= AssociatedObjectOnClick;


        }
    }
}
