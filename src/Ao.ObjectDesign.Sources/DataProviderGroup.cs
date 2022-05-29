using Ao.ObjectDesign.Data;
using System.Collections.Generic;

namespace Ao.ObjectDesign.Sources
{
    public class DataProviderGroup : List<IDataProvider>, IDataProvider
    {
        public DataProviderGroup()
        {
        }

        public DataProviderGroup(IEnumerable<IDataProvider> collection) : base(collection)
        {
        }

        public DataProviderGroup(int capacity) : base(capacity)
        {
        }

        public object GetData()
        {
            var dyn = new DynamicCombineObject();
            foreach (var item in this)
            {
                dyn.Combiner.Add(item);
            }
            return dyn;
        }
    }
}
