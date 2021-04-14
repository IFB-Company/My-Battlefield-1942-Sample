using System;

namespace Common.Extensions
{
    public static class GameExtensions
    {
        private static readonly Random _rng = new Random();
        
        public static T[] Shuffled<T>(this T[] source)
        {
            var shuffled = new T[source.Length];
            source.CopyTo(shuffled, 0);

            var len = source.Length;
            for (var i = 0; i < len - 1; i++)
            {
                var j = i + _rng.Next(len - i);
                var t = shuffled[j];
                shuffled[j] = shuffled[i];
                shuffled[i] = t;
            }
        
            return shuffled;
        }
    }
}

