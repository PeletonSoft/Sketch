using System.Windows;

namespace PeletonSoft.Sketch.ViewModel.Geometry
{
    public static class VertexViewModelHelper
    {
        public static VertexViewModel ToVertex(this Point point)
        {
            return new VertexViewModel(point);
        }
    }
}
