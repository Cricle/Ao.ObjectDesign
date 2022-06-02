using System;

namespace Ao.ObjectDesign
{
    public static class InnerObjectDesignExtensions
    {
        public static IObjectProxy ToProxy(this IObjectDeclaring declaring, object instance)
        {
            if (declaring is null)
            {
                throw new ArgumentNullException(nameof(declaring));
            }

            if (instance is null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            return new ObjectProxy(instance, declaring.Type);
        }

        public static object GetValue(this IPropertyDeclare declare,object instance)
        {
            var getter = CompiledPropertyInfo.GetGetter(new PropertyIdentity(declare.PropertyInfo));
            return getter(instance);
        }
        public static void SetValue(this IPropertyDeclare declare, object instance,object value)
        {
            var setter = CompiledPropertyInfo.GetSetter(new PropertyIdentity(declare.PropertyInfo));
            setter(instance,value);
        }

        public static IPropertyProxy ToProxy(this IPropertyDeclare declaring, object declareInstance)
        {
            if (declaring is null)
            {
                throw new ArgumentNullException(nameof(declaring));
            }

            if (declareInstance is null)
            {
                throw new ArgumentNullException(nameof(declareInstance));
            }

            return new PropertyProxy(declareInstance, declaring.PropertyInfo);
        }
    }
}
