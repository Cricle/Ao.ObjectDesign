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
            return CompiledPropertyInfo.GetGetter(identity);
        }

        protected override PropertySetter GetPropertySetter(in PropertyIdentity identity)
        {
            return CompiledPropertyInfo.GetSetter(identity);
        }
    }
}