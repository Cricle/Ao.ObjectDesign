using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Ao.ObjectDesign.Data
{
    public class ExternalReadOnlyDictionary<TKey, TValue> : IReadOnlyDictionary<TKey, TValue>
    {
        public ExternalReadOnlyDictionary()
        {
            innerMap = new ConcurrentDictionary<TKey, TValue>();
        }
        public ExternalReadOnlyDictionary(int concurrencyLevel, int capacity)
        {
            innerMap = new ConcurrentDictionary<TKey, TValue>(concurrencyLevel, capacity);
        }
        public ExternalReadOnlyDictionary(IDictionary<TKey, TValue> map)
        {
            innerMap = new ConcurrentDictionary<TKey, TValue>(map);
        }

        public ExternalReadOnlyDictionary(IEqualityComparer<TKey> comparer)
        {
            innerMap = new ConcurrentDictionary<TKey, TValue>(comparer);
        }

        protected readonly ConcurrentDictionary<TKey, TValue> innerMap;

        public TValue this[TKey key] => innerMap[key];

        public IEnumerable<TKey> Keys => innerMap.Keys;

        public IEnumerable<TValue> Values => innerMap.Values;

        public int Count => innerMap.Count;


        public bool ContainsKey(TKey key)
        {
            return innerMap.ContainsKey(key);
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return innerMap.GetEnumerator();
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return innerMap.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return innerMap.GetEnumerator();
        }

        public override string ToString()
        {
            return $"{{Count = {Count}}}";
        }

        private string GetDebuggerDisplay()
        {
            return ToString();
        }
    }
}
