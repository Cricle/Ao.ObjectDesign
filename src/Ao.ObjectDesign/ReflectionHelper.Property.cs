using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Ao.ObjectDesign
{
    public static partial class ReflectionHelper
    {
        private static readonly ConcurrentDictionary<Type, WeakReference<IReadOnlyDictionary<PropertyIdentity, object>>> defaultValueCache
            = new ConcurrentDictionary<Type, WeakReference<IReadOnlyDictionary<PropertyIdentity, object>>>();

        public static IReadOnlyDictionary<Type, IReadOnlyDictionary<PropertyIdentity, object>> GetDefaultValueMap(Type type, bool ignoreNoAttribute)
        {
            var res = new Dictionary<Type, IReadOnlyDictionary<PropertyIdentity, object>>();
            var includeTypes = new List<Type> { type };
            while (includeTypes.Count != 0)
            {
                var nextTypes = new List<Type>();
                foreach (var item in includeTypes)
                {
                    if (res.ContainsKey(item))
                    {
                        continue;
                    }

                    var properties = item.GetProperties();
                    if (defaultValueCache.TryGetValue(item, out var weakPropertyDefaults) &&
                        weakPropertyDefaults.TryGetTarget(out var propertyDefaults))
                    {
                        if (ignoreNoAttribute)
                        {
                            var hasAttributeProperties = new HashSet<string>(properties.Where(x => x.GetCustomAttribute<DefaultValueAttribute>() != null).Select(x => x.Name));
                            propertyDefaults = propertyDefaults.Where(x => hasAttributeProperties.Contains(x.Key.PropertyName))
                                .ToDictionary(x => x.Key, x => x.Value);
                        }
                        res.Add(item, propertyDefaults);
                        continue;
                    }

                    var defMap = new Dictionary<PropertyIdentity, object>();
                    res.Add(item, defMap);

                    foreach (var prop in properties)
                    {
                        var identity = new PropertyIdentity(item, prop.Name);
                        if (!defMap.ContainsKey(identity))
                        {
                            var def = prop.GetCustomAttribute<DefaultValueAttribute>();
                            if (def != null)
                            {
                                defMap.Add(identity, def.Value);
                            }
                            else if (!ignoreNoAttribute)
                            {
                                object value = null;
                                if (prop.PropertyType.IsValueType)
                                {
                                    value = Activator.CreateInstance(prop.PropertyType);
                                }
                                defMap.Add(identity, value);
                            }
                        }
                    }
                    var readonlyDefMap = new ReadOnlyDictionary<PropertyIdentity, object>(defMap);
                    if (weakPropertyDefaults is null)
                    {
                        defaultValueCache[item] = new WeakReference<IReadOnlyDictionary<PropertyIdentity, object>>(readonlyDefMap);
                    }
                    else
                    {
                        weakPropertyDefaults.SetTarget(readonlyDefMap);
                    }
                }
                includeTypes = includeTypes
                    .SelectMany(x => x.GetProperties().Select(p => p.PropertyType))
                    .Where(CanStepIn)
                    .ToList();
            }
            return res;

            bool CanStepIn(Type testType)
            {
                return testType.IsClass && 
                    testType != StringType&&
                    !res.ContainsKey(testType);
            }
        }
        public static void SetDefault(object instance, SetDefaultOptions options)
        {
            if (instance is null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            SetDefault(instance, instance.GetType(), options);
        }
        public static void SetDefault(object instance, Type type, SetDefaultOptions options)
        {
            if (instance is null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            var map = GetDefaultValueMap(type, (options & SetDefaultOptions.IgnoreNoAttribute) != 0);
            SetDefault(instance, map, options);
        }
        public static void SetDefault(object instance,
            IReadOnlyDictionary<Type, IReadOnlyDictionary<PropertyIdentity, object>> defaultValueMap, 
            SetDefaultOptions options)
        {
            if (instance is null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            if (defaultValueMap is null)
            {
                throw new ArgumentNullException(nameof(defaultValueMap));
            }

            var type = instance.GetType();
            var properties = type.GetProperties();

            if (defaultValueMap.TryGetValue(type, out var layoutMap))
            {
                foreach (var item in properties)
                {
                    var identity = new PropertyIdentity(type, item.Name);
                    if (item.PropertyType.IsClass &&
                        item.PropertyType != StringType)
                    {
                        if ((options & SetDefaultOptions.ClassGenerateNew) != 0)
                        {
                            var v = Create(item.PropertyType);
                            SetValue(instance, v, identity);
                            continue;
                        }
                        else
                        {
                            var propValue = GetValue(instance, identity);
                            if (propValue != null && (options & SetDefaultOptions.IgnoreNotNull) == 0)
                            {
                                SetDefault(propValue, defaultValueMap, options);
                                continue;
                            }
                        }
                    }
                    if (layoutMap.TryGetValue(identity, out var val))
                    {
                        SetValue(instance, val, identity);
                    }
                }
            }
        }
    }
}
