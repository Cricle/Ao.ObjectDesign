using Ao.ObjectDesign;
using Ao.ObjectDesign.Bindings;
using Ao.ObjectDesign.Designing.Annotations;
using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Wpf.Data;
using Ao.ObjectDesign.WpfDesign;
using ObjectDesign.Brock.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace ObjectDesign.Brock.Controls.BindingCreators
{
    public partial class CanvasSettingBindingCreator : BrockAutoBindingCreator<Canvas, CanvasSetting>
    {
        public CanvasSettingBindingCreator(IDesignPair<UIElement, UIElementSetting> designUnit, IBindingCreatorState state)
            : base(designUnit, state)
        {
        }
    }
}
