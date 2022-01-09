﻿using System;
using System.Collections;
using System.Diagnostics;
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
            where T : class
        {
            return Clone(source, ReadOnlyHashSet<Type>.Empty);
        }
        public static T Clone<T>(T source, IReadOnlyHashSet<Type> ignoreTypes)
            where T : class
        {
            return (T)Clone(typeof(T), source, ignoreTypes);
        }
        public static object Clone(Type destType, object source)
        {
            return Clone(destType, source, ReadOnlyHashSet<Type>.Empty);
        }
        public static object Clone(Type destType, object source, IReadOnlyHashSet<Type> ignoreTypes)
        {
            if (destType is null)
            {
                throw new ArgumentNullException(nameof(destType));
            }

            object instance = Create(destType);
            Clone(instance, source, ignoreTypes);
            return instance;
        }
        public static void Clone(object dest, object source)
        {
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
            if (!sourceType.IsInstanceOfType(dest))
            {
                throw new InvalidOperationException("Dest and source type must equals or base on!");
            }
            var tps = TypeMappings.GetTypeProperties(destType);
            var len = tps.Count;
            for (int i = 0; i < len; i++)
            {
                var item = tps[i];
                Debug.Assert(item.PropertyInfo != null);
                var type = item.PropertyInfo.PropertyType;
                if (!item.CanSet || ignoreTypes.Contains(type))
                {
                    continue;
                }
                object sourceValue = item.Getter(source);
                if (sourceValue is null)
                {
                    continue;
                }
                if (type.IsClass &&
                    type != StringType)
                {
                    object itemValue = Create(type);

                    if (itemValue is IList destEnu)
                    {
                        IList enu = (IList)sourceValue;
                        var enuCount = enu.Count;
                        for (int j = 0; j < enuCount; j++)
                        {
                            destEnu.Add(enu[j]);
                        }
                    }
                    else
                    {
                        Clone(itemValue, sourceValue, ignoreTypes);
                        item.Setter(dest, itemValue);
                    }
                }
                else
                {
                    item.Setter(dest, sourceValue);
                }
            }
        }
    }
}
