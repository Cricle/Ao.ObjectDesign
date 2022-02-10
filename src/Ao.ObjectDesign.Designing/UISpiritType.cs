using System;

namespace Ao.ObjectDesign.Designing
{
    public class UISpiritType
    {
        public UISpiritType(Type uIType, Type settingType)
        {
            UIType = uIType ?? throw new ArgumentNullException(nameof(uIType));
            SettingType = settingType ?? throw new ArgumentNullException(nameof(settingType));
        }

        public Type UIType { get; }

        public Type SettingType { get; }

        public override bool Equals(object obj)
        {
            if (obj is UISpiritType pair)
            {
                return pair.SettingType == SettingType &&
                    pair.UIType == UIType;
            }
            return false;
        }
        public override int GetHashCode()
        {
            unchecked
            {
                var h = 17 * 31 + UIType.GetHashCode();
                h = h * 31 + SettingType.GetHashCode();
                return h;
            }
        }
        public override string ToString()
        {
            return $"{{UIType: {UIType}, SettingType:{SettingType}}}";
        }
    }
}
