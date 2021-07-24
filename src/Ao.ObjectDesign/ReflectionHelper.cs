using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Ao.ObjectDesign
{
    public static partial class ReflectionHelper
    {
        private static readonly string IListTypeName = typeof(IList).FullName;
        private static readonly Type StringType = typeof(string);

        public static object Create(Type type)
        {
            TypeCreator creator = CompiledPropertyInfo.GetCreator(type);
            object obj = creator();
            return obj;
        }
        public static object GetValue(object instance, PropertyInfo propertyInfo)
        {
            PropertyIdentity identity = new PropertyIdentity(propertyInfo.DeclaringType, propertyInfo.Name);
            return GetValue(instance, identity);
        }
        public static object GetValue(object instance, PropertyIdentity identity)
        {
            PropertyGetter getter = CompiledPropertyInfo.GetGetter(identity);
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
            setter(instance, value);
        }
        public static T Clone<T>(T source)
        {
            return Clone(source, ReadOnlyHashSet<Type>.Empty);
        }
        public static T Clone<T>(T source, IReadOnlyHashSet<Type> ignoreTypes)
        {
            return (T)Clone(typeof(T), source, ignoreTypes);
        }
        public static object Clone(Type destType, object source)
        {
            return Clone(destType, source, ReadOnlyHashSet<Type>.Empty);
        }
        public static object Clone(Type destType, object source, IReadOnlyHashSet<Type> ignoreTypes)
        {
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
            Type destType = dest.GetType();
            Type sourceType = source.GetType();
            if (destType != sourceType)
            {
                throw new InvalidOperationException("Dest and source type must equals!");
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
