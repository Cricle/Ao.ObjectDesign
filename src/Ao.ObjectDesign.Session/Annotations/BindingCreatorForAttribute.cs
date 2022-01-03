using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Session.Annotations
{
    [AttributeUsage(AttributeTargets.Class,AllowMultiple =false,Inherited =false)]
    public class BindingCreatorForAttribute:Attribute
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
