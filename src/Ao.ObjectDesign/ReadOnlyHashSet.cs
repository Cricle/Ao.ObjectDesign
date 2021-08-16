using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Ao.ObjectDesign
{
    public class ReadOnlyHashSet<T> : IReadOnlyHashSet<T>, IReadOnlyCollection<T>
    {
        public static readonly ReadOnlyHashSet<T> Empty = new ReadOnlyHashSet<T>(Enumerable.Empty<T>());

        private readonly HashSet<T> set;

        public ReadOnlyHashSet(IEnumerable<T> set)
        {
            if (set is null)
            {
                throw new ArgumentNullException(nameof(set));
            }

            this.set = new HashSet<T>(set);
        }
        public ReadOnlyHashSet(IEnumerable<T> set, IEqualityComparer<T> comparer)
        {
            if (set is null)
            {
                throw new ArgumentNullException(nameof(set));
            }

            if (comparer is null)
            {
                throw new ArgumentNullException(nameof(comparer));
            }

            this.set = new HashSet<T>(set, comparer);
        }
        public ReadOnlyHashSet(HashSet<T> set)
        {
            if (set is null)
            {
                throw new ArgumentNullException(nameof(set));
            }

            this.set = new HashSet<T>(set, set.Comparer);
        }

        public int Count => set.Count;

        public bool Contains(T value)
        {
            return set.Contains(value);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return set.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
