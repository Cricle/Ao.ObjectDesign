using System.Collections.Generic;

namespace Ao.ObjectDesign.Wpf
{
    public interface IReadOnlyHashSet<T> : IReadOnlyCollection<T>
    {
        bool Contains(T value);
    }
}
