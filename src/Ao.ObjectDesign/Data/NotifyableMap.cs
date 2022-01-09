using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;

namespace Ao.ObjectDesign.Data
{
    [DebuggerDisplay("{" + nameof(ToString) + "(),nq}")]
    public class NotifyableMap<TKey, TValue> : ExternalReadOnlyDictionary<TKey, TValue>
    {
        public NotifyableMap()
        {
            innerMap = originMap as ConcurrentDictionary<TKey, TValue>;
        }

        public NotifyableMap(int concurrencyLevel, int capacity) : base(concurrencyLevel, capacity)
        {
            innerMap = originMap as ConcurrentDictionary<TKey, TValue>;
        }

        public NotifyableMap(IDictionary<TKey, TValue> map) : base(map)
        {
            innerMap = originMap as ConcurrentDictionary<TKey, TValue>;
        }

        public NotifyableMap(IEqualityComparer<TKey> comparer) : base(comparer)
        {
            innerMap = originMap as ConcurrentDictionary<TKey, TValue>;
        }

        protected readonly ConcurrentDictionary<TKey, TValue> innerMap;
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
            TValue newValue = default;
            if (innerMap is null)
            {
                if (originMap.TryGetValue(key, out var old))
                {
                    var val = updateFactory(key, old);
                    args = new DataChangedEventArgs<TKey, TValue>(key, old, val, ChangeModes.Change);
                    OnWritingData(args);
                    originMap[key] = val;
                    newValue = val;
                }
                else
                {
                    var val = addFactory(key);
                    args = new DataChangedEventArgs<TKey, TValue>(key, default, val, ChangeModes.New);
                    OnWritingData(args);
                    originMap.Add(key, val);
                    newValue = val;
                }
            }
            else
            {
                newValue = innerMap.AddOrUpdate(key, x =>
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
            }
            Debug.Assert(args != null);
            DataChanged?.Invoke(this, args);
            OnWritedData(args);
            return newValue;
        }

        protected virtual void OnWritingData(DataChangedEventArgs<TKey, TValue> e)
        {

        }
        protected virtual void OnWritedData(DataChangedEventArgs<TKey, TValue> e)
        {

        }

        public virtual TValue GetOrAdd(TKey key, Func<TKey, TValue> valueFactory)
        {
            DataChangedEventArgs<TKey, TValue> args = null;
            TValue value = default;
            if (innerMap is null)
            {
                if (originMap.TryGetValue(key, out var val))
                {
                    return val;
                }
                var newValue = valueFactory(key);
                args = new DataChangedEventArgs<TKey, TValue>(key, default, newValue, ChangeModes.New);
                OnWritingData(args);
                originMap.Add(key, newValue);
            }
            else
            {
                value = innerMap.GetOrAdd(key, x =>
                  {
                      var v = valueFactory(x);
                      args = new DataChangedEventArgs<TKey, TValue>(x, default, v, ChangeModes.New);
                      OnWritingData(args);
                      return value;
                  });
            }
            DataChanged?.Invoke(this, args);
            OnWritedData(args);
            return value;
        }
        public virtual bool Remove(TKey key)
        {
            TValue value;
            bool succeed;
            if (innerMap is null)
            {
                succeed = originMap.TryGetValue(key, out value);
                if (succeed)
                {
                    originMap.Remove(key);
                }
            }
            else
            {
                succeed = innerMap.TryRemove(key, out value);
            }
            if (succeed)
            {
                var e = new DataChangedEventArgs<TKey, TValue>(key, value, default, ChangeModes.Reset);
                OnWritingData(e);
                DataChanged?.Invoke(this, e);
                OnWritedData(e);
            }
            return succeed;
        }

        public virtual void Clear()
        {
            originMap.Clear();
            Clean?.Invoke(this, EventArgs.Empty);
        }

    }
}
