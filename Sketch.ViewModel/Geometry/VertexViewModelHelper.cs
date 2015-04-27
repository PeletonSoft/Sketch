using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using PeletonSoft.Sketch.ViewModel.Geometry;

namespace SketchViewModel.Geometry
{
    public static class VertexViewModelHelper
    {
        public static VertexViewModel ToVertex(this Point point)
        {
            return new VertexViewModel(point);
        }
    }
}
