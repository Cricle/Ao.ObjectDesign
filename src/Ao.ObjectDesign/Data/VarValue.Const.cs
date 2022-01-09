using System;

namespace Ao.ObjectDesign.Data
{
    public abstract partial class VarValue
    {
        public static readonly RefValue NullValue = new RefValue(null, TypeCode.Empty);

        public static readonly RefValue StringNullValue = new RefValue(null, TypeCode.String);
        public static readonly RefValue StringEmptyValue = new RefValue(string.Empty, TypeCode.String);

        public static readonly RefValue ObjectValue = new RefValue(new object(), TypeCode.Object);
        public static readonly RefValue DBNullValue = new RefValue(DBNull.Value, TypeCode.DBNull);

        public static readonly AnyValue MinTimeValue = new AnyValue(DateTime.MinValue, TypeCode.DateTime);
        public static readonly AnyValue MaxTimeValue = new AnyValue(DateTime.MaxValue, TypeCode.DateTime);

        public static readonly AnyValue FalseValue = new AnyValue(false, TypeCode.Boolean);
        public static readonly AnyValue TrueValue = new AnyValue(true, TypeCode.Boolean);

        public static readonly AnyValue SByte0Value = new AnyValue((sbyte)0, TypeCode.SByte);
        public static readonly AnyValue Char0Value = new AnyValue((char)0, TypeCode.Char);
        public static readonly AnyValue Short0Value = new AnyValue((short)0, TypeCode.Int16);
        public static readonly AnyValue Int0Value = new AnyValue(0, TypeCode.Int32);
        public static readonly AnyValue Long0Value = new AnyValue(0L, TypeCode.Int64);
        public static readonly AnyValue Float0Value = new AnyValue(0f, TypeCode.Single);
        public static readonly AnyValue Double0Value = new AnyValue(0d, TypeCode.Double);
        public static readonly AnyValue Decimal0Value = new AnyValue(0m, TypeCode.Decimal);

        public static readonly AnyValue Byte0Value = new AnyValue((byte)0, TypeCode.Byte);
        public static readonly AnyValue UShort0Value = new AnyValue((short)0, TypeCode.UInt16);
        public static readonly AnyValue UInt0Value = new AnyValue(0U, TypeCode.UInt32);
        public static readonly AnyValue ULong0Value = new AnyValue(0UL, TypeCode.UInt64);

    }
}
