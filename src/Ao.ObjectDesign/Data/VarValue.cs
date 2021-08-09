using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Data
{
    public abstract partial class VarValue : IEquatable<VarValue>, ICloneable, IVarValue
    {
        protected VarValue(object value)
        {
            Value = value;
            Convertible = value as IConvertible;

            if (Convertible is null && value is null)
            {
                if (value is null)
                {
                    TypeCode = TypeCode.Empty;
                }
                else
                {
                    throw new ArgumentException($"Not null, Type {value?.GetType()} can't convert to IConvertible");
                }
            }
            else
            {
                TypeCode = Convertible.GetTypeCode();
            }
        }

        protected VarValue(object value, TypeCode typeCode)
        {
            Value = value;
            Convertible = value as IConvertible;
            TypeCode = typeCode;
        }

        public IConvertible Convertible { get; }

        public object Value { get; }

        public TypeCode TypeCode { get; }

        public bool Equals(VarValue other)
        {
            if (other?.Value is null)
            {
                return Value is null;
            }
            return TypeCode == other.TypeCode && other.Value.Equals(Value);
        }
        public override bool Equals(object obj)
        {
            return Equals(obj as VarValue);
        }
        public override int GetHashCode()
        {
            return Value is null ? 0 : Value.GetHashCode();
        }
        public override string ToString()
        {
            return $"{{{TypeCode}, {Value}}}";
        }

        private string GetDebuggerDisplay()
        {
            return ToString();
        }

        object ICloneable.Clone()
        {
            return Clone();
        }
        public abstract VarValue Clone();

        public static bool operator ==(VarValue a, VarValue b)
        {
            if (a is null && b is null)
            {
                return true;
            }
            if (a is null || b is null)
            {
                return false;
            }
            return a.Equals(b);
        }
        public static bool operator !=(VarValue a, VarValue b)
        {
            if (a is null && b is null)
            {
                return false;
            }
            if (a is null || b is null)
            {
                return true;
            }
            return !a.Equals(b);
        }
    }
    [DebuggerDisplay("{" + nameof(ToString) + "(),nq}")]
    public abstract partial class VarValue<T> : VarValue, IEquatable<VarValue<T>>, IVarValue<T>
    {
        protected VarValue(T value) : base(value)
        {
        }

        protected VarValue(T value, TypeCode typeCode) : base(value, typeCode)
        {
        }

        public new T Value => (T)base.Value;

        public bool Equals(VarValue<T> other)
        {
            return base.Equals(other);
        }
    }
}
