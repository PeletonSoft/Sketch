using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace PeletonSoft.Tools.View.Controls
{
    /// <summary>
    /// Логика взаимодействия для LineView.xaml
    /// </summary>
    public partial class LineView
    {
        public LineView()
        {
            InitializeComponent();
        }

        public double OffsetX
        {
            get { return (double) GetValue(OffsetXProperty); }
            set { SetValue(OffsetXProperty, value); }
        }

        public static readonly DependencyProperty OffsetXProperty = DependencyProperty.Register(
          nameof(OffsetX), typeof(double), typeof(LineView), new PropertyMetadata(0.0));


        public double OffsetY
        {
            get { return (double) GetValue(OffsetYProperty); }
            set { SetValue(OffsetYProperty, value); }
        }

        public static readonly DependencyProperty OffsetYProperty = DependencyProperty.Register(
          nameof(OffsetY), typeof(double), typeof(LineView), new PropertyMetadata(0.0));

        public double ThumbX
        {
            get { return (double) GetValue(ThumbXProperty); }
            set { SetValue(ThumbXProperty, value); }
        }

        public static readonly DependencyProperty ThumbXProperty = DependencyProperty.Register(
          nameof(ThumbX), typeof(double), typeof(LineView), new PropertyMetadata(0.0));

        public double ThumbY
        {
            get { return (double) GetValue(ThumbYProperty); }
            set { SetValue(ThumbYProperty, value); }
        }

        public static readonly DependencyProperty ThumbYProperty = DependencyProperty.Register(
          nameof(ThumbY), typeof(double), typeof(LineView), new PropertyMetadata(0.0));

        public Brush LineColor
        {
            get { return (Brush) GetValue(LineColorProperty); }
            set { SetValue(LineColorProperty, value); }
        }

        public static readonly DependencyProperty LineColorProperty = DependencyProperty.Register(
          nameof(LineColor), typeof(Brush), typeof(LineView), new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));

        public ICommand SplitCommand
        {
            get { return (ICommand)GetValue(SplitCommandProperty); }
            set { SetValue(SplitCommandProperty, value); }
        }

        public static readonly DependencyProperty SplitCommandProperty = DependencyProperty.Register(
          nameof(SplitCommand), typeof(ICommand), typeof(LineView), new PropertyMetadata(null));

    }

}
