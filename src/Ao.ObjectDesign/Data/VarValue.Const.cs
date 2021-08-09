using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Data
{
    public abstract partial class VarValue
    {
        public static readonly RefValue NullValue = new RefValue(null, TypeCode.Empty);

        public static readonly RefValue StringNullValue = new RefValue(null, TypeCode.String);
        public static readonly RefValue StringEmptyValue = new RefValue(string.Empty, TypeCode.String);

        public static readonly RefValue ObjectValue = new RefValue(new object(), TypeCode.Object);
        public static readonly RefValue DBNullValue = new RefValue(DBNull.Value, TypeCode.DBNull);

        public static readonly StructValue MinTimeValue = new StructValue(DateTime.MinValue, TypeCode.DateTime);
        public static readonly StructValue MaxTimeValue = new StructValue(DateTime.MaxValue, TypeCode.DateTime);

        public static readonly StructValue FalseValue = new StructValue(false, TypeCode.Boolean);
        public static readonly StructValue TrueValue = new StructValue(true, TypeCode.Boolean);

        public static readonly StructValue SByte0Value = new StructValue((sbyte)0, TypeCode.SByte);
        public static readonly StructValue Char0Value = new StructValue((char)0, TypeCode.Char);
        public static readonly StructValue Short0Value = new StructValue((short)0, TypeCode.Int16);
        public static readonly StructValue Int0Value = new StructValue(0, TypeCode.Int32);
        public static readonly StructValue Long0Value = new StructValue(0L, TypeCode.Int64);
        public static readonly StructValue Float0Value = new StructValue(0f, TypeCode.Single);
        public static readonly StructValue Double0Value = new StructValue(0d, TypeCode.Double);
        public static readonly StructValue Decimal0Value = new StructValue(0m, TypeCode.Decimal);

        public static readonly StructValue Byte0Value = new StructValue((byte)0, TypeCode.Byte);
        public static readonly StructValue UShort0Value = new StructValue((short)0, TypeCode.UInt16);
        public static readonly StructValue UInt0Value = new StructValue(0U, TypeCode.UInt32);
        public static readonly StructValue ULong0Value = new StructValue(0UL, TypeCode.UInt64);

    }
}
