using System;

namespace Ao.ObjectDesign.Data
{
    [Flags]
    public enum ChangeModes
    {
        New = 0,
        Change = 1,
        Reset = 2
    }
}
