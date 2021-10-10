using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Ao.ObjectDesign
{
    public class ObjectProxy : ObjectDeclaring, IObjectProxy, IEquatable<ObjectProxy>
    {
        protected ObjectProxy()
        {

        }
        public ObjectProxy(object instance, Type type) : base(type)
        {
            Instance = instance ?? throw new ArgumentNullException(nameof(instance));

            if (!type.IsInstanceOfType(instance))
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
        public override int GetHashCode()
        {
            unchecked
            {
                var h = 17;
                if (Type != null)
                {
                    h = h * 31 + Type.GetHashCode();
                }
                if (Instance != null)
                {
                    h = h * 31 + Instance.GetHashCode();
                }
                return h;
            }
        }
        public override bool Equals(object obj)
        {
            if (obj is ObjectProxy)
            {
                return Equals((ObjectProxy)obj);
            }
            return false;
        }

        public bool Equals(ObjectProxy other)
        {
            if (other is null)
            {
                return false;
            }
            return other.Type == Type &&
                other.Instance == Instance;
        }
        public override string ToString()
        {
            return $"{{{Type}, {Instance}}}";
        }
    }
}
