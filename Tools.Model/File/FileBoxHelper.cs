using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeletonSoft.Tools.Model.File
{
    public static class FileBoxHelper
    {
        public static string GetFileName(this IFileBox fileBox, string name)
        {
            return String.Format("{0}.{1}", name, fileBox.Extention);
        }
    }
}
