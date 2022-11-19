using System;
namespace Ao.ObjectDesign.Designing
{
    [Flags]
    public enum TransformTypes
    {
        None = 0,
        Scale = 1,
        Rotate = Scale << 1,
        Translate = Scale << 2,
        Skew = Scale << 3
    }
}
