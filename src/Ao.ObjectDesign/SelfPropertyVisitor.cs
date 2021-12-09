using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Ao.ObjectDesign
{
    public abstract class SelfPropertyVisitor : PropertyVisitor
    {
        public SelfPropertyVisitor(object declaringInstance, PropertyInfo propertyInfo)
            : base(declaringInstance, propertyInfo)
        {
            Identity = new PropertyIdentity(PropertyInfo.DeclaringType, PropertyInfo.Name);
        }

        private PropertyGetter getter;
        private PropertySetter setter;

        public PropertyIdentity Identity { get; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override object GetValue()
        {
            if (CanGet)
            {
                if (getter == null)
                {
                    getter = GetPropertyGetter(Identity);
                }
                return getter(DeclaringInstance);
            }
            throw new InvalidOperationException($"Type {DeclaringInstance.GetType()}.{PropertyInfo} has not get method");
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override void SetValue(object value)
        {
            if (CanSet)
            {
                if (setter == null)
                {
                    setter = GetPropertySetter(Identity);
                }
                setter(DeclaringInstance, ConvertValue(value));
                RaiseValueChanged();
                return;
            }
            throw new InvalidOperationException($"Type {DeclaringInstance.GetType()}.{PropertyInfo} has not get method");
        }

        protected abstract PropertyGetter GetPropertyGetter(in PropertyIdentity identity);
        protected abstract PropertySetter GetPropertySetter(in PropertyIdentity identity);
    }
}