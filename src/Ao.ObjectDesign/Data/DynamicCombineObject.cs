using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace Ao.ObjectDesign.Data
{
    public class DynamicCombineObject : DynamicObject,IDictionary<string, object>
    {
        public DynamicCombineObject()
            :this(new ObjectCombiner())
        {
        }

        public DynamicCombineObject(ObjectCombiner combiner)
        {
            Combiner = combiner ?? throw new ArgumentNullException(nameof(combiner));
        }

        public ObjectCombiner Combiner { get; }

        public ICollection<string> Keys => Combiner.PropertyProxyMap.Keys.ToList();

        public ICollection<object> Values => Combiner.PropertyProxyMap.Values.Select(x=>x.GetValue()).ToList();

        public int Count => Combiner.PropertyProxyMap.Count;

        public bool IsReadOnly => false;

        public object this[string key] 
        {
            get => Combiner.PropertyProxyMap[key].GetValue();
            set => Combiner.PropertyProxyMap[key].SetValue(value);
        }

        public override IEnumerable<string> GetDynamicMemberNames()
        {
            return Combiner.PropertyProxyMap.Keys;
        }
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            if (Combiner.TryGetProxy(binder.Name, out var val))
            {
                val.SetValue(value);
                return true;
            }
            return base.TrySetMember(binder, value);
        }
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            if (Combiner.TryGet(binder.Name, out var val))
            {
                result = val;
                return true;
            }
            return base.TryGetMember(binder, out result);
        }

        public void Add(string key, object value)
        {
            throw new NotSupportedException();
        }

        public bool ContainsKey(string key)
        {
            return Combiner.ContainsKey(key);
        }

        public bool Remove(string key)
        {
            throw new NotSupportedException();
        }

        public bool TryGetValue(string key, out object value)
        {
            return Combiner.TryGet(key, out value);
        }

        public void Add(KeyValuePair<string, object> item)
        {
            throw new NotSupportedException();
        }

        public void Clear()
        {
            throw new NotSupportedException();
        }

        public bool Contains(KeyValuePair<string, object> item)
        {
            throw new NotSupportedException();
        }

        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
            throw new NotSupportedException();
        }

        public bool Remove(KeyValuePair<string, object> item)
        {
            throw new NotSupportedException();
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            foreach (var item in Combiner.PropertyProxyMap)
            {
                yield return new KeyValuePair<string, object>(
                    item.Key, item.Value.GetValue());
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

}
