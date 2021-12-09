using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Ao.ObjectDesign
{
    public class PropertyProxy : PropertyDeclare, IPropertyProxy, IObjectProxy
    {
        public PropertyProxy(object declaringInstance, PropertyInfo propertyInfo)
            : base(propertyInfo)
        {
            DeclaringInstance = declaringInstance ?? throw new ArgumentNullException(nameof(declaringInstance));

            if (!PropertyInfo.DeclaringType.IsInstanceOfType(declaringInstance))
            {
                throw new ArgumentException($"Declare instance {declaringInstance.GetType()} is not declare the property {PropertyInfo.Name}");
            }
        }
        private ExpressionPropertyVisitor propertyVisitor;
        public virtual object DeclaringInstance { get; }

        public virtual object Instance
        {
            get => EnsureLoadVisitor().Value;
            set => EnsureLoadVisitor().Value = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private ExpressionPropertyVisitor EnsureLoadVisitor()
        {
            return propertyVisitor ??
               (propertyVisitor = new ExpressionPropertyVisitor(DeclaringInstance, PropertyInfo));
        }
        public override string ToString()
        {
            return $"{{Delclare:{DeclaringInstance}, PropertyInfo:{PropertyInfo}}}";
        }
        protected PropertyProxy CreatePropertyProxy(PropertyInfo propertyInfo)
        {
            return new PropertyProxy(Instance, propertyInfo);
        }
        public IEnumerable<PropertyProxy> GetPropertyProxies()
        {
            return AsProperties(CreatePropertyProxy);
        }

        IEnumerable<IPropertyProxy> IObjectProxy.GetPropertyProxies()
        {
            return AsProperties(CreatePropertyProxy).OfType<IPropertyProxy>();
        }
    }
}
