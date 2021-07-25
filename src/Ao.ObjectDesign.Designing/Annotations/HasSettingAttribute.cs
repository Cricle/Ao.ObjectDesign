using System;

namespace Ao.ObjectDesign.Designing.Annotations
{
    [AttributeUsage(AttributeTargets.Class)]
    public class HasSettingAttribute : Attribute
    {
        public HasSettingAttribute(Type settingType)
        {
            SettingType = settingType ?? throw new ArgumentNullException(nameof(settingType));
        }

        public Type SettingType { get; }
    }
}
