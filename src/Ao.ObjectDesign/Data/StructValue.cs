using System;
using System.Diagnostics;

namespace Ao.ObjectDesign.Data
{
    [DebuggerDisplay("{" + nameof(ToString) + "(),nq}")]
    public class StructValue : VarValue, IVarValue<ValueType>
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

        public new ValueType ConvertedValue => (ValueType)ForValueHelper.ForValue(Value, TypeCode);

        public override VarValue Clone()
        {
            return new StructValue(Value, TypeCode);
        }
    }
}
