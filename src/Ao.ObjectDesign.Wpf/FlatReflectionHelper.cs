using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Wpf
{
    public static class FlatReflectionHelper
    {
        public static IReadOnlyCollection<SpecularMappingItem> SpecularMapping(object source,object target)
        {
            var res = new List<SpecularMappingItem>();
            var sourceProperties = source.GetType().GetProperties();
            var destProperties = target.GetType().GetProperties()
                .ToDictionary(x=>x.Name);
            foreach (var item in sourceProperties)
            {
                if (destProperties.TryGetValue(item.Name, out var propInfo) &&
                    item.PropertyType == propInfo.PropertyType)
                {
                    var sourceValue = ReflectionHelper.GetValue(source, item);
                    ReflectionHelper.SetValue(target, sourceValue, propInfo);
                    res.Add(new SpecularMappingItem(item, propInfo, sourceValue, true));
                }
                else
                {
                    res.Add(new SpecularMappingItem(item, null, null, false));
                }
            }
            return res;
        }
    }
}
