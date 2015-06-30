using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PeletonSoft.Tools.Model.File
{
    public static class FileHelper
    {
        public static IEnumerable<IFileBox> GetFiles(this IEnumerable<IEnumerable<IFileBox>> filess)
        {
            if (filess == null)
            {
                return null;
            }

            var filesList = filess
                .Where(files => files != null)
                .ToList();

            if (!filesList.Any())
            {
                return null;
            }

            List<IFileBox> list = null;

            foreach (var files in filesList)
            {
                if (list == null)
                {
                    list = new List<IFileBox>();
                }

                var range = files
                    .Where(file => file != null);
                list.AddRange(range);
            }

            return list;
        }

        public static string GetTemporaryCopy(this string fileName)
        {
            var copy = Path.GetTempFileName() +
                       Path.GetExtension(fileName);

            System.IO.File.Copy(fileName, copy, true);
            return copy;
        }
    }
}
