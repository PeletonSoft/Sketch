using System;
using System.IO;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using PeletonSoft.Tools.Model;

namespace PeletonSoft.Tools.View.Behavior
{
    public class OpenImageFileBehavior : Behavior<ButtonBase>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Click +=AssociatedObjectOnClick;
        }

        private void AssociatedObjectOnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            var fileDialog = new OpenFileDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures),
                Filter = "PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif",
                DefaultExt = "png",
                AddExtension = false
            };

            if (fileDialog.ShowDialog() == true && Command != null)
            {
                var source = new BitmapImage(new Uri(fileDialog.FileName));
                var parameter = source.ToPngImageBox();

                if (Command.CanExecute(parameter))
                {
                    Command.Execute(parameter);
                }
            }
        }

        public ICommand Command
        {
            get { return (ICommand) GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
          nameof(Command), typeof(ICommand), typeof(OpenImageFileBehavior), new PropertyMetadata(null));


        protected override void OnDetaching()
        {
            AssociatedObject.Click -= AssociatedObjectOnClick;
        }
    }
}
