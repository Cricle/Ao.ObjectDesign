using System;

namespace Ao.ObjectDesign.Data
{
    [Flags]
    public enum DesignUpdateSourceTrigger
    {
        Default = 0,
        PropertyChanged = 1,
        Explicit = 2
    }
}
