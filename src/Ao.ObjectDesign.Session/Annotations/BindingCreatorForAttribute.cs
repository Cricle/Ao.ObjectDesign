using System;

namespace Ao.ObjectDesign.Session.Annotations
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class BindingCreatorForAttribute : Attribute
    {
        public BindingCreatorForAttribute(Type settingType, Type uIType)
        {
            SettingType = settingType;
            UIType = uIType;
        }

        public Type SettingType { get; }

        public Type UIType { get; }
    }
}
