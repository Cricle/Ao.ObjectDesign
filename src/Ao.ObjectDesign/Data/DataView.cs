using Ao.ObjectDesign;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace Ao.ObjectDesign.Data
{
    [DebuggerDisplay("{" + nameof(ToString) + "(),nq}")]
    public class DataView<TKey> : NotifyableMap<TKey, IVarValue>
    {
        public DataView()
        {
        }

        public DataView(IDictionary<TKey, IVarValue> map) : base(map)
        {
        }
        
        public DataView(IEqualityComparer<TKey> comparer) : base(comparer)
        {
        }

        public DataView(int concurrencyLevel, int capacity) : base(concurrencyLevel, capacity)
        {
        }
    }
}
