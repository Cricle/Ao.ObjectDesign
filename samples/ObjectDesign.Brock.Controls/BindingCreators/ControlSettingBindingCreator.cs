using Ao.ObjectDesign.Bindings;
using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Wpf.Data;
using ObjectDesign.Brock.Components;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace ObjectDesign.Brock.Controls.BindingCreators
{

    public partial class ControlSettingBindingCreator : FrameworkElementSettingBindingCreator
    {
        public ControlSettingBindingCreator(IDesignPair<UIElement, UIElementSetting> designUnit, IBindingCreatorState state)
            : base(designUnit, state)
        {
        }
        protected override void SetToUI()
        {
            base.SetToUI();
            SetControlSettingToUI((ControlSetting)DesignUnit.DesigningObject, DesignUnit.UI);
        }
        protected override IEnumerable<IWithSourceBindingScope> GenerateBindings()
        {
            return base.GenerateBindings().Concat(ControlSettingTwoWayScopes
                        .Where(BindingCondition)
                        .Select(x => x.ToWithSource(DesignUnit.DesigningObject)));
        }
    }
}
