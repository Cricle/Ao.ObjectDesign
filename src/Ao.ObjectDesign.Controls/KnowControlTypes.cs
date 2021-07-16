using Ao.ObjectDesign.Wpf;
using Ao.ObjectDesign.Wpf.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Ao.ObjectDesign.Controls
{
    public static class KnowControlTypes
    {
        public static readonly IReadOnlyList<DesignMapping> SettingTypes = GetControlTypes().ToArray();

        private static IEnumerable<DesignMapping> GetControlTypes()
        {
            IEnumerable<Type> types = typeof(KnowControlTypes).Assembly.GetExportedTypes()
                .Where(x => typeof(NotifyableObject).IsAssignableFrom(x));
            foreach (Type item in types)
            {
                MappingForAttribute mapping = item.GetCustomAttribute<MappingForAttribute>();
                if (mapping != null)
                {
                    yield return new DesignMapping(item, mapping.Type);
                }
            }
        }
    }
}
