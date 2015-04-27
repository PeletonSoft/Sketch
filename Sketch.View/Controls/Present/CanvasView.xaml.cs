using System.Windows;
using PeletonSoft.Sketch.ViewModel.Visual;

namespace PeletonSoft.Sketch.View.Controls.Present
{
    /// <summary>
    /// Логика взаимодействия для CanvasView.xaml
    /// </summary>
    public partial class CanvasView
    {
        public CanvasView()
        {
            InitializeComponent();
        }

        public ScreenVisualViewModel Screen
        {
            get { return (ScreenVisualViewModel)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
          "Screen", typeof(ScreenVisualViewModel), typeof(CanvasView), new PropertyMetadata(null));
    }
}
