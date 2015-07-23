using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeletonSoft.Sketch.Model.Interface.Element
{
    public interface IVisibleElement : IElement
    {
        double Opacity { get; set; }
        bool Visibility { get; set; }
    }
}
