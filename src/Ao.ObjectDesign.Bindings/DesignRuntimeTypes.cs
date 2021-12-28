using System;

namespace Ao.ObjectDesign.Bindings
{
    [Flags]
    public enum DesignRuntimeTypes
    {
        Unknow = 0,
        Designing = 1,
        Simulation = 2,
        Running = 3
    }
}
