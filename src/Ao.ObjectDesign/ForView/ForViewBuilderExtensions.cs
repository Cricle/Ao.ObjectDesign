using Ao.ObjectDesign.Abstract.Annotations;
using System;
using System.Linq;
using System.Reflection;

namespace Ao.ObjectDesign.ForView
{
    public static class ForViewBuilderExtensions
    {
        public static TView Build<TView, TContext>(this IForViewBuilder<TView, TContext> builder, TContext context)
            where TContext : IForViewBuildContext
        {
            return Build(builder, context, true);
        }
        public static TView Build<TView, TContext>(this IForViewBuilder<TView, TContext> builder, TContext context, bool forceSelectBuild)
            where TContext : IForViewBuildContext
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            var attr = context.PropertyProxy.PropertyInfo.GetCustomAttribute<ForViewConditionAttribute>();
            if (attr != null)
            {
                var inst = (IForViewCondition<TView, TContext>)Activator.CreateInstance(attr.ConditionType);
                if (forceSelectBuild || inst.CanBuild(context))
                {
                    return inst.Create(context);
                }
            }
            foreach (var item in builder.OrderByDescending(x => x.Order))
            {
                if (item.CanBuild(context))
                {
                    return item.Create(context);
                }
            }
            return default;
        }
    }
}
