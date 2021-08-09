using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;

namespace Ao.ObjectDesign.Data
{
    [DebuggerDisplay("{" + nameof(ToString) + "(),nq}")]
    public class NotifyableMap<TKey, TValue> : ExternalReadOnlyDictionary<TKey,TValue>
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

        public event EventHandler<DataChangedEventArgs<TKey,TValue>> DataChanged;
        public event EventHandler Clean;

        public TValue AddOrUpdate(TKey key, TValue value)
        {
            return AddOrUpdate(key, value, value);
        }

        public TValue AddOrUpdate(TKey key, TValue addValue, TValue updateValue)
        {
            return AddOrUpdate(key, addValue, (_, __) => updateValue);
        }

        public TValue AddOrUpdate(TKey key, TValue addValue, Func<TKey, TValue, TValue> updateFactory)
        {
            return AddOrUpdate(key, _ => addValue, updateFactory);
        }

        public TValue AddOrUpdate(TKey key,Func<TKey,TValue> addFactory, Func<TKey,TValue,TValue> updateFactory)
        {
            return innerMap.AddOrUpdate(key, x =>
            {
                var val = addFactory(x);
                if (DataChanged != null)
                {
                    DataChanged(this, new DataChangedEventArgs<TKey,TValue>(x,default, val, ChangeModes.New));
                }
                return val;
            }, (x, old) =>
             {
                 var val = updateFactory(x, old);
                 if (DataChanged != null)
                 {
                     DataChanged(this, new DataChangedEventArgs<TKey,TValue>(x,old, val, ChangeModes.Change));
                 }
                 return val;
             });
        }
        public TValue GetOrAdd(TKey key, Func<TKey, TValue> valueFactory)
        {
            return innerMap.GetOrAdd(key, x =>
             {
                 var value = valueFactory(x);
                 if (DataChanged != null)
                 {
                     DataChanged(this, new DataChangedEventArgs<TKey,TValue>(x, default, value, ChangeModes.New));
                 }
                 return value;
             });
        }
        public bool Remove(TKey key)
        {
            if (innerMap.TryRemove(key, out var value))
            {
                if (DataChanged != null)
                {
                    DataChanged(this, new DataChangedEventArgs<TKey, TValue>(key, value, default, ChangeModes.Reset));
                }
                return true;
            }
            return false;
        }

        public void Clear()
        {
            innerMap.Clear();
            Clean?.Invoke(this, EventArgs.Empty);
        }

    }
}
