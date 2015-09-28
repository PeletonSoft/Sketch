using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace PeletonSoft.Tools.View.Behavior
{
    public class OnLoadBehavior : Behavior<FrameworkElement>
    {

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Loaded += AssociatedObjectOnLoaded;
        }

        private void AssociatedObjectOnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (Command.CanExecute(null))
            {
                Command.Execute(null);    
            }
        }

        protected override void OnDetaching()
        {
            AssociatedObject.Loaded -= AssociatedObjectOnLoaded;
        }

        public ICommand Command
        {
            get { return (ICommand) GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
          nameof(Command), typeof(ICommand), typeof(OnLoadBehavior), new PropertyMetadata(null));
    }
}
