using System.Collections.Generic;

namespace Ao.ObjectDesign.Designing
{
    public interface IReadOnlyCommandWays<T> : IEnumerable<T>
    {
        int Count { get; }
        T First { get; }
        T Last { get; }
        int MaxSize { get; }

        bool Contains(T item);
    }
}