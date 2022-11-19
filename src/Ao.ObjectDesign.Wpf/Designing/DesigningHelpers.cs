using Ao.ObjectDesign.Designing.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Ao.ObjectDesign.Designing
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
            var list = new List<PropertyIdentity>();
            var types = assembly.GetExportedTypes();
            var len = types.Length;
            for (int i = 0; i < len; i++)
            {
                var type = types[i];
                DesignForAttribute attr = type.GetCustomAttribute<DesignForAttribute>();
                if (attr != null)
                {
                    PropertyInfo prop = type.GetProperties().Where(y => y.PropertyType == attr.Type).FirstOrDefault();
                    if (prop != null)
                    {
                        list.Add(new PropertyIdentity(type, prop.Name));
                    }
                }
            }
            return new ReadOnlyHashSet<PropertyIdentity>(list);
        }
        public static IReadOnlyHashSet<Type> GetDesigningTypes(Assembly assembly)
        {
            if (assembly is null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }
            var list = new List<Type>();
            var types = assembly.GetExportedTypes();
            var len = types.Length;
            for (int i = 0; i < len; i++)
            {
                var type = types[i];
                var attr = type.GetCustomAttribute<DesignForAttribute>();
                if (attr != null)
                {
                    list.Add(attr.Type);
                }
            }
            return new ReadOnlyHashSet<Type>(list);
        }
    }
}
