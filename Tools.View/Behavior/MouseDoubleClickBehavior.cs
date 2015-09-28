using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace PeletonSoft.Tools.View.Behavior
{
    public class MouseDoubleClickBehavior : Behavior<Control>
    {
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
            nameof(Command), typeof (ICommand), typeof (MouseDoubleClickBehavior),
            new PropertyMetadata(default(ICommand)));

        public ICommand Command
        {
            get { return (ICommand) GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.MouseDoubleClick += OnMouseDoubleClick;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.MouseDoubleClick -= OnMouseDoubleClick;
            base.OnDetaching();
        }

        private void OnMouseDoubleClick(object sender, RoutedEventArgs e)
        {
            Command?.Execute(null);
        }
    }

}
