using System;
using System.Diagnostics;

namespace Ao.ObjectDesign.Data
{
    [DebuggerDisplay("{" + nameof(ToString) + "(),nq}")]
    public class RefValue : VarValue, IVarValue<object>
    {
        public RefValue(object value) 
            : base(value)
        {
        }

        public RefValue(object value, TypeCode typeCode)
            : base(value, typeCode)
        {
        }

        public override VarValue Clone()
        {
            return new RefValue(Value, TypeCode);
        }
    }
}
