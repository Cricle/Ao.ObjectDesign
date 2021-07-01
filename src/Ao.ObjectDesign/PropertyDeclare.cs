using System;
using System.Reflection;

namespace Ao.ObjectDesign
{
    public class PropertyDeclare : ObjectDeclaring, IPropertyDeclare
    {
        public PropertyDeclare(PropertyInfo propertyInfo)
        {
            PropertyInfo = propertyInfo ?? throw new ArgumentNullException(nameof(propertyInfo));
        }

        public PropertyInfo PropertyInfo { get; }

        public override Type Type => PropertyInfo.PropertyType;
    }
}
