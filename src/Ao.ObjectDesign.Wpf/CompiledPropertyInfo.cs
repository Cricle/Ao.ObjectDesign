using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;

namespace Ao.ObjectDesign.Wpf
{
    internal delegate void PropertySetter(object instance, object value);
    internal delegate object PropertyGetter(object instance);
    internal delegate object TypeCreator();
    internal static class CompiledPropertyInfo
    {
        private static readonly ConcurrentDictionary<PropertyIdentity, PropertySetter> propertySetters =
            new ConcurrentDictionary<PropertyIdentity, PropertySetter>(PropertyIdentityComparer.Instance);

        private static readonly ConcurrentDictionary<PropertyIdentity, PropertyGetter> propertyGetters =
            new ConcurrentDictionary<PropertyIdentity, PropertyGetter>(PropertyIdentityComparer.Instance);
        
        private static readonly ConcurrentDictionary<Type, TypeCreator> typeCreators =
            new ConcurrentDictionary<Type, TypeCreator>();

        public static TypeCreator GetCreator(Type type)
        {
            return typeCreators.GetOrAdd(type, t =>
            {
                var construct = type.GetConstructor(Type.EmptyTypes);
                if (construct is null)
                {
                    throw new NotSupportedException($"Type {type.FullName} can't build, it has not empty argument constructor");
                }
                return Expression.Lambda<TypeCreator>(Expression.Convert(Expression.New(construct), typeof(object))).Compile();
            });
        }
        public static PropertySetter GetSetter(PropertyIdentity identity)
        {
            return propertySetters.GetOrAdd(identity, x =>
            {
                var propertInfo = x.Type.GetProperty(x.PropertyName);
                var dn = CompiledPropertyVisitor.CreateObjectSetter(x.Type, propertInfo);
                return (PropertySetter)dn.CreateDelegate(typeof(PropertySetter));
            });
        }
        public static PropertyGetter GetGetter(PropertyIdentity identity)
        {
            return propertyGetters.GetOrAdd(identity, x =>
            {
                var propertInfo = x.Type.GetProperty(x.PropertyName);
                var dn = CompiledPropertyVisitor.CreateObjectGetter(x.Type, propertInfo);
                return (PropertyGetter)dn.CreateDelegate(typeof(PropertyGetter));
            });
        }

    }
}
