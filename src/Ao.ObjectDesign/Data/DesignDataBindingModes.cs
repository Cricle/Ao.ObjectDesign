using System;

namespace Ao.ObjectDesign.Data
{
    [Flags]
    public enum DesignDataBindingModes
    {
        TwoWay = 0,
        OneWay = 1,
        OneTime = 2,
        OneWayToSource = 3,
        Defualt = 4,
    }
}
