using System.Windows;

namespace PeletonSoft.Tools.View.Controls
{
    /// <summary>
    /// Логика взаимодействия для SelectPointMarker.xaml
    /// </summary>
    public partial class SelectPointMarker
    {
        public SelectPointMarker()
        {
            InitializeComponent();
        }

        public double MarkerRadius
        {
            get { return (double)GetValue(MarkerRadiusProperty); }
            set { SetValue(MarkerRadiusProperty, value); }
        }

        public static readonly DependencyProperty MarkerRadiusProperty = DependencyProperty.Register(
          "MarkerRadius", typeof(double), typeof(SelectPointMarker), new PropertyMetadata(0.0));

        public double MarkerOpacity
        {
            get { return (double)GetValue(MarkerOpacityProperty); }
            set { SetValue(MarkerOpacityProperty, value); }
        }

        public static readonly DependencyProperty MarkerOpacityProperty = DependencyProperty.Register(
          "MarkerOpacity", typeof(double), typeof(SelectPointMarker), new PropertyMetadata(0.0));
    }
}
