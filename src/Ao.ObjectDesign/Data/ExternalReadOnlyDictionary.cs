using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Ao.ObjectDesign.Data
{
    public class ExternalReadOnlyDictionary<TKey, TValue> : IReadOnlyDictionary<TKey, TValue>
    {
        public ExternalReadOnlyDictionary()
        {
            originMap = CreateMap();
            innerMap =originMap as ConcurrentDictionary<TKey, TValue>;
        }
        public ExternalReadOnlyDictionary(int concurrencyLevel, int capacity)
        {
            originMap = CreateMap(concurrencyLevel,capacity);
            innerMap = originMap as ConcurrentDictionary<TKey, TValue>;
        }
        public ExternalReadOnlyDictionary(IDictionary<TKey, TValue> map)
        {
            originMap = CreateMap(map);
            innerMap = originMap as ConcurrentDictionary<TKey, TValue>;
        }

        public ExternalReadOnlyDictionary(IEqualityComparer<TKey> comparer)
        {
            originMap = CreateMap(comparer);
            innerMap = originMap as ConcurrentDictionary<TKey, TValue>;
        }
        protected readonly IDictionary<TKey, TValue> originMap;
        protected readonly ConcurrentDictionary<TKey, TValue> innerMap;

        public TValue this[TKey key] => originMap[key];

        public IEnumerable<TKey> Keys => originMap.Keys;

        public IEnumerable<TValue> Values => originMap.Values;

        public int Count => originMap.Count;

        protected virtual IDictionary<TKey,TValue> CreateMap()
        {
            return new Dictionary<TKey, TValue>();
        }
        protected virtual IDictionary<TKey, TValue> CreateMap(int concurrencyLevel, int capacity)
        {
            return new Dictionary<TKey, TValue>(capacity);
        }
        protected virtual IDictionary<TKey, TValue> CreateMap(IDictionary<TKey, TValue> map)
        {
            return new Dictionary<TKey, TValue>(map);
        }
        protected virtual IDictionary<TKey, TValue> CreateMap(IEqualityComparer<TKey> comparer)
        {
            return new Dictionary<TKey, TValue>(comparer);
        }

        public bool ContainsKey(TKey key)
        {
            return originMap.ContainsKey(key);
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return originMap.GetEnumerator();
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return originMap.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return originMap.GetEnumerator();
        }

        public override string ToString()
        {
            return $"{{Count = {Count}}}";
        }
    }
}
