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
        private readonly Dictionary<Type, IInstanceFactory> instanceFactoryMap;

        public UIDesignMap()
        {
            designTypeMap = new Dictionary<Type, HashSet<Type>>();
            instanceFactoryMap = new Dictionary<Type, IInstanceFactory>();
        }

        public int UITypeCount => designTypeMap.Count;

        public int InstanceFactoryCount => instanceFactoryMap.Count;

        public IEnumerable<Type> UITypes => designTypeMap.Keys;

        public IReadOnlyDictionary<Type, IInstanceFactory> InstanceFactory => instanceFactoryMap;

        public event EventHandler<UIDesignMapActionInstanceFactoryEventArgs> ActionInstanceFactory;
        public event EventHandler<UIDesignMapActionDeisgnTypeEventArgs> ActionDeisgnType;
        public event EventHandler CleanInstanceFactories;
        public event EventHandler CleanDeisgnTypes;

        public IInstanceFactory GetInstanceFactory(Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            instanceFactoryMap.TryGetValue(type, out var factory);
            return factory;
        }
        public bool UnRegistInstanceFactory(Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            var ev = ActionInstanceFactory;
            if (ev is null)
            {
                return instanceFactoryMap.Remove(type);
            }
            if (instanceFactoryMap.TryGetValue(type,out var factory))
            {
                instanceFactoryMap.Remove(type);
                ev(this, new UIDesignMapActionInstanceFactoryEventArgs(type, factory,null, UIDesignMapActionTypes.Removed));
                return true;
            }
            return false;   
        }
        public void RegistInstanceFactory(IInstanceFactory factory)
        {
            if (factory is null)
            {
                throw new ArgumentNullException(nameof(factory));
            }

            var ev = ActionInstanceFactory;
            if (ev is null)
            {
                instanceFactoryMap[factory.TargetType] = factory;
                return;
            }
            UIDesignMapActionTypes actionType= UIDesignMapActionTypes.New;
            if (instanceFactoryMap.TryGetValue(factory.TargetType, out var old))
            {
                instanceFactoryMap[factory.TargetType] = factory;
                actionType = UIDesignMapActionTypes.Replaced;
            }
            else
            {
                instanceFactoryMap.Add(factory.TargetType, factory);
            }
            ev(this, new UIDesignMapActionInstanceFactoryEventArgs(factory.TargetType, old, factory, actionType));
        }

        public void RegistDesignType(Type uiType, Type designType)
        {
            if (!designTypeMap.TryGetValue(uiType, out var map))
            {
                map = new HashSet<Type>();
                designTypeMap.Add(uiType, map);
            }

            var changeType = map.Contains(designType) ? UIDesignMapActionTypes.Replaced : UIDesignMapActionTypes.New;
            if (changeType == UIDesignMapActionTypes.New)
            {
                map.Add(designType);
            }
            var ev = ActionDeisgnType;
            if (ev != null)
            {
                ev(this, new UIDesignMapActionDeisgnTypeEventArgs(uiType, designType, changeType));
            }
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
        public void ClearDesignTypeMaps()
        {
            designTypeMap.Clear();
            CleanDeisgnTypes?.Invoke(this, EventArgs.Empty);
        }
        public void ClearDesignInstanceFactories()
        {
            instanceFactoryMap.Clear();
            CleanInstanceFactories?.Invoke(this, EventArgs.Empty);
        }
        public bool UnRegistDesignType(Type uiType, Type designType)
        {
            if (designTypeMap.TryGetValue(uiType, out var map))
            {
                var ok = map.Remove(designType);
                if (map.Count == 0)
                {
                    designTypeMap.Remove(uiType);
                }
                var ev = ActionDeisgnType;
                if (ok && ev != null)
                {
                    ev(this, new UIDesignMapActionDeisgnTypeEventArgs(uiType, designType, UIDesignMapActionTypes.Removed));
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
