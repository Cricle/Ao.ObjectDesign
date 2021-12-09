using System;
using System.Linq.Expressions;
using System.Reflection;
using FastExpressionCompiler;

namespace Ao.ObjectDesign
{
    public class ExpressionPropertyVisitor : SelfPropertyVisitor
    {
        public ExpressionPropertyVisitor(object declaringInstance, PropertyInfo propertyInfo) : base(declaringInstance, propertyInfo)
        {
        }

        protected override PropertyGetter GetPropertyGetter(in PropertyIdentity identity)
        {
            var par1 = Expression.Parameter(typeof(object));

            var exp = Expression.Convert(
                Expression.Call(
                    Expression.Convert(par1, identity.Type), identity.PropertyInfo.GetMethod),typeof(object));

            return Expression.Lambda<PropertyGetter>(exp, par1).CompileFast();
        }

        protected override PropertySetter GetPropertySetter(in PropertyIdentity identity)
        {
            var par1 = Expression.Parameter(typeof(object));
            var par2 = Expression.Parameter(typeof(object));

            var exp = Expression.Call(
                        Expression.Convert(par1, identity.Type), identity.PropertyInfo.SetMethod,
                            Expression.Convert(par2,identity.PropertyInfo.PropertyType));

            return Expression.Lambda<PropertySetter>(exp, par1, par2).CompileFast();
        }
    }
}