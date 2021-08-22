using System;

namespace Ao.ObjectDesign.Designing
{
    public static class UIDesignMapCreateExtensions
    {
        public static object CreateByFactoryOrReflection(this UIDesignMap map, Type type)
        {
            if (map is null)
            {
                throw new ArgumentNullException(nameof(map));
            }

            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            var factory = map.GetInstanceFactory(type);
            if (factory is null)
            {
                return Activator.CreateInstance(type);
            }
            return factory.Create();
        }
        public static object CreateByFactoryOrEmit(this UIDesignMap map, Type type)
        {
            if (map is null)
            {
                throw new ArgumentNullException(nameof(map));
            }

            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            var factory = map.GetInstanceFactory(type);
            if (factory is null)
            {
                return ReflectionHelper.Create(type);
            }
            return factory.Create();
        }
    }
}
