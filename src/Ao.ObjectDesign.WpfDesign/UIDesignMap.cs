using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.WpfDesign
{
    public class UIDesignMap : IEnumerable<KeyValuePair<Type, IReadOnlyHashSet<Type>>>
    {
        private readonly Dictionary<Type, HashSet<Type>> designTypeMap;
        private readonly Dictionary<Type, IInstanceFactory> instanceFactory;

        public UIDesignMap()
        {
            designTypeMap = new Dictionary<Type, HashSet<Type>>();
            instanceFactory = new Dictionary<Type, IInstanceFactory>();
        }

        public int UITypeCount => designTypeMap.Count;

        public int InstanceFactoryCount => instanceFactory.Count;

        public IEnumerable<Type> UITypes => designTypeMap.Keys;

        public IReadOnlyDictionary<Type, IInstanceFactory> InstanceFactory => instanceFactory;

        public IInstanceFactory GetInstanceFactory(Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            instanceFactory.TryGetValue(type, out var factory);
            return factory;
        }
        public void UnRegistInstanceFactory(Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            instanceFactory.Remove(type);
        }
        public void RegistInstanceFactory(Type type, IInstanceFactory factory)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            instanceFactory[type] = factory;
        }

        public bool RegistDesignType(Type uiType, Type designType)
        {
            if (!designTypeMap.TryGetValue(uiType, out var map))
            {
                map = new HashSet<Type>();
                designTypeMap.Add(uiType, map);
            }

            return map.Add(designType);
        }
        public IReadOnlyHashSet<Type> GetDesignTypes(Type uiType)
        {
            if (designTypeMap.TryGetValue(uiType, out var set))
            {
                return new ReadOnlyHashSet<Type>(set);
            }
            return null;
        }
        public Type GetUIType(Type designType)
        {
            foreach (var item in designTypeMap)
            {
                if (item.Value.Contains(designType))
                {
                    return item.Key;
                }
            }
            return null;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool HasUIType(Type uiType)
        {
            return designTypeMap.ContainsKey(uiType);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool HasDesignType(Type designType)
        {
            return GetUIType(designType) != null;
        }
        public void Clear()
        {
            designTypeMap.Clear();
        }
        public bool UnRegist(Type uiType, Type designType)
        {
            if (designTypeMap.TryGetValue(uiType, out var map))
            {
                var ok = map.Remove(designType);
                if (map.Count == 0)
                {
                    designTypeMap.Remove(uiType);
                }
                return ok;
            }
            return false;
        }


        public IEnumerator<KeyValuePair<Type, IReadOnlyHashSet<Type>>> GetEnumerator()
        {
            Debug.Assert(designTypeMap != null);
            return designTypeMap.Select(x => new KeyValuePair<Type, IReadOnlyHashSet<Type>>(x.Key, new ReadOnlyHashSet<Type>(x.Value)))
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)designTypeMap).GetEnumerator();
        }
    }
}
