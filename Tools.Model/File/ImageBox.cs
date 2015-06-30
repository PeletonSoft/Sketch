namespace PeletonSoft.Tools.Model.File
{
    public class ImageBox : FileBox
    {
        private readonly int _width;
        public int Width
        {
            get { return _width; }
        }

        private readonly int _height;
        public int Height
        {
            get { return _height; }
        }

        public ImageBox(byte[] data, string extention, int width, int height) 
            : base(data, extention)
        {
            _width = width;
            _height = height;
        }

        public ImageBox(byte[] data, int width, int height)
            : base(data)
        {
            _width = width;
            _height = height;
        }
    }
}
