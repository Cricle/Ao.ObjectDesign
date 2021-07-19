using System.Collections.Generic;

namespace Ao.ObjectDesign
{
    public interface IReadOnlyHashSet<T> : IReadOnlyCollection<T>
    {
        bool Contains(T value);
    }
}
