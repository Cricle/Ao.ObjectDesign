using System;

namespace Ao.ObjectDesign.Data
{
    public static class ToAnyValueExtensions
    {
        public static AnyValue ToAny(this IVarValue value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return new AnyValue(value.Value, value.TypeCode);
        }
    }
}
