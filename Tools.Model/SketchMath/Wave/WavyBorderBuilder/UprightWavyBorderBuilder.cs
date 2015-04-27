using System.Collections.Generic;

namespace PeletonSoft.Tools.Model.SketchMath.Wave.WavyBorderBuilder
{
    public class UprightWavyBorderBuilder : BaseWavyBorderBuilder
    {
        public UprightWavyBorderBuilder(WavyBorderParameters parameters,
            IExtraStrategy extraStrategy) : base(parameters, extraStrategy)
        {
            WavyBorderOffset = new WavyBorderOffset(0, 0);
        }

        protected override WavyBorder<Position> Calculate()
        {
            var waves = new List<Wave<Position>>();
            var bottoms = new List<Bottom<Position>>();

            bottoms.Add(
                new Bottom<Position>(
                    new Position(0, 0),
                    new Position(ExtraStart, ExtraStart)));

            for (var i = 0; i < WaveCount; i++)
            {
                var root = ExtraStart + Step / 2 + i * Step;
                var rootPath = ExtraStart + Step / 2 + +WaveHeight + i * (Step + 2 * WaveHeight);
                waves.Add(
                    new Wave<Position>(
                        new Position(root, rootPath),
                        new Position(root + Step / 2, rootPath + WaveHeight + Step / 2),
                        new Position(root - Step / 2, rootPath - WaveHeight - Step / 2)));
                if (i > 0)
                {
                    bottoms.Add(
                        new Bottom<Position>(
                            new Position(root - Step / 2, rootPath - WaveHeight - Step / 2),
                            new Position(root - Step / 2, rootPath - WaveHeight - Step / 2)));
                }
            }

            bottoms.Add(
                new Bottom<Position>(
                    new Position(Width - ExtraFinish, FullWidth - ExtraFinish),
                    new Position(Width, FullWidth)));

            return new WavyBorder<Position>(waves, bottoms);
        }

    }
}
