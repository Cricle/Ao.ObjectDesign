using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Data
{
    public static class NotifyableMapActionExtensions
    {
        public static void AddOrUpdateMany<TKey, TValue>(this NotifyableMap<TKey, TValue> map,
            IEnumerable<KeyValuePair<TKey, TValue>> values)
        {
            if (map is null)
            {
                throw new ArgumentNullException(nameof(map));
            }

            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            foreach (var item in values)
            {
                map.AddOrUpdate(item.Key, item.Value);
            }
        }
        public static Task AddOrUpdateManyAsync<TKey,TValue>(this NotifyableMap<TKey,TValue> map,
            IEnumerable<KeyValuePair<TKey,TValue>> values)
        {
            if (map is null)
            {
                throw new ArgumentNullException(nameof(map));
            }

            if (values is null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            return Task.Factory.StartNew(state =>
            {
                var m = (NotifyableMap<TKey, TValue>)state;
                AddOrUpdateMany(m, values);
            }, map);
        }
    }
}
