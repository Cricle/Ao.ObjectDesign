using System;

namespace Ao.ObjectDesign.WpfDesign
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
