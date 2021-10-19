using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Ao.ObjectDesign
{
    public sealed class FrozenDictionary<TKey, TValue> : IReadOnlyDictionary<TKey, TValue>
    {
        private static readonly Entry[] EmptyEntry = new Entry[0];

        private static readonly EqualityComparer<TKey> defaultComparer = EqualityComparer<TKey>.Default;
        #region Constants
        private static readonly Func<TValue, TValue> PassThrough = x => x;
        #endregion


        #region Fields
        private readonly IEqualityComparer<TKey> equalityComparer;
        private Entry[] buckets;
        private int size;

        private readonly float loadFactor;

        public float LoadFactor => loadFactor;
        public IEqualityComparer<TKey> EqualityComparer => equalityComparer;
        #endregion


        #region Constructors
        /// <summary>
        /// Creates instance.
        /// </summary>
        /// <param name="bucketSize"></param>
        /// <param name="loadFactor"></param>
        private FrozenDictionary(int bucketSize, float loadFactor, IEqualityComparer<TKey> comparer)
        {
            buckets = (bucketSize == 0) ? EmptyEntry : new Entry[bucketSize];
            this.loadFactor = loadFactor;
            equalityComparer = comparer ?? defaultComparer;
        }
        #endregion


        #region Create
        /// <summary>
        /// Creates a <see cref="FrozenDictionary{TKey, TValue}"/> from an <see cref="IEnumerable{T}"/> according to a specified key selector function.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public static FrozenDictionary<TKey, TValue> Create(IEnumerable<TValue> source, 
            Func<TValue, TKey> keySelector,
            EqualityComparer<TKey> comparer)
            => Create(source, keySelector, PassThrough,comparer);
        public static FrozenDictionary<TKey, TValue> Create(Dictionary<TKey, TValue> dic)
        {
            return Create(dic, dic.Comparer);
        }
        public static FrozenDictionary<TKey, TValue> Create(IDictionary<TKey, TValue> dic,
            IEqualityComparer<TKey> comparer)
        {
            var result = CreateFrozenDictionary(dic, comparer);
            foreach (var x in dic)
            {
                if (!result.TryAddInternal(x.Key, x.Value, out _))
                {
                    throw new ArgumentException($"Key was already exists. Key:{x.Key}");
                }
            }
            return result;
        }


        /// <summary>
        ///  Creates a <see cref="FrozenDictionary{TKey, TValue}"/> from an <see cref="IEnumerable{T}"/> according to specified key selector and value selector functions.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <param name="valueSelector"></param>
        /// <returns></returns>
        public static FrozenDictionary<TKey, TValue> Create<TSource>(IEnumerable<TSource> source, 
            Func<TSource, TKey> keySelector, 
            Func<TSource, TValue> valueSelector,
            IEqualityComparer<TKey> comparer)
        {
            var result = CreateFrozenDictionary(source, comparer);
            foreach (var x in source)
            {
                var key = keySelector(x);
                var value = valueSelector(x);
                if (!result.TryAddInternal(key, value, out _))
                {
                    throw new ArgumentException($"Key was already exists. Key:{key}");
                }
            }

            return result;
        }
        private static FrozenDictionary<TKey, TValue> CreateFrozenDictionary<TSource>(IEnumerable<TSource> source, IEqualityComparer<TKey> comparer)
        {
            const int initialSize = 4;
            const float loadFactor = 0.75f;
            var size = CountIfMaterialized(source) ?? initialSize;
            var bucketSize = CalculateCapacity(size, loadFactor);
            return new FrozenDictionary<TKey, TValue>(bucketSize, loadFactor, comparer);

        }
        private static int? CountIfMaterialized<T>(IEnumerable<T> source)
        {
            if (source is ICollection<T> a)
            {
                return a.Count;
            }

            if (source is IReadOnlyCollection<T> b)
            {
                return b.Count;
            }

            return null;
        }

        #endregion


        #region Add
        /// <summary>
        /// Add element.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="resultingValue"></param>
        /// <returns></returns>
        private bool TryAddInternal(TKey key, TValue value, out TValue resultingValue)
        {
            var nextCapacity = CalculateCapacity(size + 1, loadFactor);
            if (buckets.Length < nextCapacity)
            {
                //--- rehash
                var len = buckets.Length;
                var nextBucket = new Entry[nextCapacity];
                for (int i = 0; i < len; i++)
                {
                    var e = buckets[i];
                    while (e != null)
                    {
                        var newEntry = new Entry(e.Key, e.Value, e.Hash);
                        AddToBuckets(nextBucket, key, newEntry, default, out _);
                        e = e.Next;
                    }
                }
                buckets = nextBucket;
            }
            var success = AddToBuckets(buckets, key, null, value, out resultingValue);
            if (success)
            {
                size++;
            }

            return success;

        }
        #region Local Functions
        //--- please pass 'key + newEntry' or 'key + value'.
        static bool AddToBuckets(Entry[] buckets, TKey newKey, Entry newEntry, TValue value, out TValue resultingValue)
        {
            var hash = newEntry?.Hash ?? defaultComparer.GetHashCode(newKey);
            var index = hash & (buckets.Length - 1);
            var lastEntry = buckets[index];
            if (lastEntry is null)
            {
                if (newEntry is null)
                {
                    resultingValue = value;
                    Debug.Assert(buckets[index] == null);
                    buckets[index] = new Entry(newKey, resultingValue, hash);
                }
                else
                {
                    resultingValue = newEntry.Value;
                    buckets[index] = newEntry;
                }
            }
            else
            {
                while (true)
                {
                    if (defaultComparer.Equals(lastEntry.Key, newKey))
                    {
                        resultingValue = lastEntry.Value;
                        return false;
                    }

                    if (lastEntry.Next is null)
                    {
                        if (newEntry is null)
                        {
                            resultingValue = value;
                            lastEntry.Next = new Entry(newKey, resultingValue, hash);
                        }
                        else
                        {
                            resultingValue = newEntry.Value;
                            lastEntry.Next = newEntry;
                        }
                        break;
                    }

                    lastEntry = lastEntry.Next;
                }
            }
            return true;
        }
        #endregion


        /// <summary>
        /// Calculates bucket capacity.
        /// </summary>
        /// <param name="collectionSize"></param>
        /// <param name="loadFactor"></param>
        /// <returns></returns>
        private static int CalculateCapacity(int collectionSize, float loadFactor)
        {
            var initialCapacity = (int)(collectionSize / loadFactor);
            var capacity = 8;
            while (capacity < initialCapacity)
            {
                capacity <<= 1;
            }
            if (capacity < 8)
            {
                return 8;
            }

            return capacity;
        }
        #endregion


        #region Get
        /// <summary>
        /// Gets the element that has the specified key in the read-only dictionary or the default value of the element type.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public TValue GetValueOrDefault(TKey key, TValue defaultValue = default)
            => TryGetValue(key, out var value)
            ? value
            : defaultValue;
        #endregion


        #region IReadOnlyDictionary<TKey, TValue> implementations
        /// <summary>
        /// Gets the element that has the specified key in the read-only dictionary.
        /// </summary>
        /// <param name="key">The key to locate.</param>
        /// <returns>The element that has the specified key in the read-only dictionary.</returns>
        public TValue this[TKey key]
            => TryGetValue(key, out var value)
            ? value
            : throw new KeyNotFoundException();


        /// <summary>
        /// Gets an enumerable collection that contains the keys in the read-only dictionary.
        /// </summary>
        public IEnumerable<TKey> Keys
            => this.Select(x => x.Key);


        /// <summary>
        /// Gets an enumerable collection that contains the values in the read-only dictionary.
        /// </summary>
        public IEnumerable<TValue> Values
            => this.Select(x => x.Value);


        /// <summary>
        /// Gets the number of elements in the collection.
        /// </summary>
        public int Count
            => size;


        /// <summary>
        /// Determines whether the read-only dictionary contains an element that has the specified key.
        /// </summary>
        /// <param name="key">The key to locate.</param>
        /// <returns>
        /// true if the read-only dictionary contains an element that has the specified key; otherwise, false.
        /// </returns>
        public bool ContainsKey(TKey key)
            => TryGetValue(key, out _);


        /// <summary>
        /// Gets the value that is associated with the specified key.
        /// </summary>
        /// <param name="key">The key to locate.</param>
        /// <param name="value">
        /// When this method returns, the value associated with the specified key, if the key is found;
        /// otherwise, the default value for the type of the value parameter.
        /// This parameter is passed uninitialized.
        /// </param>
        /// <returns>true if the object that implements the <see cref="IReadOnlyDictionary{TKey, TValue}"/> interface contains an element that has the specified key; otherwise, false.</returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public bool TryGetValue(TKey key, out TValue value)
        {
            var hash = defaultComparer.GetHashCode(key);
            var index = hash & (buckets.Length - 1);
            var next = buckets[index];
            while (next != null)
            {
                if (defaultComparer.Equals(next.Key, key))
                {
                    value = next.Value;
                    return true;
                }
                next = next.Next;
            }
            value = default;
            return false;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return new Enumerator(buckets);
        }


        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>An <see cref="IEnumerator"/> object that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();
        #endregion


        #region Inner Classes
        private class Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>
        {            
            public KeyValuePair<TKey, TValue> Current
            {
                get
                {
                    if (current is null)
                    {
                        return default;
                    }
                    return new KeyValuePair<TKey, TValue>(current.Key, current.Value);
                }
            }

            object IEnumerator.Current => Current;
            private int index = 0;
            private Entry current;
            private readonly Entry[] buckets;

            public Enumerator(Entry[] buckets)
            {
                this.buckets = buckets;
                Reset();
            }

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                if (buckets.Length <= index)
                {
                    return false;
                }
                if (current?.Next is null)
                {
                    current = null;
                    while (buckets.Length > index + 1 && current == null)
                    {
                        current = buckets[++index];
                    }
                    return current != null;
                }
                current = current.Next;
                return true;
            }

            public void Reset()
            {
                index = -1;
            }
        }
        /// <summary>
        /// Represents <see cref="FrozenDictionary{TKey, TValue}"/> entry.
        /// </summary>
        private class Entry
        {
            public readonly TKey Key;
            public readonly TValue Value;
            public readonly int Hash;
            public Entry Next;

            public Entry(TKey key, TValue value, int hash)
            {
                Key = key;
                Value = value;
                Hash = hash;
            }
        }
        #endregion
    }
}
