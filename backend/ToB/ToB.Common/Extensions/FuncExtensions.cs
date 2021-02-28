using System;

namespace ToB.Common.Extensions
{
    public static class FuncExtensions
    {
        public static TOut _<TIn, TOut>(this TIn x, Func<TIn, TOut> f)
        {
            return f(x);
        }

        public static void _<T>(this T x, Action<T> p)
        {
            p(x);
        }

        public static void _<TFirst, TSecond>(this TFirst x, Action<TFirst, TSecond> p, TSecond y)
        {
            p(x, y);
        }
    }
}