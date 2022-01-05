using Ao.ObjectDesign.Designing.Annotations;
using FastExpressionCompiler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Ao.ObjectDesign.Designing
{
    internal static class DynamicTypePropertyHelper
    {
        public static readonly IReadOnlyList<string> knowStartWiths = new string[]
        {
            "get","set","get_","set_","_"
        };

        private static readonly Dictionary<Type, string[]> propertyNames = new Dictionary<Type, string[]>();
        
        private static readonly Dictionary<Type, Dictionary<string, PropertyBox>> propertyInfos = new Dictionary<Type, Dictionary<string, PropertyBox>>();

        public static IReadOnlyDictionary<string, PropertyBox> GetPropertyMap(Type type)
        {
            if (!propertyInfos.TryGetValue(type,out var map))
            {
                map = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .ToDictionary(x => x.Name, x => new PropertyBox { Property = x });
                //查看特性的
                var virtualProperies = new Dictionary<string, PropertyBox>();
                foreach (var item in type.GetMethods(BindingFlags.Public | BindingFlags.Instance))
                {
                    var attrGet = item.GetCustomAttribute<PlatformTargetGetMethodAttribute>();
                    if (attrGet != null)
                    {
                        var name = AnlysisName(attrGet,item);
                        ThrowIfContainsKey(name);
                        if (!virtualProperies.TryGetValue(name, out var box))
                        {
                            box = new PropertyBox { IsBuilt = true, IsVirtualPropery = true };
                            virtualProperies[name] = box;
                        }
                        if (box.Getter!=null)
                        {
                            throw new ArgumentException($"Virtual property {name} in type {type} has same getter method");
                        }
                        box.Getter = VirtualPropertyCompiler.BuildGetter(item);
                    }
                    var attrSet = item.GetCustomAttribute<PlatformTargetSetMethodAttribute>();
                    if (attrSet != null)
                    {
                        var name = AnlysisName(attrSet, item);
                        ThrowIfContainsKey(name);
                        if (!virtualProperies.TryGetValue(name, out var box))
                        {
                            box = new PropertyBox { IsBuilt = true, IsVirtualPropery = true };
                            virtualProperies[name] = box;
                        }
                        if (box.Setter != null)
                        {
                            throw new ArgumentException($"Virtual property {name} in type {type} has same setter method");
                        }
                        box.Setter = VirtualPropertyCompiler.BuildSetter(item);
                    }
                }
                foreach (var item in virtualProperies)
                {
                    map.Add(item.Key, item.Value);
                }
                propertyInfos[type] = map;
            }
            return map;

            void ThrowIfContainsKey(string name)
            {
                if (map.ContainsKey(name))
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
                    var selectWith= knowStartWiths[i];
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
                var nonPublics= type.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic)
                    .Where(x=>x.IsDefined(typeof(IncludeDynamicNamettribute)))
                    .Select(x=>x.Name);

                names = publics.Concat(nonPublics)
                    .Distinct()
                    .ToArray();
            }
            return names;
        }
    }
}
