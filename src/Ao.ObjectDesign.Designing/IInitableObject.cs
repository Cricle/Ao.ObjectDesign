﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Designing
{
    public interface IInitableObject
    {
        bool IsInitialized { get; }

        void Initialize();
    }
}
