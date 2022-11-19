using Ao.ObjectDesign.Designing.Annotations;
using Ao.ObjectDesign.Designing;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Ao.ObjectDesign.Controls
{
    [MappingFor(typeof(RadioButton))]
    public class RadioButtonSetting : ToggleButtonSetting, IMiddlewareDesigner<RadioButton>
    {
        private string groupName;

        [DefaultValue(null)]
        public virtual string GroupName
        {
            get => groupName;
            set => Set(ref groupName, value);
        }
        public override void SetDefault()
        {
            base.SetDefault();
            GroupName = null;
        }
        public void Apply(RadioButton value)
        {
            if (value is null)
            {
                SetDefault();
            }
            else
            {
                Apply((ToggleButton)value);
                GroupName = value.GroupName;
            }
        }

        public void WriteTo(RadioButton value)
        {
            if (value != null)
            {
                WriteTo((ToggleButton)value);
                value.GroupName = groupName;
            }
        }
    }
}
