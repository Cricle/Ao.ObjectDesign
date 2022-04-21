using System.Collections.Generic;
using System.Diagnostics;

namespace Ao.ObjectDesign.Data
{
    [DebuggerDisplay("{" + nameof(ToString) + "(),nq}")]
    public class ValidDataView<TKey, TValue> : ValidNotifyableMap<TKey, TValue>
    {
        public ValidDataView()
        {
        }

        public ValidDataView(IDictionary<TKey, TValue> map) : base(map)
        {
        }

        public ValidDataView(IEqualityComparer<TKey> comparer) : base(comparer)
        {
        }

        public ValidDataView(int concurrencyLevel, int capacity) : base(concurrencyLevel, capacity)
        {
        }
    }
}
