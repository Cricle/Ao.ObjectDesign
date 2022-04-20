﻿using System;
using System.ComponentModel;

namespace Ao.ObjectDesign.Data
{
    public abstract partial class VarValue : IEquatable<VarValue>, ICloneable, IVarValue
    {
        protected VarValue(object value)
        {
            Value = value;
            Convertible = value as IConvertible;

            if (Convertible is null)
            {
                if (value is null)
                {
                    TypeCode = TypeCode.Empty;
                }
                else
                {
                    throw new ArgumentException($"Not null, Type {value.GetType()} can't convert to IConvertible");
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

        public virtual object ConvertedValue => ForValueHelper.ForValue(Value, TypeCode);

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
        public bool TryTo(Type type, out Exception exception, out object result)
        {
            exception = null;
            result = null;

            if (Value is null || type == typeof(void))
            {
                return true;
            }
            else if (type.IsPrimitive || type == typeof(string) || type == typeof(decimal))
            {
                try
                {
                    var convert = TypeDescriptor.GetConverter(Value);
                    result = convert.ConvertTo(Value, type);
                    return true;
                }
                catch (Exception ex)
                {
                    exception = ex;
                    return false;
                }
            }
            else if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                TryTo(Nullable.GetUnderlyingType(type), out _, out result);
                return true;
            }
            exception = new InvalidCastException($"Can't case {Value} to {type}");
            return false;
        }
        public object To(Type type)
        {
            TryTo(type, out _, out var result);
            return result;
        }

        public static IVarValue FromObject(object value)
        {
            if (value is null)
            {
                return NullValue;
            }
            if (value is IVarValue var)
            {
                return var;
            }
            var type = value.GetType();
            if (type.IsPrimitive)
            {
                var val = (IConvertible)value;
                return new AnyValue(value, val.GetTypeCode());
            }
            else if (type == typeof(string))
            {
                return new RefValue(value.ToString(), TypeCode.String);
            }
            else if (type == typeof(decimal))
            {
                return new AnyValue(value, TypeCode.Decimal);
            }
            else if (type == typeof(DateTime))
            {
                return new AnyValue(value, TypeCode.DateTime);
            }
            else if (value == DBNull.Value)
            {
                return DBNullValue;
            }
            return new RefValue(value, TypeCode.Object);
        }
    }
}
