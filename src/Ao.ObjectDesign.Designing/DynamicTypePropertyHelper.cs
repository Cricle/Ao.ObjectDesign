using Ao.ObjectDesign.Designing.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Ao.ObjectDesign.Designing
{
    public static class DynamicTypePropertyHelper
    {
        public static readonly IReadOnlyList<string> knowStartWiths = new string[]
        {
            "get","set","get_","set_","_"
        };

        private static readonly Dictionary<Type, string[]> propertyNames = new Dictionary<Type, string[]>();

        private static readonly Dictionary<Type, TypePropertyBoxInfo> propertyInfos = new Dictionary<Type, TypePropertyBoxInfo>();

        public static IEnumerable<IPropertyBox> EnumerablePropertyBoxs(Type type)
        {
            if (!propertyInfos.TryGetValue(type, out var propertyBoxInfo))
            {
                propertyBoxInfo = GetPropertyMap(type);
            }
            foreach (var item in propertyBoxInfo.Box.Values)
            {
                yield return item;
            }
        }
        public static IPropertyBox FindFirstVirtualProperty(Type type)
        {
            if (!propertyInfos.TryGetValue(type, out var propertyBoxInfo))
            {
                propertyBoxInfo = GetPropertyMap(type);
            }
            return propertyBoxInfo.FirstVirtual;
        }
        public static IPropertyBox FindPropertyBox(Type type,string name)
        {
            if(!propertyInfos.TryGetValue(type, out var propertyBoxInfo))
            {
                propertyBoxInfo = GetPropertyMap(type);
            }
            propertyBoxInfo.Box.TryGetValue(name, out var box);
            return box;
        }

        internal static TypePropertyBoxInfo GetPropertyMap(Type type)
        {
            if (!propertyInfos.TryGetValue(type, out var map))
            {
                var propertyBoxs = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .ToDictionary(x => x.Name, x =>
                    {
                        var box = new PropertyBox { property = x, name = x.Name };
                        box.EnsureBuild();
                        return box;
                    });
                map = new TypePropertyBoxInfo
                {
                    Box = propertyBoxs,
                    VirtualBox = new Dictionary<string, PropertyBox>()
                };
                //查看特性的
                var virtualProperies = new Dictionary<string, PropertyBox>();
                foreach (var item in type.GetMethods(BindingFlags.Public | BindingFlags.Instance))
                {
                    var attrGet = item.GetCustomAttribute<PlatformTargetGetMethodAttribute>();
                    if (attrGet != null)
                    {
                        var name = AnlysisName(attrGet, item);
                        ThrowIfContainsKey(name);
                        if (!virtualProperies.TryGetValue(name, out var box))
                        {
                            box = new PropertyBox { isBuilt = true, isVirtualPropery = true, name = name };
                            virtualProperies[name] = box;
                        }
                        if (box.getter != null)
                        {
                            throw new ArgumentException($"Virtual property {name} in type {type} has same getter method");
                        }
                        box.getter = VirtualPropertyCompiler.BuildGetter(item);
                    }
                    var attrSet = item.GetCustomAttribute<PlatformTargetSetMethodAttribute>();
                    if (attrSet != null)
                    {
                        var name = AnlysisName(attrSet, item);
                        ThrowIfContainsKey(name);
                        if (!virtualProperies.TryGetValue(name, out var box))
                        {
                            box = new PropertyBox { isBuilt = true, isVirtualPropery = true ,name=name};
                            virtualProperies[name] = box;
                        }
                        if (box.setter != null)
                        {
                            throw new ArgumentException($"Virtual property {name} in type {type} has same setter method");
                        }
                        box.setter = VirtualPropertyCompiler.BuildSetter(item);
                    }
                }
                foreach (var item in virtualProperies)
                {
                    map.VirtualBox.Add(item.Key, item.Value);
                    map.Box.Add(item.Key, item.Value);
                }
                map.FirstVirtual = map.VirtualBox.Values.FirstOrDefault();
                propertyInfos[type] = map;
            }
            return map;

            void ThrowIfContainsKey(string name)
            {
                if (map.Box.ContainsKey(name))
                {
                    throw new ArgumentException($"Property {name} is alread contains truly properties!");
                }
            }
        }
        private static string AnlysisName(PlatformTargetMethodAttribute attr, MethodInfo info)
        {
            if (attr.AutoAnalysis)
            {
                var methodName = info.Name;
                for (int i = 0; i < knowStartWiths.Count; i++)
                {
                    var selectWith = knowStartWiths[i];
                    if (methodName.StartsWith(selectWith, StringComparison.OrdinalIgnoreCase))
                    {
                        return methodName.Substring(selectWith.Length, methodName.Length - selectWith.Length);
                    }
                }
                throw new NotSupportedException($"Method {info.Name} can't analysis, only knows start with {string.Join(",", knowStartWiths)} methods");
            }
            return attr.Property;
        }
        public static string[] GetPropertyNames(Type type)
        {
            if (!propertyNames.TryGetValue(type, out var names))
            {
                var publics = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Select(p => p.Name);
                var nonPublics = type.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic)
                    .Where(x => x.IsDefined(typeof(IncludeDynamicNamettribute)))
                    .Select(x => x.Name);

                names = publics.Concat(nonPublics)
                    .Distinct()
                    .ToArray();
            }
            return names;
        }
    }
}
