using Ao.ObjectDesign.Designing;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace System.Collections.ObjectModel
{
    public class DynamicSilentObservableCollection<T>:NotifyableObject, ISilentObservableCollection<T>,INotifyCollectionChanged, IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IList, ICollection, IReadOnlyList<T>, IReadOnlyCollection<T>
    {
        public DynamicSilentObservableCollection()
        {
            collection = new SilentObservableCollection<T>();
        }

        public DynamicSilentObservableCollection(IEnumerable<T> collection)
        {
            this.collection=new SilentObservableCollection<T>(collection);
        }

        public DynamicSilentObservableCollection(List<T> list)
        {
            collection=new SilentObservableCollection<T>(list);
        }

        protected readonly SilentObservableCollection<T> collection;

        public T this[int index] { get => ((IList<T>)collection)[index]; set => ((IList<T>)collection)[index] = value; }
        
        object IList.this[int index] { get => ((IList)collection)[index]; set => ((IList)collection)[index] = value; }

        public int Count => ((ICollection<T>)collection).Count;

        bool IList.IsReadOnly => false;

        bool IList.IsFixedSize => ((IList)collection).IsFixedSize;

        object ICollection.SyncRoot => ((ICollection)collection).SyncRoot;

        bool ICollection.IsSynchronized => ((ICollection)collection).IsSynchronized;

        bool ICollection<T>.IsReadOnly => ((ICollection<T>)collection).IsReadOnly;

        public event NotifyCollectionChangedEventHandler CollectionChanged
        {
            add
            {
                ((INotifyCollectionChanged)collection).CollectionChanged += value;
            }

            remove
            {
                ((INotifyCollectionChanged)collection).CollectionChanged -= value;
            }
        }

        public void Add(T item)
        {
            ((ICollection<T>)collection).Add(item);
        }

        int IList.Add(object value)
        {
            return ((IList)collection).Add(value);
        }

        public void Clear()
        {
            ((ICollection<T>)collection).Clear();
        }

        public bool Contains(T item)
        {
            return ((ICollection<T>)collection).Contains(item);
        }

        bool IList.Contains(object value)
        {
            return ((IList)collection).Contains(value);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            ((ICollection<T>)collection).CopyTo(array, arrayIndex);
        }

        void ICollection.CopyTo(Array array, int index)
        {
            ((ICollection)collection).CopyTo(array, index);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)collection).GetEnumerator();
        }

        public int IndexOf(T item)
        {
            return ((IList<T>)collection).IndexOf(item);
        }

        int IList.IndexOf(object value)
        {
            return ((IList)collection).IndexOf(value);
        }

        public void Insert(int index, T item)
        {
            ((IList<T>)collection).Insert(index, item);
        }

        void IList.Insert(int index, object value)
        {
            ((IList)collection).Insert(index, value);
        }

        public bool Remove(T item)
        {
            return ((ICollection<T>)collection).Remove(item);
        }

        void IList.Remove(object value)
        {
            ((IList)collection).Remove(value);
        }

        public void RemoveAt(int index)
        {
            ((IList<T>)collection).RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)collection).GetEnumerator();
        }

        public void AddRange(IEnumerable<T> items)
        {
            ((ISilentObservableCollection<T>)collection).AddRange(items);
        }

        public void AddRangeNotifyReset(IEnumerable<T> items)
        {
            ((ISilentObservableCollection<T>)collection).AddRangeNotifyReset(items);
        }

        public void RemoveRange(IEnumerable<T> items)
        {
            ((ISilentObservableCollection<T>)collection).RemoveRange(items);
        }

        public void RemoveRangeNotifyReset(IEnumerable<T> items)
        {
            ((ISilentObservableCollection<T>)collection).RemoveRangeNotifyReset(items);
        }

        public void Sort<TProperty>(Func<T, TProperty> order)
        {
            ((ISilentObservableCollection<T>)collection).Sort(order);
        }

        public void SortDescending<TProperty>(Func<T, TProperty> order)
        {
            ((ISilentObservableCollection<T>)collection).SortDescending(order);
        }

        public void Move(int oldIndex, int newIndex)
        {
            ((ISilentObservableCollection<T>)collection).Move(oldIndex, newIndex);
        }
    }
    /// <summary>
    /// SilentObservableCollection is a ObservableCollection with some extensions.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SilentObservableCollection<T> : ObservableCollection<T>, ISilentObservableCollection<T>
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
