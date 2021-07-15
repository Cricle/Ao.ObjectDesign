﻿using Ao.ObjectDesign.Wpf.Annotations;
using Ao.ObjectDesign.Wpf.Designing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace Ao.ObjectDesign.Controls
{
    [MappingFor(typeof(ToggleButton))]
    public class ToggleButtonSetting : ButtonBaseSetting,IMiddlewareDesigner<ToggleButton>
    {
        private bool isThreeState;
        private bool? isChecked;

        public virtual bool? IsChecked
        {
            get => isChecked;
            set => Set(ref isChecked, value);
        }

        public virtual bool IsThreeState
        {
            get => isThreeState;
            set => Set(ref isThreeState, value);
        }
        public override void SetDefault()
        {
            base.SetDefault();
            IsChecked = false;
            IsThreeState = false;
        }
        public void Apply(ToggleButton value)
        {
            if (value is null)
            {
                SetDefault();
            }
            else
            {
                Apply((ButtonBase)value);
                IsChecked = value.IsChecked;
                IsThreeState = value.IsThreeState;
            }
        }

        public void WriteTo(ToggleButton value)
        {
            if (value!=null)
            {
                WriteTo((ButtonBase)value);
                value.IsChecked = isChecked;
                value.IsThreeState = isThreeState;
            }
        }
    }
}