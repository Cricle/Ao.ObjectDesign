using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Wpf
{
    public class ReadOnlyHashSet<T> : IReadOnlyHashSet<T>, IReadOnlyCollection<T>
    {
        private readonly HashSet<T> set;

        public ReadOnlyHashSet(HashSet<T> set)
        {
            if (set is null)
            {
                throw new ArgumentNullException(nameof(set));
            }

            this.set = new HashSet<T>(set);
            this.set.TrimExcess();
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
