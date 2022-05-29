using Ao.ObjectDesign.Data;
using System.Collections.Generic;

namespace Ao.ObjectDesign.Sources
{
    public class ProviderFactoryGroup<TContext> : List<IProviderFactory<TContext>>, IProviderFactory<TContext>
    {
        public ProviderFactoryGroup()
        {
        }

        public ProviderFactoryGroup(IEnumerable<IProviderFactory<TContext>> collection) : base(collection)
        {
        }

        public ProviderFactoryGroup(int capacity) : base(capacity)
        {
        }

        public bool Condition(TContext context)
        {
            for (int i = 0; i < this.Count; i++)
            {
                var item=this[i];
                if (item.Condition(context))
                {
                    return true;
                }
            }
            return false;
        }

        public IDataProvider GetDataProvider(TContext context)
        {
            for (int i = 0; i < this.Count; i++)
            {
                var item = this[i];
                if (item.Condition(context))
                {
                    return item.GetDataProvider(context);
                }
            }
            return null;
        }
    }
}
