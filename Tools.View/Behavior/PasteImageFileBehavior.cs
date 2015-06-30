using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Interactivity;

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
                var source = Clipboard.GetImage();
                var parameter = source.ToPngImageBox();

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
                return (ICommand)GetValue(CommandProperty);
            }
            set
            {
                SetValue(CommandProperty, value);
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
