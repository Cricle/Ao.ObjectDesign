using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Ao.ObjectDesign
{
    public static partial class ReflectionHelper
    {
        private static readonly Type StringType = typeof(string);

        public static object Create(Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            TypeCreator creator = CompiledPropertyInfo.GetCreator(type);
            Debug.Assert(creator != null);
            return creator();
        }
        public static object GetValue(object instance, PropertyInfo propertyInfo)
        {
            if (instance is null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            if (propertyInfo is null)
            {
                throw new ArgumentNullException(nameof(propertyInfo));
            }

            PropertyIdentity identity = new PropertyIdentity(propertyInfo.DeclaringType, propertyInfo.Name);
            return GetValue(instance, identity);
        }
        public static object GetValue(object instance, PropertyIdentity identity)
        {
            if (instance is null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            PropertyGetter getter = CompiledPropertyInfo.GetGetter(identity);
            Debug.Assert(getter != null);
            return getter(instance);
        }
        public static void SetValue(object instance, object value, PropertyInfo propertyInfo)
        {
            PropertyIdentity identity = new PropertyIdentity(propertyInfo.DeclaringType, propertyInfo.Name);
            SetValue(instance, value, identity);
        }
        public static void SetValue(object instance, object value, PropertyIdentity identity)
        {
            PropertySetter setter = CompiledPropertyInfo.GetSetter(identity);
            Debug.Assert(setter != null);
            setter(instance, value);
        }
        public static T Clone<T>(T source)
            where T: class
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return Clone(source, ReadOnlyHashSet<Type>.Empty);
        }
        public static T Clone<T>(T source, IReadOnlyHashSet<Type> ignoreTypes)
            where T:class
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (ignoreTypes is null)
            {
                throw new ArgumentNullException(nameof(ignoreTypes));
            }

            return (T)Clone(typeof(T), source, ignoreTypes);
        }
        public static object Clone(Type destType, object source)
        {
            if (destType is null)
            {
                throw new ArgumentNullException(nameof(destType));
            }

            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return Clone(destType, source, ReadOnlyHashSet<Type>.Empty);
        }
        public static object Clone(Type destType, object source, IReadOnlyHashSet<Type> ignoreTypes)
        {
            if (destType is null)
            {
                throw new ArgumentNullException(nameof(destType));
            }

            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (ignoreTypes is null)
            {
                throw new ArgumentNullException(nameof(ignoreTypes));
            }

            object instance = Create(destType);
            Clone(instance, source, ignoreTypes);
            return instance;
        }
        public static void Clone(object dest, object source)
        {
            if (dest is null)
            {
                throw new ArgumentNullException(nameof(dest));
            }

            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            Clone(dest, source, ReadOnlyHashSet<Type>.Empty);
        }
        public static void Clone(object dest, object source, IReadOnlyHashSet<Type> ignoreTypes)
        {
            if (dest is null)
            {
                throw new ArgumentNullException(nameof(dest));
            }

            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (ignoreTypes is null)
            {
                throw new ArgumentNullException(nameof(ignoreTypes));
            }

            Type destType = dest.GetType();
            Type sourceType = source.GetType();
            if (destType != sourceType && !sourceType.IsAssignableFrom(destType))
            {
                throw new InvalidOperationException("Dest and source type must equals or base on!");
            }
            IEnumerable<PropertyInfo> props = destType.GetProperties()
                .Where(x => x.CanWrite && !ignoreTypes.Contains(x.PropertyType));
            foreach (PropertyInfo item in props)
            {
                PropertyIdentity identity = new PropertyIdentity(destType, item.Name);
                PropertyGetter getter = CompiledPropertyInfo.GetGetter(identity);
                PropertySetter setter = CompiledPropertyInfo.GetSetter(identity);
                object sourceValue = getter(source);
                if (sourceValue is null)
                {
                    continue;
                }
                if (item.PropertyType.IsClass &&
                    item.PropertyType != StringType)
                {
                    object itemValue = Create(item.PropertyType);

                    if (itemValue is IList destEnu)
                    {
                        IList enu = (IList)sourceValue;
                        foreach (object e in enu)
                        {
                            destEnu.Add(e);
                        }
                    }
                    else
                    {
                        Clone(itemValue, sourceValue, ignoreTypes);
                        setter(dest, itemValue);
                    }
                }
                else
                {
                    setter(dest, sourceValue);
                }
            }
        }

    }
}
