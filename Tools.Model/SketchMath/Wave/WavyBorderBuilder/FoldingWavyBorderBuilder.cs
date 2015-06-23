using System.Collections.Generic;

namespace PeletonSoft.Tools.Model.SketchMath.Wave.WavyBorderBuilder
{
    public class FoldingWavyBorderBuilder : BaseWavyBorderBuilder
    {

        public FoldingWavyBorderBuilder(WavyBorderParameters parameters,
            IExtraStrategy extraStrategy)
            : base(parameters, extraStrategy)
        {
            WavyBorderOffset = new WavyBorderOffset(WaveHeight, -0.8);
        }

        protected override WavyBorder<Position> Calculate()
        {
            var waves = new List<Wave<Position>>();
            var bottoms = new List<Bottom<Position>>();
            double start = 0;
            double path = 0;
            for (var i = 0; i < WaveCount; i++)
            {
                var root = ExtraStart + i*Step;
                var rootPath = ExtraStart + i*(Step + 2*WaveHeight);
                waves.Add(
                    new Wave<Position>(
                        new Position(root + WaveHeight, rootPath + WaveHeight),                        
                        new Position(root, rootPath + 2*WaveHeight),
                        new Position(root, rootPath)));

                bottoms.Add(
                    new Bottom<Position>(
                        new Position(start, path), 
                        new Position(root, rootPath)));

                start = root;
                path = rootPath;
            }

            bottoms.Add(
                new Bottom<Position>(
                    new Position(start, path + 2*WaveHeight), 
                    new Position(Width, FullWidth)));

            return new WavyBorder<Position>(waves,bottoms);
        }


    }
}
