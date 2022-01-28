using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Ao.ObjectDesign.Designing
{
    internal class PropertyBox: IPropertyBox
    {
        internal PropertyInfo property;

        internal PropertyGetter getter;

        internal PropertySetter setter;

        internal bool isBuilt;

        internal bool isVirtualPropery;

        internal string name;

        public string Name => name;

        public PropertyInfo Property => property;

        public PropertyGetter Getter => getter;

        public PropertySetter Setter => setter;

        public bool IsBuilt => isBuilt;

        public bool IsVirtualPropery => isVirtualPropery;

        public object GetValue(object instance)
        {
            EnsureBuild();
            if (getter == null)
            {
                throw new InvalidOperationException($"Type {property.DeclaringType}.{property.Name} has no get method");
            }
            return getter(instance);
        }
        public void SetValue(object instance, object value)
        {
            EnsureBuild();
            if (setter == null)
            {
                throw new InvalidOperationException($"Type {property.DeclaringType}.{property.Name} has no set method");
            }
            setter(instance, value);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void EnsureBuild()
        {
            if (!isBuilt)
            {
                var identity = new PropertyIdentity(property);

                if (property.CanWrite && property.SetMethod.GetParameters().Length == 1)
                {
                    setter = CompiledPropertyInfo.GetSetter(identity);
                }
                if (property.CanRead && property.GetMethod.GetParameters().Length == 0)
                {
                    getter = CompiledPropertyInfo.GetGetter(identity);
                }
                isBuilt = true;
            }
        }
    }
}
