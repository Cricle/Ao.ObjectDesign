﻿using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Session
{
    public interface ISceneFetcher<TScene>
    {
        TScene Get(IFileInfo file);
    }
}
