using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Ao.ObjectDesign.Designing
{
    public static class TypeHelper
    {
        internal static readonly Type NullableType = typeof(Nullable<>);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsNullable(Type type)
        {
            return type.IsGenericType &&
                type.GetGenericTypeDefinition() == NullableType;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static object SafeChangeType(object value, Type type)
        {
            if (TryChangeType(value, type, out _, out object res))
            {
                return res;
            }
            return null;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static object ChangeType(object value, Type type)
        {
            if (TryChangeType(value, type, out Exception ex, out object res))
            {
                return res;
            }
            throw ex;
        }
        public static bool TryChangeType(object value, Type type, out Exception ex, out object result)
        {
            ex = null;
            result = null;
            if (value is null)
            {
                return false;
            }
            if (IsConvertableType(type))
            {
                try
                {
                    result = Convert.ChangeType(value, type);
                    return true;
                }
                catch (Exception e)
                {
                    ex = e;
                    return false;
                }
            }
            if (IsNullable(type))
            {
                return TryChangeType(value, Nullable.GetUnderlyingType(type), out ex, out result);
            }
            if (type.IsEnum)
            {
                try
                {
                    result = Enum.Parse(type, value.ToString());
                    return true;
                }
                catch (Exception e)
                {
                    ex = e;
                    return false;
                }
            }
            return false;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool IsConvertableType(Type type)
        {
            Debug.Assert(type != null);
            return type.IsPrimitive || type == typeof(string) || type == typeof(decimal);
        }
        public static bool IsBaseType(Type type)
        {
            if (IsConvertableType(type))
            {
                return true;
            }
            if (IsNullable(type))
            {
                return IsBaseType(Nullable.GetUnderlyingType(type));
            }
            return false;
        }
    }
}
