using System;

namespace Ao.ObjectDesign.Designing
{
    public static class InitableObjectThrowExtensions
    {
        public static void ThrowIfNoInitialized(this IInitableObject @object)
        {
            if (@object is null)
            {
                throw new ArgumentNullException(nameof(@object));
            }

            if (!@object.IsInitialized)
            {
                throw new InvalidOperationException($"Can't operator, must be initialized");
            }
        }
    }
}
