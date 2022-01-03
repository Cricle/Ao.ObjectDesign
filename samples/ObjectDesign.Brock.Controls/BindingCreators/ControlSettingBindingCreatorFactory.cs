﻿using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.WpfDesign;
using Ao.ObjectDesign.Bindings;
using Ao.ObjectDesign.Session.BindingCreators;
using ObjectDesign.Brock.Components;
using System.Collections.Generic;
using System.Windows;
using Ao.ObjectDesign.Wpf.Data;

namespace ObjectDesign.Brock.Controls.BindingCreators
{
    
    public class ControlSettingBindingCreatorFactory : BindingCreatorFactory<UIElementSetting>
    {

        public override bool IsAccept(IDesignPair<UIElement, UIElementSetting> unit, IBindingCreatorState state)
        {
            return unit.DesigningObject.GetType() == typeof(ControlSetting);
        }

        protected override IEnumerable<IWpfBindingCreator<UIElementSetting>> CreateWpfCreators(IDesignPair<UIElement, UIElementSetting> unit, IBindingCreatorState state)
        {
            yield return new ControlSettingBindingCreator(unit, state);
        }
    }
}