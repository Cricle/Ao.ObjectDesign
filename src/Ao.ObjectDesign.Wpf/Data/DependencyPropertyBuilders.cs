using System;
using System.Windows;
using System.Windows.Data;
using LExpression = System.Linq.Expressions;

namespace Ao.ObjectDesign.Wpf.Data
{
    public static class DependencyPropertyBuilders
    {
        public static IBindingMaker Creator(this DependencyProperty property, string name)
        {
            return new BindingMaker(property).AddSetPath(name);
        }
        public static IBindingScope Scope(this DependencyProperty property, string name)
        {
            return Creator(property, name).Build();
        }
        public static BindingExpressionBase Bind<T>(this DependencyProperty property,
            DependencyObject @object,
            T source,
            LExpression.Expression<Func<T, object>> nameExp)
        {
            string name=null;
            if (nameExp.Body is LExpression.MemberExpression me)
            {
                name = me.Member.Name;
            }
            else if (nameExp.Body is LExpression.UnaryExpression ue&&
                ue.Operand is LExpression.MemberExpression ueme)
            {
                name = ueme.Member.Name;
            }
            if (string.IsNullOrEmpty(name))
            {
                throw new NotSupportedException(nameExp.ToString());
            }
            return Bind(property, @object, source, name);
        }
        public static BindingExpressionBase Bind(this DependencyProperty property, DependencyObject @object, object source, string name)
        {
            return Scope(property, name).Bind(@object, source);
        }
        public static IWithSourceBindingScope WithSourceScope(this DependencyProperty property, string name,object source)
        {
            return Creator(property, name).Build().ToWithSource(source);
        }

    }

}
