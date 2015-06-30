using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeletonSoft.Tools.Model.File
{
    [FileExtention("png")]
    public class PngImageBox : ImageBox
    {
        public PngImageBox(byte[] data, int width, int height) : base(data, width, height)
        {
        }
    }
}
