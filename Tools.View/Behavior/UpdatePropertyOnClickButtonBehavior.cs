using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Interactivity;

namespace PeletonSoft.Tools.View.Behavior
{
    public class UpdatePropertyOnClickButtonBehavior : Behavior<Button>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Click += AssociatedObjectOnClick;
        }

        private void AssociatedObjectOnClick(object sender, RoutedEventArgs routedEventArgs)
        {
            if (Element != null && Property != null)
            {
                var bindingExpression = BindingOperations.GetBindingExpression(Element, Property);
                bindingExpression?.UpdateSource();
                var multiBindingExpression = BindingOperations.GetMultiBindingExpression(Element, Property);
                multiBindingExpression?.UpdateSource();
            }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.Click -= AssociatedObjectOnClick;
        }

        public DependencyObject Element
        {
            get { return (DependencyObject) GetValue(ElementProperty); }
            set { SetValue(ElementProperty, value); }
        }

        public static readonly DependencyProperty ElementProperty = DependencyProperty.Register(
          nameof(Element), typeof(DependencyObject), typeof(UpdatePropertyOnClickButtonBehavior), new PropertyMetadata(null));

        public DependencyProperty Property
        {
            get { return (DependencyProperty) GetValue(PropertyProperty); }
            set { SetValue(PropertyProperty, value); }
        }

        public static readonly DependencyProperty PropertyProperty = DependencyProperty.Register(
          nameof(Property), typeof(DependencyProperty), typeof(UpdatePropertyOnClickButtonBehavior), new PropertyMetadata(null));
    }

}
