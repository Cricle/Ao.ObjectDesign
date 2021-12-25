using FastExpressionCompiler;
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
            CheckPropertyIdentity(ref x);
            var par1 = Expression.Parameter(typeof(object));
            var par2 = Expression.Parameter(typeof(object));

            var exp = Expression.Call(
                        Expression.Convert(par1, x.Type), x.PropertyInfo.SetMethod,
                            Expression.Convert(par2, x.PropertyInfo.PropertyType));

            return Expression.Lambda<PropertySetter>(exp, par1, par2).CompileSys();
        }
        private static PropertyGetter CreateGetter(PropertyIdentity x)
        {
            CheckPropertyIdentity(ref x);
            var par1 = Expression.Parameter(typeof(object));

            var exp = Expression.Convert(
                Expression.Call(
                    Expression.Convert(par1, x.Type), x.PropertyInfo.GetMethod),typeof(object));

            return Expression.Lambda<PropertyGetter>(exp, par1).CompileSys();
        }
        private static void CheckPropertyIdentity(ref PropertyIdentity x)
        {
            if (x.PropertyInfo is null)
            {
                throw new ArgumentException("Property identity can't found property info!");
            }
        }
        public static TypeCreator GetCreator(Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            return typeCreators.GetOrAdd(type, t =>
            {
                if (!t.IsValueType && t.GetConstructor(Type.EmptyTypes) is null)
                {
                    throw new NotSupportedException($"Type {t.FullName} can't build, it has not empty argument constructor");
                }
                var exp = Expression.Convert(Expression.New(t), typeof(object));
                return Expression.Lambda<TypeCreator>(exp).CompileSys();
            });
        }
        public static PropertySetter GetSetter(in PropertyIdentity identity)
        {
            return propertySetters.GetOrAdd(identity, setterFunc);
        }
        public static PropertyGetter GetGetter(in PropertyIdentity identity)
        {
            return propertyGetters.GetOrAdd(identity, getterFunc);
        }

    }
}
