using System;

namespace Ao.ObjectDesign.Data
{
    public static class ForValueHelper
    {
        public static object ForValue(object val,TypeCode code,string timeFormatter=null)
        {
            switch (code)
            {
                case TypeCode.Boolean:
                    if (val is bool b)
                    {
                        return b;
                    }
                    return bool.Parse(val.ToString());
                case TypeCode.Byte:
                    if (val is byte by)
                    {
                        return by;
                    }
                    return byte.Parse(val.ToString());
                case TypeCode.Char:
                    if (val is char c)
                    {
                        return c;
                    }
                    return char.Parse(val.ToString());
                case TypeCode.DateTime:
                    if (val is DateTime dt)
                    {
                        return dt;
                    }
                    if (timeFormatter!=null)
                    {
                        return DateTime.ParseExact(val.ToString(), timeFormatter, null);
                    }
                    return DateTime.Parse(val.ToString());
                case TypeCode.DBNull:
                    return DBNull.Value;
                case TypeCode.Decimal:
                    if (val is decimal dec)
                    {
                        return dec;
                    }
                    return decimal.Parse(val.ToString());
                case TypeCode.Double:
                    if (val is double d)
                    {
                        return d;
                    }
                    return double.Parse(val.ToString());
                case TypeCode.Empty:
                    return null;
                case TypeCode.Int16:
                    if (val is short s)
                    {
                        return s;
                    }
                    return bool.Parse(val.ToString());
                case TypeCode.Int32:
                    if (val is int i)
                    {
                        return i;
                    }
                    return int.Parse(val.ToString());
                case TypeCode.Int64:
                    if (val is long l)
                    {
                        return l;
                    }
                    return long.Parse(val.ToString());
                case TypeCode.Object:
                    return val;
                case TypeCode.SByte:
                    if (val is sbyte sb)
                    {
                        return sb;
                    }
                    return sbyte.Parse(val.ToString());
                case TypeCode.Single:
                    if (val is float f)
                    {
                        return f;
                    }
                    return float.Parse(val.ToString());
                case TypeCode.String:
                    if (val is string str)
                    {
                        return str;
                    }
                    return val.ToString();
                case TypeCode.UInt16:
                    if (val is ushort us)
                    {
                        return us;
                    }
                    return ushort.Parse(val.ToString());
                case TypeCode.UInt32:
                    if (val is uint ui)
                    {
                        return ui;
                    }
                    return uint.Parse(val.ToString());
                case TypeCode.UInt64:
                    if (val is ulong ul)
                    {
                        return ul;
                    }
                    return ulong.Parse(val.ToString());
                default:
                    throw new NotSupportedException(code.ToString());
            }
        }
    }
}
