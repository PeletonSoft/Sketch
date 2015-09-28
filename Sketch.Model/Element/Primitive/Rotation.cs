using System;
using System.Windows;

namespace PeletonSoft.Sketch.Model.Element.Primitive
{
    public abstract class Rotation
    {
        public double Angle { get; private set; }

        public Rotation(double angle)
        {
            Angle = angle / 180 * Math.PI;
        }

        public Size Rotate(Size size)
        {
            return new Size(
                Math.Abs(size.Width * Math.Cos(Angle) + size.Height*Math.Sin(Angle)),
                Math.Abs(size.Width * Math.Sin(Angle) + size.Height * Math.Cos(Angle))
                );
        }
    }
}
