using System;

namespace ToB.Common.Extensions
{
    public static class FuncExtensions
    {
        public static TOut _<TIn, TOut>(this TIn x, Func<TIn, TOut> f)
        {
            return f(x);
        }
    }
}