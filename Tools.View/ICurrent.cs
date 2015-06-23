using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeletonSoft.Tools.View
{
    public interface ICurrent<T>
    {
        T Current { get; set; }
    }
}
