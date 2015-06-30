using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using PeletonSoft.Tools.Model;
using PeletonSoft.Tools.Model.File;

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

        public static PngImageBox ToPngImageBox(this BitmapSource source)
        {
            var frame = BitmapFrame.Create(source);
            byte[] data;

            var png = new PngBitmapEncoder();
            png.Frames.Add(frame);
            using (var mem = new MemoryStream())
            {
                png.Save(mem);
                data = mem.ToArray();
            }

            return new PngImageBox(data, frame.PixelWidth, frame.PixelHeight);
        }
    }
}
