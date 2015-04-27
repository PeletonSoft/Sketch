using System.Windows;
using System.Windows.Controls;

// ReSharper disable once CheckNamespace
namespace PeletonSoft.Tools.View.Controls
{
    /// <summary>
    /// Логика взаимодействия для NumericIntBox.xaml
    /// </summary>
    public partial class NumericIntBox
    {
        public NumericIntBox()
        {
            InitializeComponent();
        }

        public int Value
        {
            get
            {
                return (int)GetValue(ValueProperty);
            }
            set
            {
                SetValue(ValueProperty, value);
            }
        }


        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
          "Value", typeof(int), typeof(NumericIntBox), new PropertyMetadata(0));

    }
}
