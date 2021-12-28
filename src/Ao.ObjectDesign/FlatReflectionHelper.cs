using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign
{
    public static class FlatReflectionHelper
    {
        private static readonly ConcurrentDictionary<Type, IReadOnlyDictionary<string, TypeProperty>> typeToRefs=new ConcurrentDictionary<Type, IReadOnlyDictionary<string, TypeProperty>>();
        private static IReadOnlyDictionary<string, TypeProperty> GetPropertyMaps(Type type)
        {
            return typeToRefs.GetOrAdd(type, CreatePropertyMaps);
        }
        private static IReadOnlyDictionary<string, TypeProperty> CreatePropertyMaps(Type type)
        {
            return TypeMappings.GetTypeProperties(type).ToDictionary(x => x.Name);
        }


        public static IReadOnlyList<SpecularMappingItem> SpecularMapping(object source, object target)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (target is null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            var sourceProperties = TypeMappings.GetTypeProperties(source.GetType());
            var destProperties = GetPropertyMaps(target.GetType());
            var res = new List<SpecularMappingItem>(destProperties.Count);
            foreach (var item in sourceProperties)
            {
                if (item.CanGet &&
                    destProperties.TryGetValue(item.Name, out var propInfo) &&
                    propInfo.CanSet)
                {
                    Debug.Assert(item.PropertyInfo != null);
                    Debug.Assert(propInfo.PropertyInfo != null);
                    if (item.PropertyInfo.PropertyType == propInfo.PropertyInfo.PropertyType)
                    {
                        var sourceValue = item.Getter(source);
                        propInfo.Setter(target, sourceValue);
                        res.Add(new SpecularMappingItem(item.PropertyInfo, propInfo.PropertyInfo, sourceValue, true));
                    }
                    else
                    {
                        res.Add(new SpecularMappingItem(item.PropertyInfo, propInfo.PropertyInfo, null, false));
                    }
                }
                else
                {
                    res.Add(new SpecularMappingItem(item.PropertyInfo, null, null, false));
                }
            }
            return res;
        }
    }
}
