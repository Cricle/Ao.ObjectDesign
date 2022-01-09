using System;
using System.Diagnostics;
using System.Reflection;
using LExpressions = System.Linq.Expressions;

namespace Ao.ObjectDesign.Wpf.Data
{
    [DebuggerDisplay("Source: {Source}, Property:{PropertyInfo}")]
    public class PropertyWithSourceBindingScope : TargetWithSourceBindingScope
    {
        public PropertyWithSourceBindingScope(object source, PropertyInfo propertyInfo, IBindingScope scope)
            : base(source, scope)
        {
            PropertyInfo = propertyInfo ?? throw new ArgumentNullException(nameof(propertyInfo));
        }

        public PropertyInfo PropertyInfo { get; }

        protected override object GetTargetValue()
        {
            Debug.Assert(Source != null);
            Debug.Assert(PropertyInfo != null);
            return ReflectionHelper.GetValue(Source, PropertyInfo);
        }

        public static PropertyWithSourceBindingScope FromExpression<T>(T source,
            LExpressions.Expression<Func<T, object>> visitor,
            IBindingScope scope)
            where T : class
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (visitor is null)
            {
                throw new ArgumentNullException(nameof(visitor));
            }

            if (scope is null)
            {
                throw new ArgumentNullException(nameof(scope));
            }

            PropertyInfo prop;
            if (visitor.Body is LExpressions.MemberExpression memberExp)
            {
                ThrowIfNotPropertyInfo(memberExp.Member);
                prop = (PropertyInfo)memberExp.Member;
            }
            else
            {
                throw new NotSupportedException($"The visitor {visitor} is not support");
            }

            Debug.Assert(prop != null);

            return new PropertyWithSourceBindingScope(source, prop, scope);

            void ThrowIfNotPropertyInfo(MemberInfo info)
            {
                if (!(info is PropertyInfo))
                {
                    throw new ArgumentException($"Visitor result {info} is not target to property!");
                }
            }
        }
    }
}
