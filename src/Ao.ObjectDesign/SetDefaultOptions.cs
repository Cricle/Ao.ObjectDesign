using System;

namespace Ao.ObjectDesign
{
    [Flags]
    public enum SetDefaultOptions
    {
        None,
        IgnoreNoAttribute = 1,
        IgnoreNotNull = IgnoreNoAttribute << 1,
        ClassGenerateNew = IgnoreNoAttribute << 2,
        Deep = IgnoreNoAttribute << 3
    }
}
