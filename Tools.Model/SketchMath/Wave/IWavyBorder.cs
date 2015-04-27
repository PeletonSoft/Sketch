using System.Collections.Generic;

namespace PeletonSoft.Tools.Model.SketchMath.Wave
{
    public interface IWavyBorder<T>
    {
        IEnumerable<IWave<T>> Waves { get; set; }
        IEnumerable<IBottom<T>> Bottoms { get; set; }
    }
}