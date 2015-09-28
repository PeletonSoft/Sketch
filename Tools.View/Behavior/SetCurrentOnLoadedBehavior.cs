using System;
using System.Windows;
using System.Windows.Interactivity;

namespace PeletonSoft.Tools.View.Behavior
{
    public class SetCurrentOnLoadedBehavior :  Behavior<FrameworkElement>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Loaded +=AssociatedObjectOnLoaded;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.Loaded -= AssociatedObjectOnLoaded;
            base.OnDetaching();
        }

        private void AssociatedObjectOnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            CurrentControl.Current = AssociatedObject;
        }

        public CurrentControl CurrentControl
        {
            get { return (CurrentControl) GetValue(CurrentControlProperty); }
            set { SetValue(CurrentControlProperty, value); }
        }

        public static readonly DependencyProperty CurrentControlProperty = DependencyProperty.Register(
          nameof(CurrentControl), typeof(CurrentControl), typeof(SetCurrentOnLoadedBehavior), new PropertyMetadata(null));
    }
}
