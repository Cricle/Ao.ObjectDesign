using System;
using System.Collections.Generic;
using System.Linq;

namespace Ao.ObjectDesign.Designing
{
    public static class ObjectLookup
    {
        private static readonly string INotifyPropertyChangeToName = typeof(INotifyPropertyChangeTo).FullName;

        public static IEnumerable<INotifyPropertyChangeTo> LookupNotifyableChangeTo(IObjectProxy proxy, bool skipSelf)
        {
            if (proxy is null)
            {
                throw new ArgumentNullException(nameof(proxy));
            }

            return Lookup(proxy,
                NotifyableChangeToCanStepInCondition,
                NotifyableChangeToValueFactory, skipSelf);
        }
        private static bool NotifyableChangeToCanStepInCondition(IPropertyProxy proxy)
        {
            if (proxy is null)
            {
                throw new ArgumentNullException(nameof(proxy));
            }

            return proxy.Type.GetInterface(INotifyPropertyChangeToName) != null;
        }
        private static INotifyPropertyChangeTo NotifyableChangeToValueFactory(IPropertyProxy proxy, IPropertyVisitor visitor)
        {
            return visitor as INotifyPropertyChangeTo;
        }
        public static IEnumerable<T> Lookup<T>(IObjectProxy proxy,
            Func<IPropertyProxy, bool> canStepInCondition,
            Func<IPropertyProxy, IPropertyVisitor, T> valueFetcher,
            bool skipSelf)
            where T : class
        {
            if (proxy is null)
            {
                throw new ArgumentNullException(nameof(proxy));
            }

            if (canStepInCondition is null)
            {
                throw new ArgumentNullException(nameof(canStepInCondition));
            }

            if (valueFetcher is null)
            {
                throw new ArgumentNullException(nameof(valueFetcher));
            }

            List<IObjectProxy> objs = new List<IObjectProxy> { proxy };

            while (objs.Count != 0)
            {
                IEnumerable<IPropertyProxy> props = objs.SelectMany(x => x.GetPropertyProxies());
                List<IObjectProxy> nexts = new List<IObjectProxy>();
                foreach (IPropertyProxy item in props)
                {
                    PropertyVisitor visitor = item.CreateVisitor();
                    T ret = valueFetcher(item, visitor);
                    if (ret != null)
                    {
                        if (!skipSelf || objs.Count != 1 || objs[0] != proxy)
                        {
                            yield return ret;
                        }
                    }
                    if (item.Type.IsClass && ret != null && canStepInCondition(item))
                    {
                        object val = visitor.Value;
                        if (val != null)
                        {
                            ObjectProxy propObjProxy = new ObjectProxy(val, val.GetType());
                            nexts.Add(propObjProxy);
                        }
                    }
                }

                objs = nexts;
            }
        }
    }
}
