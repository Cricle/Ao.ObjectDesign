using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Ao.ObjectDesign
{
    public class ObjectProxy : ObjectDeclaring, IObjectProxy
    {
        protected ObjectProxy()
        {

        }
        public ObjectProxy(object instance, Type type) : base(type)
        {
            Instance = instance ?? throw new ArgumentNullException(nameof(instance));

            if (!type.IsAssignableFrom(instance.GetType()))
            {
                throw new ArgumentException($"Instance {instance} is not assignable from {type}");
            }
        }

        public virtual object Instance { get; }

        public IEnumerable<PropertyProxy> GetPropertyProxies()
        {
            return AsProperties(CreatePropertyProxy);
        }
        protected virtual PropertyProxy CreatePropertyProxy(PropertyInfo info)
        {
            return new PropertyProxy(Instance, info);
        }

        IEnumerable<IPropertyProxy> IObjectProxy.GetPropertyProxies()
        {
            return AsProperties(CreatePropertyProxy).OfType<IPropertyProxy>();
        }
    }
}
