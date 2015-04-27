using System;
using System.Collections.Generic;
using System.Linq;

namespace PeletonSoft.Tools.Model.SketchMath.Wave
{
    public static class WavyBorderHelper
    {
        public static IWavyBorder<TResult> Zip<TFirst, TSecond, TResult>(
            this IWavyBorder<TFirst> first,
            IWavyBorder<TSecond> second,
            Func<TFirst, TSecond, TResult> resultSelector)
        {

            var waves = first.Waves
                .Zip(second.Waves,
                    (f, s) =>
                        new Wave<TResult>(
                            resultSelector(f.Peak, s.Peak),
                            resultSelector(f.InSeat, s.InSeat),
                            resultSelector(f.OutSeat, s.OutSeat)));

            var bottoms = first.Bottoms
                .Zip(second.Bottoms,
                    (f, s) =>
                        new Bottom<TResult>(
                            resultSelector(f.Start, s.Start),
                            resultSelector(f.Finish, s.Finish)));

            return new WavyBorder<TResult>(waves, bottoms);
        }

        public static IWavyBorder<TResult> Zip<TFirst, TSecond, TThird, TResult>(
            this IWavyBorder<TFirst> first,
            IWavyBorder<TSecond> second,
            IWavyBorder<TThird> third,
            Func<TFirst, TSecond, TThird, TResult> resultSelector)
        {
            return first
                .Zip(second, (f, s) => new {First = f, Second = s})
                .Zip(third, (c, t) => resultSelector(c.First, c.Second, t));
        }

        public static IWavyBorder<TR> Transform<T, TR>(this IWavyBorder<T> wavyBorder, Func<T, TR> transformer)
        {
            return new WavyBorder<TR>
            {
                Waves = wavyBorder.Waves
                    .Select(wave => wave.Transform(transformer)),
                Bottoms = wavyBorder.Bottoms
                    .Select(bottom => bottom.Transform(transformer))
            };
        }

        public static IWave<TR> Transform<T, TR>(this IWave<T> wave, Func<T, TR> transformer)
        {
            return new Wave<TR>(
                transformer(wave.Peak),
                transformer(wave.InSeat),
                transformer(wave.OutSeat));
        }

        public static IBottom<TR> Transform<T, TR>(this IBottom<T> bottom, Func<T, TR> transformer)
        {
            return new Bottom<TR>(
                transformer(bottom.Start),
                transformer(bottom.Finish));
        }

        public static IEnumerable<T> SelectMany<T>(this IWavyBorder<T> border)
        {
            var waves = border.Waves.SelectMany(wave => new[] { wave.InSeat, wave.OutSeat, wave.OutSeat });
            var bottoms = border.Bottoms.SelectMany(bottom => new[] { bottom.Start, bottom.Finish });
            return waves.Concat(bottoms);
        }

    }
}
