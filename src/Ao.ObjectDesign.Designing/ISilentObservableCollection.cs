using System.Collections.Generic;

namespace System.Collections.ObjectModel
{
    public interface ISilentObservableCollection<T> : IList<T>
    {
        void AddRange(IEnumerable<T> items);
        void AddRangeNotifyReset(IEnumerable<T> items);
        void RemoveRange(IEnumerable<T> items);
        void RemoveRangeNotifyReset(IEnumerable<T> items);
        void Sort<TProperty>(Func<T, TProperty> order);
        void SortDescending<TProperty>(Func<T, TProperty> order);

        void Move(int oldIndex, int newIndex);
    }
}