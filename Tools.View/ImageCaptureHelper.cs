using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PeletonSoft.Tools.View
{
    public static class ImageCaptureHelper
    {
        public static byte[] ExportToPng(this FrameworkElement surface)
        {
            var transform = surface.LayoutTransform;
            surface.LayoutTransform = null;

            var size = new Size(surface.ActualWidth, surface.ActualHeight);
            surface.Measure(size);
            surface.Arrange(new Rect(size));

            var renderBitmap = new RenderTargetBitmap((int)size.Width, (int)size.Height, 96d, 96d, PixelFormats.Pbgra32);
            renderBitmap.Render(surface);

            try
            {
                using (var outStream = new MemoryStream())
                {
                    var encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(renderBitmap));
                    encoder.Save(outStream);
                    return outStream.ToArray();
                }
            }
            finally 
            {
                surface.LayoutTransform = transform;
            }
        }
    }
}
