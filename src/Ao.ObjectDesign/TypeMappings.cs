using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Ao.ObjectDesign
{
    public static class TypeMappings
    {
        private static readonly ConcurrentDictionary<Type, IReadOnlyList<TypeProperty>> typeToProperties = new ConcurrentDictionary<Type, IReadOnlyList<TypeProperty>>();
        private static readonly Func<Type, IReadOnlyList<TypeProperty>> CreateTypePropertiesFunc = CreateTypeProperties;

        public static IReadOnlyList<TypeProperty> GetTypeProperties(Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return typeToProperties.GetOrAdd(type, CreateTypePropertiesFunc);
        }
        private static IReadOnlyList<TypeProperty> CreateTypeProperties(Type type)
        {
            var prop = type.GetProperties();
            var len = prop.Length;
            var res = new TypeProperty[len];
            for (int i = 0; i < len; i++)
            {
                var op = prop[i];
                var identity = new PropertyIdentity(type, op.Name);
                PropertyGetter getter = null;
                PropertySetter setter = null; 
                if (op.CanWrite)
                {
                    setter = CompiledPropertyInfo.GetSetter(identity);
                }
                if (op.CanRead)
                {
                    getter = CompiledPropertyInfo.GetGetter(identity);
                }
                var p = new TypeProperty(op,
                    setter,
                    getter,
                    op.Name, 
                    op.CanRead,
                    op.CanWrite, 
                    identity);
               
                res[i] = p;
            }
            return res;
        }
    }
}
