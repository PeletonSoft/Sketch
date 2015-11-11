using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeletonSoft.Tools.Model.Memento;

namespace PeletonSoft.Sketch.ViewModel.DataTransfer.Element.Primitive
{
    public sealed class TieBackSideDataTransfer : IDataTransfer
    {
        public double Weight { get; set; }
        public double TailScatter { get; set; }

    }
}
