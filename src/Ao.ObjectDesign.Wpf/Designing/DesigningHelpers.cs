using Ao.ObjectDesign.Wpf.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Wpf.Designing
{
    public static class DesigningHelpers
    {
        private static IReadOnlyHashSet<Type> knowDesigningTypes;
        public static IReadOnlyHashSet<Type> KnowDesigningTypes
        {
            get
            {
                if (knowDesigningTypes is null)
                {
                    knowDesigningTypes = GetDesigningTypes(typeof(DesigningHelpers).Assembly);
                }
                return knowDesigningTypes;
            }
        }
        public static IReadOnlyHashSet<Type> GetDesigningTypes(Assembly assembly)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            var types= assembly.GetExportedTypes()
                .Select(x => x.GetCustomAttribute<DesignForAttribute>())
                .Where(x => x != null)
                .Select(x => x.Type);
            return new ReadOnlyHashSet<Type>(types);
        }
    }
}
