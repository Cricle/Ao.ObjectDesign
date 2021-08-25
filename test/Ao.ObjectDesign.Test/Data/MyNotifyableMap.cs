using Ao.ObjectDesign.Data;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Ao.ObjectDesign.Test.Data
{
    internal class MyNotifyableMap<TKey, TValue> : NotifyableMap<TKey, TValue>
    {
        public bool Concurrent { get; set; }

        protected override IDictionary<TKey, TValue> CreateMap()
        {
            if (Concurrent)
            {
                return new ConcurrentDictionary<TKey, TValue>();
            }
            return base.CreateMap();
        }
        protected override IDictionary<TKey, TValue> CreateMap(IDictionary<TKey, TValue> map)
        {
            if (Concurrent)
            {
                return new ConcurrentDictionary<TKey, TValue>(map);
            }
            return base.CreateMap(map);
        }
        protected override IDictionary<TKey, TValue> CreateMap(IEqualityComparer<TKey> comparer)
        {
            if (Concurrent)
            {
                return new ConcurrentDictionary<TKey, TValue>(comparer);
            }
            return base.CreateMap(comparer);
        }
        protected override IDictionary<TKey, TValue> CreateMap(int concurrencyLevel, int capacity)
        {
            if (Concurrent)
            {
                return new ConcurrentDictionary<TKey, TValue>(concurrencyLevel, capacity);
            }
            return base.CreateMap(concurrencyLevel, capacity);
        }
    }
}
