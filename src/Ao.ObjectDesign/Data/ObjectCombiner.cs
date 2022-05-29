using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace Ao.ObjectDesign.Data
{
    public class ObjectCombiner
    {
        private readonly Dictionary<string, ICombineItem> properties = new Dictionary<string, ICombineItem>();

        public IReadOnlyDictionary<string, ICombineItem> PropertyProxyMap => properties;
        
        public void Add(object instance)
        {
            if (instance is ObjectCombiner combiner)
            {
                foreach (var item in combiner.properties)
                {
                    properties[item.Key] = item.Value;
                }
            }
            else if (instance is IDictionary<string,object> map)
            {
                foreach (var item in map)
                {
                    properties[item.Key] = new MapCombineItem(map, item.Key);
                }
            }
            else
            {
                var props = new ObjectProxy(instance, instance.GetType())
                        .GetPropertyProxies();
                foreach (var item in props)
                {
                    properties[item.PropertyInfo.Name] = new PropertyCombineItem(item);
                }
            }
        }


        public int Count => properties.Count;

        public bool TryGetProxy(string name,out ICombineItem combineItem)
        {
            if (properties.TryGetValue(name, out var prop))
            {
                combineItem = prop;
                return true;
            }
            combineItem = null;
            return false;
        }
        public bool TryGet(string name, out object value)
        {
            if (TryGetProxy(name, out var prop))
            {
                value = prop.GetValue();
                return true;
            }
            value = null;
            return false;
        }
        public bool ContainsKey(string name)
        {
            return properties.ContainsKey(name);
        }
    }

}
