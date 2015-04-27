using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace PeletonSoft.Tools.View.Behavior
{
    public class DragBehavior : Behavior<FrameworkElement>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.MouseDown += AssociatedObjectOnMouseDown;
        }

        private void AssociatedObjectOnMouseDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            var dataObject = new DataObject(DataFormat, AssociatedObject.DataContext);
            DragDrop.DoDragDrop(AssociatedObject, dataObject, DragDropEffects.Copy);
        }
        
        public Type DataFormat
        {
            get
            {
                return (Type)GetValue(DataFormatProperty);
            }
            set
            {
                this.SetValue(DataFormatProperty, value);
            }
        }

        public static readonly DependencyProperty DataFormatProperty = DependencyProperty.Register(
          "DataFormat", typeof(Type), typeof(DragBehavior), new PropertyMetadata(typeof(object)));
        
        protected override void OnDetaching()
        {
            base.OnDetaching();

            AssociatedObject.MouseDown -= AssociatedObjectOnMouseDown;
        }
    }
}
