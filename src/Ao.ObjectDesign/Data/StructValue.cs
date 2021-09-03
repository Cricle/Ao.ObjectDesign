using System;
using System.Diagnostics;

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
    [DebuggerDisplay("{" + nameof(ToString) + "(),nq}")]
    public class StructValue : VarValue<ValueType>,IVarValue<ValueType>
    {
        public StructValue(ValueType value) 
            : base(value)
        {
            Value = value;
        }

        public StructValue(ValueType value, TypeCode typeCode) : base(value, typeCode)
        {
            Value = value;
        }

        public new ValueType Value { get; }
       
        public override VarValue Clone()
        {
            return new StructValue(Value, TypeCode);
        }
    }
}
