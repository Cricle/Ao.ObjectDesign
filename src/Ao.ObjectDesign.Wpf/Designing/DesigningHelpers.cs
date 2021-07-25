using Ao.ObjectDesign.Designing.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Ao.ObjectDesign.Wpf.Designing
{
    public static class DesigningHelpers
    {
        private static IReadOnlyHashSet<Type> knowDesigningTypes;
        private static IReadOnlyHashSet<PropertyIdentity> knowDesigningPropertyIdentities;
        public static IReadOnlyHashSet<PropertyIdentity> KnowDesigningPropertyIdentities
        {
            get
            {
                if (knowDesigningPropertyIdentities is null)
                {
                    knowDesigningPropertyIdentities = GetDesigningPropertyIdentities(typeof(DesigningHelpers).Assembly);
                }
                return knowDesigningPropertyIdentities;
            }
        }
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
        public static IReadOnlyHashSet<PropertyIdentity> GetDesigningPropertyIdentities(Assembly assembly)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            IEnumerable<PropertyIdentity> types = assembly.GetExportedTypes()
                .Select(x =>
                {
                    DesignForAttribute attr = x.GetCustomAttribute<DesignForAttribute>();
                    if (attr is null)
                    {
                        return null;
                    }
                    PropertyInfo prop = x.GetProperties().Where(y => y.PropertyType == attr.Type).FirstOrDefault();
                    if (prop is null)
                    {
                        return null;
                    }
                    return new PropertyIdentity(x, prop.Name);
                })
                .Where(x => x != null);
            return new ReadOnlyHashSet<PropertyIdentity>(types);
        }
        public static IReadOnlyHashSet<Type> GetDesigningTypes(Assembly assembly)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            IEnumerable<Type> types = assembly.GetExportedTypes()
                .Select(x => x.GetCustomAttribute<DesignForAttribute>())
                .Where(x => x != null)
                .Select(x => x.Type);
            return new ReadOnlyHashSet<Type>(types);
        }
    }
}
