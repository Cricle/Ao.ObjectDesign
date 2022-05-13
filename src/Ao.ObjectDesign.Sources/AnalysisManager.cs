using Ao.ObjectDesign.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Ao.ObjectDesign.Sources
{
    public class AnalysisManager<TContext>
    {
        public AnalysisManager()
            :this(new List<IProviderFactory<TContext>>(),new List<IAsyncProviderFactory<TContext>>())
        {

        }
        public AnalysisManager(List<IProviderFactory<TContext>> providerFactories, List<IAsyncProviderFactory<TContext>> asyncDataProviderFactories)
        {
            ProviderFactories = providerFactories ?? throw new ArgumentNullException(nameof(providerFactories));
            AsyncDataProviderFactories = asyncDataProviderFactories ?? throw new ArgumentNullException(nameof(asyncDataProviderFactories));
        }

        public List<IProviderFactory<TContext>> ProviderFactories { get; }

        public List<IAsyncProviderFactory<TContext>> AsyncDataProviderFactories { get; }

    }
}
