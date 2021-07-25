using System.Reflection;

namespace Ao.ObjectDesign.Designing
{
    public class SpecularMappingItem
    {
        public SpecularMappingItem(PropertyInfo sourceProperty,
            PropertyInfo targetProperty,
            object sourceValue,
            bool succeed)
        {
            SourceProperty = sourceProperty;
            TargetProperty = targetProperty;
            SourceValue = sourceValue;
            Succeed = succeed;
        }

        public PropertyInfo SourceProperty { get; }

        public PropertyInfo TargetProperty { get; }

        public object SourceValue { get; }

        public bool Succeed { get; }

        public override string ToString()
        {
            return $"{{PropertyName:{SourceProperty.Name}, Suceed:{Succeed}}}";
        }
    }
}
