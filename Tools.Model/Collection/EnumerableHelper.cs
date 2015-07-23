using System;
using System.Collections.Generic;
using System.Linq;

namespace PeletonSoft.Tools.Model.Collection
{
    public static class EnumerableHelper
    {
        public static IEnumerable<T> RemoveSequentialRepeats<T>(this IEnumerable<T> source)
        {
            using (var iterator = source.GetEnumerator())
            {
                var comparer = EqualityComparer<T>.Default;

                if (!iterator.MoveNext())
                    yield break;

                var current = iterator.Current;
                yield return current;

                while (iterator.MoveNext())
                {
                    if (comparer.Equals(iterator.Current, current))
                        continue;

                    current = iterator.Current;
                    yield return current;
                }
            }
        }

        public static IEnumerable<TResult> Zip<TFirst, TSecond, TThird, TResult>(
            this IEnumerable<TFirst> first,
            IEnumerable<TSecond> second,
            IEnumerable<TThird> third,
            Func<TFirst, TSecond, TThird, TResult> resultSelector)
        {
            return first
                .Zip(second, (f, s) => new {First = f, Second = s})
                .Zip(third, (c, t) => resultSelector(c.First, c.Second, t));

        }

        public static T Head<T>(this IEnumerable<T> list)
        {
            return list.First();
        }

        public static IEnumerable<T> Tail<T>(this IEnumerable<T> list)
        {
            return list.Where((e, i) => i > 0);
        }
        public static IEnumerable<T> Init<T>(this IEnumerable<T> list)
        {
            return list.Reverse().Tail().Reverse();
        }

        public static void ForEach<TK, TV>(this IEnumerable<KeyValuePair<TK, TV>> list, Action<TK,TV> action)
        {
            foreach (var pair in list)
            {
                action(pair.Key, pair.Value);
            }
        }

        public static void ForEach<T>(this IEnumerable<T> list, Action<T> action)
        {
            foreach (var item in list)
            {
                action(item);
            }
        }
    }
}
