using Ao.ObjectDesign.Wpf;
using Ao.ObjectDesign.Wpf.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Controls
{
    public static class KnowControlTypes
    {
        public static readonly IReadOnlyList<DesignMapping> SettingTypes = GetControlTypes().ToArray();

        private static IEnumerable<DesignMapping> GetControlTypes()
        {
            var types = typeof(KnowControlTypes).Assembly.GetExportedTypes()
                .Where(x => typeof(NotifyableObject).IsAssignableFrom(x));
            foreach (var item in types)
            {
                var mapping = item.GetCustomAttribute<MappingForAttribute>();
                if (mapping != null)
                {
                    yield return new DesignMapping(item, mapping.Type);
                }
            }
        }
    }
}
