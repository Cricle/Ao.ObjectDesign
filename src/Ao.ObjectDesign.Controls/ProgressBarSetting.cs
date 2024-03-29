﻿using Ao.ObjectDesign.Designing.Annotations;
using Ao.ObjectDesign.Wpf.Designing;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Ao.ObjectDesign.Controls
{
    [MappingFor(typeof(ProgressBar))]
    public class ProgressBarSetting : RangeBaseSetting, IMiddlewareDesigner<ProgressBar>
    {
        private bool isIndeterminate;
        private Orientation orientation;

        [DefaultValue(false)]
        public virtual bool IsIndeterminate
        {
            get => isIndeterminate;
            set => Set(ref isIndeterminate, value);
        }

        [DefaultValue(Orientation.Horizontal)]
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
