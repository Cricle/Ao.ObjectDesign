using System;

namespace Ao.ObjectDesign
{
    public class ObjectDesigner : IObjectDesigner
    {
        public readonly static ObjectDesigner Instance = new ObjectDesigner();

        public static IObjectProxy CreateDefaultProxy(object instance, Type type)
        {
            if (instance is null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return Instance.CreateProxy(instance, type);
        }
        public static IObjectProxy CreateDefaultProxy(object instance)
        {
            if (instance is null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            return Instance.CreateProxy(instance, instance.GetType());
        }
        public IObjectProxy CreateProxy(object instance, Type type)
        {
            if (instance is null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            if (!type.IsClass)
            {
                throw new ArgumentException($"Type {type.FullName} can't be parse, must class and not string");
            }

            return new ObjectProxy(instance, type);
        }
    }
}
