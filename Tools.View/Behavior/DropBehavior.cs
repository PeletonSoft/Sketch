using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;
using PeletonSoft.Tools.Model.Dragable;

namespace PeletonSoft.Tools.View.Behavior
{
    public class DropBehavior : Behavior<FrameworkElement>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Drop += AssociatedObjectOnDrop;
            AssociatedObject.DragEnter += AssociatedObjectOnDragEnter;
        }

        private void AssociatedObjectOnDragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormat))
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private void AssociatedObjectOnDrop(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormat))
            {
                return;
            }

            var param = new DataTransition(
                e.Data.GetData(DataFormat), 
                AssociatedObject.DataContext);


            Command?.Execute(param);
        }

        public Type DataFormat
        {
            get { return (Type) this.GetValue(DataFormatProperty); }
            set { SetValue(DataFormatProperty, value); }
        }

        public static readonly DependencyProperty DataFormatProperty = DependencyProperty.Register(
          nameof(DataFormat), typeof(Type), typeof(DropBehavior), new PropertyMetadata(typeof(object)));

        public ICommand Command
        {
            get { return (ICommand) this.GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
          nameof(Command), typeof(ICommand), typeof(DropBehavior), new PropertyMetadata(null));        
        protected override void OnDetaching()
        {
            base.OnDetaching();

            AssociatedObject.Drop -= AssociatedObjectOnDrop;
            AssociatedObject.DragEnter -= AssociatedObjectOnDragEnter;
        }
    }
}
