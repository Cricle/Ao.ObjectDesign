using System.Collections.Generic;
using System.Linq;

namespace Ao.ObjectDesign.Sources
{
    public static class AnalysisManagerExtensions
    {
        public static IProviderFactory Get(this AnalysisManager manager, ProviderFactorySelectContext context)
        {
            return CoreGet(manager.ProviderFactories, context).FirstOrDefault();
        }

        public static IAsyncProviderFactory AsyncGet(this AnalysisManager manager, ProviderFactorySelectContext context)
        {
            return CoreGet(manager.AsyncDataProviderFactories, context).FirstOrDefault();
        }

        private static IEnumerable<T> CoreGet<T>(List<T> datas, ProviderFactorySelectContext context)
            where T :IProviderFactoryCondition
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
