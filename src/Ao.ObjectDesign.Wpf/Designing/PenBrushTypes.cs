﻿using System;

namespace Ao.ObjectDesign.Designing
{
    [Flags]
    public enum PenBrushTypes
    {
        None = 0,
        Solid = 1,
        Liner = 2,
        Radial = 3,
        Image = 4
    }
}
