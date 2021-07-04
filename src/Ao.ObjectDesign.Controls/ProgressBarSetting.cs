using Ao.ObjectDesign.Wpf.Annotations;
using Ao.ObjectDesign.Wpf.Designing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Ao.ObjectDesign.Controls
{
    [MappingFor(typeof(ProgressBar))]
    public class ProgressBarSetting : RangeBaseSetting, IMiddlewareDesigner<ProgressBar>
    {
        private bool isIndeterminate;
        private Orientation orientation;

        public virtual bool IsIndeterminate
        {
            get => isIndeterminate;
            set => Set(ref isIndeterminate, value);
        }

        public virtual Orientation Orientation
        {
            get => orientation;
            set => Set(ref orientation, value);
        }
        public override void SetDefault()
        {
            base.SetDefault();
            IsIndeterminate = false;
            Orientation = Orientation.Horizontal;
        }

        public void Apply(ProgressBar value)
        {
            if (value is null)
            {
                SetDefault();
            }
            else
            {
                Apply((RangeBase)value);
                IsIndeterminate = value.IsIndeterminate;
                Orientation = value.Orientation;
            }
        }

        public void WriteTo(ProgressBar value)
        {
            if (value != null)
            {
                WriteTo((RangeBase)value);
                value.IsIndeterminate = IsIndeterminate;
                value.Orientation = Orientation;
            }
        }
    }
}
