using System.IO;
using System.Linq;

namespace PeletonSoft.Tools.Model.File
{
    public class FileBox : IFileBox
    {
        private readonly byte[] _data;
        public byte[] Data
        {
            get { return _data; }
        }

        private readonly string _extention;

        public string Extention
        {
            get { return _extention; }
        }

        public void WriteToFile(string fileName)
        {
            using (var stream = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                stream.Write(Data, 0, Data.Length);
                stream.Close();
            }
        }

        public FileBox(byte[] data)
        {
            _data = data;
            var attributes = GetType()
                .GetCustomAttributes(true)
                .OfType<FileExtentionAttribute>()
                .ToList();
            if (attributes.Any())
            {
                _extention = attributes.First().Extention;
            }
        }

        public FileBox(byte[] data, string extention)
        {
            _data = data;
            _extention = extention;
        }

    }
}
