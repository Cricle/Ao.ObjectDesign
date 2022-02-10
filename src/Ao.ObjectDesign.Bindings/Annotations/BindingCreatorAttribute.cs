using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Bindings.Annotations
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public sealed class BindingCreatorAttribute : Attribute
    {
        public BindingCreatorAttribute(Type settingType)
        {
            SettingType = settingType ?? throw new ArgumentNullException(nameof(settingType));
        }

        public BindingCreatorAttribute(Type uIType, Type settingType) : this(settingType)
        {
            UIType = uIType ?? throw new ArgumentNullException();
        }

        public Type UIType { get; }

        public Type SettingType { get; }
    }
}
