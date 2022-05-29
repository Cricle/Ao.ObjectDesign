using System;
using System.Collections.Generic;

namespace Ao.ObjectDesign.Data
{
    public class MapCombineItem : ICombineItem
    {
        public MapCombineItem(IDictionary<string, object> map, string name)
        {
            Map = map ?? throw new ArgumentNullException(nameof(map));
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public IDictionary<string,object> Map { get; }

        public string Name { get; }

        public object GetValue()
        {
            if (Map.TryGetValue(Name,out var val))
            {
                return val;
            }
            return null;
        }

        public void SetValue(object value)
        {
            Map[Name] = value;
        }
    }

}
