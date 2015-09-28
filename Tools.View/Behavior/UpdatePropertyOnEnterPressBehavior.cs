using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace PeletonSoft.Tools.View.Behavior
{
    public class UpdatePropertyOnEnterPressBehavior : Behavior<UIElement>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.PreviewKeyDown += AssociatedObjectOnPreviewKeyDown;
        }

        private void AssociatedObjectOnPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                if (Element != null && Property != null)
                {
                    var bindingExpression = BindingOperations.GetBindingExpression(Element, Property);
                    bindingExpression?.UpdateSource();
                    var multiBindingExpression = BindingOperations.GetMultiBindingExpression(Element, Property);
                    multiBindingExpression?.UpdateSource();
                }
            }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.PreviewKeyDown -= AssociatedObjectOnPreviewKeyDown;
        }

        public DependencyObject Element
        {
            get { return (DependencyObject) GetValue(ElementProperty); }
            set { SetValue(ElementProperty, value); }
        }

        public static readonly DependencyProperty ElementProperty = DependencyProperty.Register(
          nameof(Element), typeof(DependencyObject), typeof(UpdatePropertyOnEnterPressBehavior), new PropertyMetadata(null));

        public DependencyProperty Property
        {
            get { return (DependencyProperty) GetValue(PropertyProperty); }
            set { SetValue(PropertyProperty, value); }
        }

        public static readonly DependencyProperty PropertyProperty = DependencyProperty.Register(
          nameof(Property), typeof(DependencyProperty), typeof(UpdatePropertyOnEnterPressBehavior), new PropertyMetadata(null));
    }

}
