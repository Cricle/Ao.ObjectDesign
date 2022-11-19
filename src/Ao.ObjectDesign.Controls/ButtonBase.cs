using Ao.ObjectDesign.Designing.Annotations;
using Ao.ObjectDesign.Designing;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Ao.ObjectDesign.Controls
{
    [MappingFor(typeof(ButtonBase))]
    public abstract class ButtonBaseSetting : ControlSetting, IMiddlewareDesigner<ButtonBase>
    {
        private ClickMode clickMode;

        [DefaultValue(ClickMode.Release)]
        public virtual ClickMode ClickMode
        {
            get => clickMode;
            set => Set(ref clickMode, value);
        }
        public override void SetDefault()
        {
            base.SetDefault();
            ClickMode = ClickMode.Release;
        }

        public void Apply(ButtonBase value)
        {
            if (value is null)
            {
                SetDefault();
            }
            else
            {
                Apply((FrameworkElement)value);
                ClickMode = value.ClickMode;
            }
        }

        public void WriteTo(ButtonBase value)
        {
            if (value != null)
            {
                WriteTo((FrameworkElement)value);
                value.ClickMode = clickMode;
            }
        }
    }
}
