using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace System.Collections.ObjectModel
{
    /// <summary>
    /// SilentObservableCollection is a ObservableCollection with some extensions.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SilentObservableCollection<T> : ObservableCollection<T>
    {
        public SilentObservableCollection()
        {
        }

        public SilentObservableCollection(IEnumerable<T> collection) : base(collection)
        {
        }

        public SilentObservableCollection(List<T> list) : base(list)
        {
        }

        internal static class EventArgsCache
        {
            internal static readonly PropertyChangedEventArgs CountPropertyChanged = new PropertyChangedEventArgs("Count");
            internal static readonly PropertyChangedEventArgs IndexerPropertyChanged = new PropertyChangedEventArgs("Item[]");
            internal static readonly NotifyCollectionChangedEventArgs ResetCollectionChanged = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);
        }
        public void AddRangeNotifyReset(IEnumerable<T> items)
        {
            if (!items.Any())
            {
                return;
            }
            CoreAddRange(items);
            OnCollectionChanged(EventArgsCache.ResetCollectionChanged);
        }
        public void RemoveRange(IEnumerable<T> items)
        {
            if (!items.Any())
            {
                return;
            }

            int startIndex = Count;
            CoreRemoveRange(items);
            var args = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, new List<T>(items), startIndex);
            OnCollectionChanged(args);
        }
        public void RemoveRangeNotifyReset(IEnumerable<T> items)
        {
            if (!items.Any())
            {
                return;
            }

            CoreRemoveRange(items);
            OnCollectionChanged(EventArgsCache.ResetCollectionChanged);
        }
        private void CoreRemoveRange(IEnumerable<T> items)
        {

            CheckReentrancy();

            foreach (T item in items)
            {
                Items.Remove(item);
            }

            OnPropertyChanged(EventArgsCache.CountPropertyChanged);
            OnPropertyChanged(EventArgsCache.IndexerPropertyChanged);
        }
        public void AddRange(IEnumerable<T> items)
        {
            if (!items.Any())
            {
                return;
            }

            int startIndex = Count;
            CoreAddRange(items);
            var args = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, new List<T>(items), startIndex);
            OnCollectionChanged(args);
        }
        private void CoreAddRange(IEnumerable<T> items)
        {
            CheckReentrancy();

            ((List<T>)Items).AddRange(items);
            OnPropertyChanged(EventArgsCache.CountPropertyChanged);
            OnPropertyChanged(EventArgsCache.IndexerPropertyChanged);
        }
        private void Reset(Func<IList<T>> v)
        {
            IList<T> ds = v();
            for (int i = 0; i < ds.Count; i++)
            {
                this[i] = ds[i];
            }
        }
        public void Sort<TProperty>(Func<T, TProperty> order)
        {
            Reset(() => this.OrderBy(order).ToList());
        }
        public void SortDescending<TProperty>(Func<T, TProperty> order)
        {
            Reset(() => this.OrderByDescending(order).ToList());
        }
    }
}
