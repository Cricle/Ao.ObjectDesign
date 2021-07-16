using Ao.ObjectDesign.Wpf.Designing;
using System;
using System.Collections;
using System.Linq;
using System.Reflection;

namespace Ao.ObjectDesign.Wpf
{
    public static class ReflectionHelper
    {
        private static readonly string IListTypeName = typeof(IList).FullName;
        private static readonly Type StringType = typeof(string);

        public static object Create(Type type)
        {
            var creator = CompiledPropertyInfo.GetCreator(type);
            var obj = creator();
            return obj;
        }
        public static object GetValue(object instance, PropertyInfo propertyInfo)
        {
            var identity = new PropertyIdentity(propertyInfo.DeclaringType, propertyInfo.Name);
            return GetValue(instance, identity);
        }
        public static object GetValue(object instance, PropertyIdentity identity)
        {
            var getter = CompiledPropertyInfo.GetGetter(identity);
            return getter(instance);
        }
        public static void SetValue(object instance, object value, PropertyInfo propertyInfo)
        {
            var identity = new PropertyIdentity(propertyInfo.DeclaringType, propertyInfo.Name);
            SetValue(instance, value, identity);
        }
        public static void SetValue(object instance,object value, PropertyIdentity identity)
        {
            var setter = CompiledPropertyInfo.GetSetter(identity);
            setter(instance, value);
        }
        public static T Clone<T>(T source)
        {
            return Clone(source, ReadOnlyHashSet<Type>.Empty);
        }
        public static T CloneIgnoreDesigners<T>(T source)
        {
            return Clone(source, DesigningHelpers.KnowDesigningTypes);
        }
        public static T Clone<T>(T source, IReadOnlyHashSet<Type> ignoreTypes)
        {
            return (T)Clone(typeof(T), source, ignoreTypes);
        }
        public static object Clone(Type destType, object source)
        {
            return Clone(destType, source, ReadOnlyHashSet<Type>.Empty);
        }
        public static object CloneIgnoreDesigners(Type destType, object source)
        {
            return Clone(destType, source, DesigningHelpers.KnowDesigningTypes);
        }
        public static object Clone(Type destType, object source, IReadOnlyHashSet<Type> ignoreTypes)
        {
            var instance = Create(destType);
            Clone(instance, source, ignoreTypes);
            return instance;
        }
        public static void Clone(object dest, object source)
        {
            Clone(dest, source,ReadOnlyHashSet<Type>.Empty);
        }
        public static void CloneIgnoreDesigners(object dest, object source)
        {
            Clone(dest, source, DesigningHelpers.KnowDesigningTypes);
        }
        public static void Clone(object dest, object source, IReadOnlyHashSet<Type> ignoreTypes)
        {
            var destType = dest.GetType();
            var sourceType = source.GetType();
            if (destType != sourceType)
            {
                throw new InvalidOperationException("Dest and source type must equals!");
            }
            var props = destType.GetProperties()
                .Where(x => !ignoreTypes.Contains(x.PropertyType));
            foreach (var item in props)
            {
                if (item.CanWrite)
                {
                    var identity = new PropertyIdentity(destType, item.Name);
                    var getter = CompiledPropertyInfo.GetGetter(identity);
                    var setter = CompiledPropertyInfo.GetSetter(identity);
                    var sourceValue = getter(source);
                    if (sourceValue is null)
                    {
                        continue;
                    }
                    if (item.PropertyType.IsClass &&
                        item.PropertyType != StringType)
                    {
                        if (item.PropertyType.GetInterface(IListTypeName) != null)
                        {
                            var itemValue = Create(item.PropertyType);
                            var enu = (IList)sourceValue;
                            var destEnu = (IList)itemValue;
                            foreach (var e in enu)
                            {
                                destEnu.Add(e);
                            }
                        }
                        else
                        {
                            var itemValue = Create(item.PropertyType);
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
}
