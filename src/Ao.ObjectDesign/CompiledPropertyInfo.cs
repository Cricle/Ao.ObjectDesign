using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;

namespace Ao.ObjectDesign
{
    public delegate void PropertySetter(object instance, object value);
    public delegate object PropertyGetter(object instance);
    public delegate object TypeCreator();

    public static class CompiledPropertyInfo
    {
        private static readonly Type ObjectType = typeof(object);
        private static readonly Type TypeCreatorType = typeof(TypeCreator);
        private static readonly Type PropertyGetterType = typeof(PropertyGetter);
        private static readonly Type PropertySetterType = typeof(PropertySetter);

        private static readonly Func<PropertyIdentity, PropertySetter> setterFunc = CreateSetter;
        private static readonly Func<PropertyIdentity, PropertyGetter> getterFunc = CreateGetter;

        private static readonly ConcurrentDictionary<PropertyIdentity, PropertySetter> propertySetters =
            new ConcurrentDictionary<PropertyIdentity, PropertySetter>(PropertyIdentityComparer.Instance);

        private static readonly ConcurrentDictionary<PropertyIdentity, PropertyGetter> propertyGetters =
            new ConcurrentDictionary<PropertyIdentity, PropertyGetter>(PropertyIdentityComparer.Instance);

        private static readonly ConcurrentDictionary<Type, TypeCreator> typeCreators =
            new ConcurrentDictionary<Type, TypeCreator>();

        private static PropertySetter CreateSetter(PropertyIdentity x)
        {
            PropertyInfo propertInfo = x.Type.GetProperty(x.PropertyName);
            DynamicMethod dn = CompiledPropertyVisitor.CreateObjectSetter(x.Type, propertInfo);
            Debug.Assert(dn != null);
            return (PropertySetter)dn.CreateDelegate(PropertySetterType);
        }
        private static PropertyGetter CreateGetter(PropertyIdentity x)
        {
            PropertyInfo propertInfo = x.Type.GetProperty(x.PropertyName);
            DynamicMethod dn = CompiledPropertyVisitor.CreateObjectGetter(x.Type, propertInfo);
            Debug.Assert(dn != null);
            return (PropertyGetter)dn.CreateDelegate(PropertyGetterType);
        }

        public static TypeCreator GetCreator(Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return typeCreators.GetOrAdd(type, t =>
            {
                ConstructorInfo construct = type.GetConstructor(Type.EmptyTypes);
                if (construct is null)
                {
                    throw new NotSupportedException($"Type {type.FullName} can't build, it has not empty argument constructor");
                }
                string name = string.Concat("create", type.Name);
                var dn = new DynamicMethod(name, ObjectType, Type.EmptyTypes, true);
                var il = dn.GetILGenerator();
                il.Emit(OpCodes.Newobj, construct);
                il.Emit(OpCodes.Ret);
                return (TypeCreator)dn.CreateDelegate(TypeCreatorType);
            });
        }
        public static PropertySetter GetSetter(PropertyIdentity identity)
        {
            if (identity is null)
            {
                throw new ArgumentNullException(nameof(identity));
            }

            return propertySetters.GetOrAdd(identity, setterFunc);
        }
        public static PropertyGetter GetGetter(PropertyIdentity identity)
        {
            if (identity is null)
            {
                throw new ArgumentNullException(nameof(identity));
            }

            return propertyGetters.GetOrAdd(identity, getterFunc);
        }
    }
}
