using System.Collections.Generic;
using System.Linq;

namespace Ao.ObjectDesign.Sources
{
    public static class AnalysisManagerExtensions
    {
        public static IProviderFactory<TContext> Get<TContext>(this AnalysisManager<TContext> manager, TContext context)
        {
            return CoreGet(manager.ProviderFactories, context).FirstOrDefault();
        }

        public static IAsyncProviderFactory<TContext> AsyncGet<TContext>(this AnalysisManager<TContext> manager, TContext context)
        {
            return CoreGet(manager.AsyncDataProviderFactories, context).FirstOrDefault();
        }

        private static IEnumerable<T> CoreGet<T,TContext>(List<T> datas, TContext context)
            where T :IProviderFactoryCondition<TContext>
        {
            for (int i = 0; i < datas.Count; i++)
            {
                var item=datas[i];
                if (item.Condition(context))
                {
                    yield return item;
                }
            }
        }
    }
}
