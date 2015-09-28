using System.Windows;
using System.Windows.Controls;

namespace PeletonSoft.Tools.View.Controls
{
    /// <summary>
    /// Логика взаимодействия для NumericBox.xaml
    /// </summary>
    public partial class NumericBox
    {
        public NumericBox()
        {
            InitializeComponent();
        }

        public double Value
        {
            get { return (double) GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
          nameof(Value), typeof(double), typeof(NumericBox), new PropertyMetadata(0.0));

        public string StringFormat
        {
            get { return (string) GetValue(StringFormatProperty); }
            set { SetValue(StringFormatProperty, value); }
        }


        public static readonly DependencyProperty StringFormatProperty = DependencyProperty.Register(
          nameof(StringFormat), typeof(string), typeof(NumericBox), new PropertyMetadata("{0:N3}"));
    }
}
