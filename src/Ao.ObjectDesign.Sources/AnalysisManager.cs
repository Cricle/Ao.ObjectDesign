using Ao.ObjectDesign.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Ao.ObjectDesign.Sources
{
    public class AnalysisManager
    {
        public List<IProviderFactory> ProviderFactories { get; }

        public List<IAsyncProviderFactory> AsyncDataProviderFactories { get; }

    }
}
