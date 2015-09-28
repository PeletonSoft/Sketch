using System.Windows;

namespace PeletonSoft.Tools.View.Controls
{
    public class ImageSourceTitleUserControl : ImageSourceUserControl
    {

        public string Title
        {
            get { return (string) GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
          nameof(Title), typeof(string), typeof(ImageSourceTitleUserControl), new PropertyMetadata(null));

    }
}
