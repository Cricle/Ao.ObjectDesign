using Ao.ObjectDesign.ForView;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Wpf
{
    public static class ObjectLookup
    {
        public static IEnumerable<INotifyPropertyChangeTo> LookupNotifyableChangeTo(IObjectProxy proxy,bool skipSelf)
        {
            return Lookup(proxy,
                NotifyableChangeToCanStepInCondition,
                NotifyableChangeToValueFactory, skipSelf);
        }
        private static bool NotifyableChangeToCanStepInCondition(IPropertyProxy proxy)
        {
            return proxy.Type.GetInterface(typeof(INotifyPropertyChangeTo).FullName) != null;
        }
        private static INotifyPropertyChangeTo NotifyableChangeToValueFactory(IPropertyProxy proxy,IPropertyVisitor visitor)
        {
            return visitor as INotifyPropertyChangeTo; 
        }
        public static IEnumerable<T> Lookup<T>(IObjectProxy proxy,
            Func<IPropertyProxy,bool> canStepInCondition, 
            Func<IPropertyProxy,IPropertyVisitor, T> valueFetcher,
            bool skipSelf)
            where T : class
        {
            var objs = new List<IObjectProxy> { proxy };

            while (objs.Count != 0)
            {
                var props = objs.SelectMany(x => x.GetPropertyProxies());
                var nexts = new List<IObjectProxy>();
                foreach (var item in props)
                {
                    var visitor = item.CreateVisitor();
                    var ret = valueFetcher(item, visitor);
                    if (ret != null)
                    {
                        if (!skipSelf || objs.Count != 1 || objs[0] != proxy)
                        {
                            yield return ret;
                        }
                    }
                    if (item.Type.IsClass && ret != null && canStepInCondition(item))
                    {
                        var val = visitor.Value;
                        if (val != null)
                        {
                            var propObjProxy = new ObjectProxy(val, val.GetType());
                            nexts.Add(propObjProxy);
                        }
                    }
                }

                objs = nexts;
            }
        }
    }
}
