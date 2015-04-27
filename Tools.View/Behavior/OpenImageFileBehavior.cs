using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Interactivity;
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
                var parameter = fileDialog.FileName.GetTemporaryCopy();
                if (Command.CanExecute(parameter))
                {
                    Command.Execute(parameter);
                }
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
          "Command", typeof(ICommand), typeof(OpenImageFileBehavior), new PropertyMetadata(null));


        protected override void OnDetaching()
        {
            AssociatedObject.Click -= AssociatedObjectOnClick;


        }
    }
}
