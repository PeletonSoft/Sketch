using System.Collections.Generic;

namespace PeletonSoft.Tools.Model.SketchMath.Wave
{
    public class WavyBorder<T> : IWavyBorder<T>
    {
        public IEnumerable<IWave<T>> Waves { get; set; }
        public IEnumerable<IBottom<T>> Bottoms { get; set; }

        public WavyBorder()
        {
        }

        public WavyBorder(IEnumerable<IWave<T>> waves, IEnumerable<IBottom<T>> bottoms)
        {
            Waves = waves;
            Bottoms = bottoms;
        }
    }
}