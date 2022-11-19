using System;

namespace Ao.ObjectDesign.Data
{
    public static class BindingScopeWithSourceExtensions
    {
        public static IWithSourceBindingScope ToWithSource(this IBindingScope scope)
        {
            if (scope is null)
            {
                throw new ArgumentNullException(nameof(scope));
            }

            return new WithSourceBindingScope(scope);
        }
        public static IWithSourceBindingScope ToWithSource(this IBindingScope scope, object source)
        {
            if (scope is null)
            {
                throw new ArgumentNullException(nameof(scope));
            }

            return new WithSourceBindingScope(scope, source);
        }
    }
}
