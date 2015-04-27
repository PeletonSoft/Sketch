using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PeletonSoft.Tools.View.Controls
{
    public class ImageSourceUserControl : UserControl
    {
        public ImageSource ImageSource
        {
            get
            {
                return (ImageSource)GetValue(ImageSourceProperty);
            }
            set
            {
                SetValue(ImageSourceProperty, value);
            }
        }

        public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register(
          "ImageSource", typeof(ImageSource), typeof(ImageSourceUserControl), new PropertyMetadata(null));

    }
}
