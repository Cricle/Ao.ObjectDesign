using Ao.ObjectDesign.Designing.Annotations;
using Ao.ObjectDesign.Designing;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Ao.ObjectDesign.Controls
{
    [MappingFor(typeof(Button))]
    public class ButtonSetting : ButtonBaseSetting, IMiddlewareDesigner<Button>
    {
        private bool isDefault;
        private bool isCancel;

        [DefaultValue(false)]
        public virtual bool IsDefault
        {
            get => isDefault;
            set => Set(ref isDefault, value);
        }

        [DefaultValue(false)]
        public virtual bool IsCancel
        {
            get => isCancel;
            set => Set(ref isCancel, value);
        }

        public override void SetDefault()
        {
            base.SetDefault();
            IsDefault = false;
            IsCancel = false;
        }

        public void Apply(Button value)
        {
            if (value is null)
            {
                SetDefault();
            }
            else
            {
                Apply((ButtonBase)value);
                IsDefault = value.IsDefault;
                IsCancel = value.IsCancel;
            }
        }

        public void WriteTo(Button value)
        {
            if (value != null)
            {
                WriteTo((ButtonBase)value);
                value.IsDefault = isDefault;
                value.IsCancel = isCancel;
            }
        }
    }
}
