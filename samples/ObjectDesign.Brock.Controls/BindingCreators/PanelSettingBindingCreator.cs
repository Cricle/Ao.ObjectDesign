﻿using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Wpf.Data;
using Ao.ObjectDesign.WpfDesign;
using Ao.ObjectDesign.Bindings;
using Ao.ObjectDesign.Session.BindingCreators;
using ObjectDesign.Brock.Components;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ObjectDesign.Brock.Controls.BindingCreators
{
    
    public partial class PanelSettingBindingCreator : FrameworkElementSettingBindingCreator
    {
        public PanelSettingBindingCreator(IDesignPair<UIElement, UIElementSetting> designUnit, IBindingCreatorState state) : base(designUnit, state)
        {
        }
        protected override void SetToUI()
        {
            base.SetToUI();
            SetPanelSettingToUI((PanelSetting)DesignUnit.DesigningObject, DesignUnit.UI);
        }
        protected override IEnumerable<IWithSourceBindingScope> GenerateBindings()
        {
            return base.GenerateBindings().Concat(PanelSettingTwoWayScopes
                        .Where(BindingCondition)
                        .Select(x => x.ToWithSource(DesignUnit.DesigningObject)));
        }
    }
}