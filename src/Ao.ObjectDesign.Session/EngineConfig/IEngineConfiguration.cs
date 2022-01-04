﻿using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Session.Environment;
using System;

namespace Ao.ObjectDesign.Session.EngineConfig
{
    public interface IEngineConfiguration<TScene, TSetting>
        where TScene : IDesignScene<TSetting>
    {
        IEngineEnvironment<TScene, TSetting> EngineEnvironment { get; }

        IServiceProvider ServiceProvider { get; }
    }
}
