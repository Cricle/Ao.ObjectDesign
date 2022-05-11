using System;

namespace Ao.ObjectDesign.Annotations
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class ForViewConditionAttribute : Attribute
    {
        public ForViewConditionAttribute(Type conditionType)
        {
            if (conditionType is null)
            {
                throw new ArgumentNullException(nameof(conditionType));
            }

            ConditionType = conditionType;
        }

        public Type ConditionType { get; }

        public override bool Equals(object obj)
        {
            if (obj is ForViewConditionAttribute attribute)
            {
                return attribute.ConditionType == ConditionType;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return ConditionType.GetHashCode();
        }
        public override string ToString()
        {
            return $"{{{ConditionType}}}";
        }
    }
}
