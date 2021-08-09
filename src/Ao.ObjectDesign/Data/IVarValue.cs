using System;

namespace Ao.ObjectDesign.Data
{
    public interface IVarValue : ICloneable
    {
        IConvertible Convertible { get; }

        object Value { get; }

        TypeCode TypeCode { get; }
    }
    public interface IVarValue<out T> : IVarValue
    {
        new T Value { get; }
    }
}
