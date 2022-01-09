﻿using Ao.ObjectDesign.Bindings;
using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Wpf.Data;
using ObjectDesign.Brock.Components;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace ObjectDesign.Brock.Controls.BindingCreators
{

    public partial class RectangleSettingBindingCreator : ShapeSettingBindingCreator
    {
        public RectangleSettingBindingCreator(IDesignPair<UIElement, UIElementSetting> designUnit, IBindingCreatorState state)
            : base(designUnit, state)
        {
        }
        protected override void SetToUI()
        {
            base.SetToUI();
            SetRectangleSettingToUI((RectangleSetting)DesignUnit.DesigningObject, DesignUnit.UI);
        }
        protected override IEnumerable<IWithSourceBindingScope> GenerateBindings()
        {
            return base.GenerateBindings().Concat(RectangleSettingTwoWayScopes
                        .Where(BindingCondition)
                        .Select(x => x.ToWithSource(DesignUnit.DesigningObject)));
        }
    }
}
