using System.Windows;
using PeletonSoft.Sketch.Model.Interface.Element;

namespace PeletonSoft.Sketch.Model.Element
{
    public class Overlay : IElement
    {
        #region implement IElement
        public string Description { get; set; }
        #endregion

        public double OverWidth { get; set; }
        public double OverHeight { get; set; }
        public double OverOffsetX { get; set; }
        public double OverOffsetY { get; set; }

        public Rect GetOverRect() => new Rect(OverOffsetX, OverOffsetY, OverWidth, OverHeight);
    }
}
