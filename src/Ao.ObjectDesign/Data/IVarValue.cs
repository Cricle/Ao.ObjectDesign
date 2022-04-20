using System;

namespace Ao.ObjectDesign.Data
{
    public interface IVarValue : ICloneable
    {
        object Value { get; }
        
        object ConvertedValue { get; }

        TypeCode TypeCode { get; }
    }
    public interface IVarValue<out T> : IVarValue
    {
        new T Value { get; }

        new T ConvertedValue { get; }
    }
}
