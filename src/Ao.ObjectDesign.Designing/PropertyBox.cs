using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Ao.ObjectDesign.Designing
{
    internal class PropertyBox
    {
        public PropertyInfo Property;

        public PropertyGetter Getter;

        public PropertySetter Setter;

        public bool IsBuilt;

        public bool IsVirtualPropery;

        public object GetValue(object instance)
        {
            EnsureBuild();
            if (Getter == null)
            {
                throw new InvalidOperationException($"Type {Property.DeclaringType}.{Property.Name} has no get method");
            }
            return Getter(instance);
        }
        public void SetValue(object instance, object value)
        {
            EnsureBuild();
            if (Setter == null)
            {
                throw new InvalidOperationException($"Type {Property.DeclaringType}.{Property.Name} has no set method");
            }
            Setter(instance, value);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void EnsureBuild()
        {
            if (!IsBuilt)
            {
                var identity = new PropertyIdentity(Property);

                if (Property.CanWrite)
                {
                    Setter = CompiledPropertyInfo.GetSetter(identity);
                }
                if (Property.CanRead)
                {
                    Getter = CompiledPropertyInfo.GetGetter(identity);
                }
                IsBuilt = true;
            }
        }
    }
}
