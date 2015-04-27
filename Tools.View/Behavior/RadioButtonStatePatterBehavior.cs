using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Interactivity;
using PeletonSoft.Tools.View.Converter;

namespace PeletonSoft.Tools.View.Behavior
{
    public class ToggleButtonStatePatternBehavior : Behavior<ToggleButton>
    {
        
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Click += AssociatedObjectOnChecked;

            var binding = new MultiBinding();
            binding.Converter = new StatePatternToBooleanConverter();
        
            binding.Bindings.Add(new Binding("State") {Source = this});
            binding.Bindings.Add(new Binding("TargetState") {Source = this});

            AssociatedObject.SetBinding(ToggleButton.IsCheckedProperty, binding);
        }

        private void AssociatedObjectOnChecked(object sender, RoutedEventArgs e)
        {
            if (AssociatedObject.IsChecked == true)
            {
                if (State != null && TargetState != null &&
                    State.GetType() != TargetState.GetType())
                {
                    State = TargetState;
                }
            }

        }

        public object State
        {
            get
            {
                return GetValue(StateProperty);
            }
            set
            {
                SetValue(StateProperty, value);
            }
        }

        public static readonly DependencyProperty StateProperty = DependencyProperty.Register(
          "State", typeof(object), typeof(ToggleButtonStatePatternBehavior), new PropertyMetadata(null));

        public object TargetState
        {
            get
            {
                return GetValue(TargetStateProperty);
            }
            set
            {
                SetValue(TargetStateProperty, value);
            }
        }

        public static readonly DependencyProperty TargetStateProperty = DependencyProperty.Register(
          "TargetState", typeof(object), typeof(ToggleButtonStatePatternBehavior), new PropertyMetadata(null));
        
        protected override void OnDetaching()
        {
            base.OnDetaching();

            AssociatedObject.Click += AssociatedObjectOnChecked;
        }
    }
}
