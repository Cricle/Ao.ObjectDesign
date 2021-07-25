using System;

namespace Ao.ObjectDesign.Designing
{
    [Flags]
    public enum CommandWaysOperatorTypes : byte
    {
        Add = 0,
        Remove = 1,
        Clear = 2
    }
}
