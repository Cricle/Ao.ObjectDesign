using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Ao.ObjectDesign.Designing
{
    public static class DesignOrderManagerAddExtensions
    {
        public static DesignOrderManager Add<T>(this DesignOrderManager mgr, Expression<Func<T, object>> selector, int order)
        {
            if (mgr is null)
            {
                throw new ArgumentNullException(nameof(mgr));
            }

            if (selector is null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            if (selector.Body is MemberExpression me &&
                me.Member is PropertyInfo propInfo)
            {
                var identity = new PropertyIdentity(propInfo.DeclaringType, propInfo.Name);
                mgr.Add(identity, order);
                return mgr;
            }
            else if (selector.Body is UnaryExpression ue &&
                ue.Operand is MemberExpression ueme &&
                ueme.Member is PropertyInfo mpi)
            {
                var identity = new PropertyIdentity(mpi.DeclaringType, mpi.Name);
                mgr.Add(identity, order);
                return mgr;
            }
            throw new NotSupportedException(selector.Body.ToString());

        }
        public static DesignOrderManager Add<T>(this DesignOrderManager mgr, Expression<Func<T, object>> selector)
        {
            return Add(mgr, selector, mgr.Count + 1);
        }
    }
}
