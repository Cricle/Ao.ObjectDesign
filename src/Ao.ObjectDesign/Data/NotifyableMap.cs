using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Data
{
    [DebuggerDisplay("{" + nameof(ToString) + "(),nq}")]
    public class NotifyableMap<TKey, TValue> : ExternalReadOnlyDictionary<TKey, TValue>
    {
        public NotifyableMap()
        {
        }

        public NotifyableMap(int concurrencyLevel, int capacity) : base(concurrencyLevel, capacity)
        {
        }

        public NotifyableMap(IDictionary<TKey, TValue> map) : base(map)
        {
        }

        public NotifyableMap(IEqualityComparer<TKey> comparer) : base(comparer)
        {
        }

        public event EventHandler<DataChangedEventArgs<TKey, TValue>> DataChanged;
        public event EventHandler Clean;

        public virtual TValue AddOrUpdate(TKey key, TValue value)
        {
            return AddOrUpdate(key, value, value);
        }

        public virtual TValue AddOrUpdate(TKey key, TValue addValue, TValue updateValue)
        {
            return AddOrUpdate(key, addValue, (_, __) => updateValue);
        }

        public virtual TValue AddOrUpdate(TKey key, TValue addValue, Func<TKey, TValue, TValue> updateFactory)
        {
            return AddOrUpdate(key, _ => addValue, updateFactory);
        }

        public virtual TValue AddOrUpdate(TKey key, Func<TKey, TValue> addFactory, Func<TKey, TValue, TValue> updateFactory)
        {
            DataChangedEventArgs<TKey, TValue> args = null;
            var res= innerMap.AddOrUpdate(key, x =>
            {
                var val = addFactory(x);
                args = new DataChangedEventArgs<TKey, TValue>(x, default, val, ChangeModes.New);
                OnWritingData(args);
                return val;
            }, (x, old) =>
             {
                 var val = updateFactory(x, old);
                 args = new DataChangedEventArgs<TKey, TValue>(x, old, val, ChangeModes.Change);
                 OnWritingData(args);
                 return val;
             });
            Debug.Assert(args != null);
            DataChanged?.Invoke(this, args);
            OnWritedData(args);
            return res;
        }

        protected virtual void OnWritingData(DataChangedEventArgs<TKey, TValue> e)
        {

        }
        protected virtual void OnWritedData(DataChangedEventArgs<TKey, TValue> e)
        {

        }

        public virtual TValue GetOrAdd(TKey key, Func<TKey, TValue> valueFactory)
        {
            return innerMap.GetOrAdd(key, x =>
             {
                 var value = valueFactory(x);
                 var e = new DataChangedEventArgs<TKey, TValue>(x, default, value, ChangeModes.New);
                 OnWritingData(e);
                 DataChanged?.Invoke(this, e);
                 OnWritedData(e);
                 return value;
             });
        }
        public virtual bool Remove(TKey key)
        {
            if (innerMap.TryRemove(key, out var value))
            {
                var e = new DataChangedEventArgs<TKey, TValue>(key, value, default, ChangeModes.Reset);
                OnWritingData(e);
                DataChanged?.Invoke(this, e);
                OnWritedData(e);
                return true;
            }
            return false;
        }

        public virtual void Clear()
        {
            innerMap.Clear();
            Clean?.Invoke(this, EventArgs.Empty);
        }

    }
}
