using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Ao.ObjectDesign
{
    public class ReadOnlyHashSet<T> : IReadOnlyHashSet<T>, IReadOnlyCollection<T>
    {
        public static readonly ReadOnlyHashSet<T> Empty = new ReadOnlyHashSet<T>(Enumerable.Empty<T>());

        protected readonly HashSet<T> set;

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

        public bool IsProperSubsetOf(IEnumerable<T> other)
        {
            return set.IsProperSubsetOf(other);
        }
        public bool IsProperSupersetOf(IEnumerable<T> other)
        {
            return set.IsProperSupersetOf(other);
        }
        public bool IsSubsetOf(IEnumerable<T> other)
        {
            return set.IsSubsetOf(other);
        }
        public bool IsSupersetOf(IEnumerable<T> other)
        {
            return set.IsSupersetOf(other);
        }
        public bool Overlaps(IEnumerable<T> other)
        {
            return set.Overlaps(other);
        }
        public bool SetEquals(IEnumerable<T> other)
        {
            return set.SetEquals(other);
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
