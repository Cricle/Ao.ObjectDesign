using System;
using System.Diagnostics;

namespace Ao.ObjectDesign.Data
{
    [DebuggerDisplay("{" + nameof(ToString) + "(),nq}")]
    public struct AnyValue : IVarValue, ICloneable, IEquatable<AnyValue>
    {
        public AnyValue(object value, TypeCode typeCode)
        {
            Value = value;
            TypeCode = typeCode;
        }

        public object Value { get; }

        public TypeCode TypeCode { get; }

        public AnyValue Clone()
        {
            return new AnyValue(Value, TypeCode);
        }

        public bool Equals(AnyValue other)
        {
            return other.TypeCode == TypeCode &&
                other.Value == Value;
        }
        public override bool Equals(object obj)
        {
            if (obj is AnyValue)
            {
                return Equals((AnyValue)obj);
            }
            return false;
        }
        public override int GetHashCode()
        {
            var b = 0;
            if (Value != null)
            {
                b = Value.GetHashCode();
            }
            return b ^ (int)TypeCode;
        }

        object ICloneable.Clone()
        {
            return Clone();
        }
        public override string ToString()
        {
            return $"{{Value:{Value}, TypeCode:{TypeCode}}}";
        }

        public static bool operator ==(AnyValue a, AnyValue b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(AnyValue a, AnyValue b)
        {
            return !a.Equals(b);
        }
    }
}
