using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Wpf.Annotations
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
