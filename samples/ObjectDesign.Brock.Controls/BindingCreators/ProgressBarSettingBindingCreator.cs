﻿using Ao.ObjectDesign.Bindings;
using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Wpf.Data;
using ObjectDesign.Brock.Components;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ObjectDesign.Brock.Controls.BindingCreators
{

    public partial class ProgressBarSettingBindingCreator : BrockAutoBindingCreator<ProgressBar, ProgressBarSetting>
    {
        public ProgressBarSettingBindingCreator(IDesignPair<UIElement, UIElementSetting> designUnit, IBindingCreatorState state)
            : base(designUnit, state)
        {
        }
    }
}
