using System;
using System.Linq.Expressions;

namespace Common.Utils
{
    internal static class ConverterHolder<TIn, TOut>
    {
        public static readonly Expression<Func<TIn, TOut>> Converter;

        static ConverterHolder()
        {
            var parameter = Expression.Parameter(typeof(TIn));
            Converter = Expression.Lambda<Func<TIn, TOut>>(
                Expression.Convert(parameter, typeof(TOut)),
                parameter
            );
        }
    }
}