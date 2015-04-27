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
            //AssociatedObject.IsEnabled = false;
            if (Element != null && Property != null)
            {
                var bindingExpression = BindingOperations.GetBindingExpression(Element, Property);
                if (bindingExpression != null)
                {
                    bindingExpression.UpdateSource();
                    
                }

                var multiBindingExpression = BindingOperations.GetMultiBindingExpression(Element, Property);
                if (multiBindingExpression != null)
                {
                    multiBindingExpression.UpdateSource();
                }
                
            }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.Click -= AssociatedObjectOnClick;
        }

        public DependencyObject Element
        {
            get
            {
                return (DependencyObject)this.GetValue(ElementProperty);
            }
            set
            {
                this.SetValue(ElementProperty, value);
            }
        }

        public static readonly DependencyProperty ElementProperty = DependencyProperty.Register(
          "Element", typeof(DependencyObject), typeof(UpdatePropertyOnClickButtonBehavior), new PropertyMetadata(null));

        public DependencyProperty Property
        {
            get
            {
                return (DependencyProperty)this.GetValue(PropertyProperty);
            }
            set
            {
                this.SetValue(PropertyProperty, value);
            }
        }

        public static readonly DependencyProperty PropertyProperty = DependencyProperty.Register(
          "Property", typeof(DependencyProperty), typeof(UpdatePropertyOnClickButtonBehavior), new PropertyMetadata(null));
    }

}
