using System;

namespace Ao.ObjectDesign.Designing
{
    [Flags]
    public enum CursorTypes
    {
        None = 0,
        SystemCursorName = 1,
        FilePath = 2,
        Stream = 3
    }
}
