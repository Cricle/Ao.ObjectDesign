using System;

namespace Ao.ObjectDesign
{
    public static class PropertyProxyExtensions
    {
        public static PropertyVisitor CreateVisitor(this IPropertyProxy propertyProxy)
        {
            if (propertyProxy is null)
            {
                throw new ArgumentNullException(nameof(propertyProxy));
            }

            return new PropertyVisitor(propertyProxy.DeclaringInstance, propertyProxy.PropertyInfo);
        }
    }
}