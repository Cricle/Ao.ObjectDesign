using Ao.ObjectDesign.Wpf.Annotations;
using Ao.ObjectDesign.Wpf.Designing;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Ao.ObjectDesign.Controls
{
    [MappingFor(typeof(CheckBox))]
    public class CheckBoxSetting : ToggleButtonSetting, IMiddlewareDesigner<CheckBox>
    {
        public void Apply(CheckBox value)
        {
            Apply((ToggleButton)value);
        }

        public void WriteTo(CheckBox value)
        {
            Apply((CheckBox)value);
        }
    }
}
