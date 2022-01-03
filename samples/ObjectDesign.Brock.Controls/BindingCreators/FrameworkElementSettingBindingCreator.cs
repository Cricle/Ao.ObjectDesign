using Ao.ObjectDesign.Bindings;
using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Session.Annotations;
using Ao.ObjectDesign.Wpf.Data;
using Ao.ObjectDesign.WpfDesign;
using ObjectDesign.Brock.Components;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace ObjectDesign.Brock.Controls.BindingCreators
{
    
    public partial class FrameworkElementSettingBindingCreator : UIElementSettingBindingCreator
    {
        public FrameworkElementSettingBindingCreator(IDesignPair<UIElement, UIElementSetting> designUnit, IBindingCreatorState state)
            : base(designUnit, state)
        {
        }
        protected override void SetToUI()
        {
            base.SetToUI();
            SetFrameworkElementSettingToUI((FrameworkElementSetting)DesignUnit.DesigningObject, DesignUnit.UI);
        }
        protected override IEnumerable<IWithSourceBindingScope> GenerateBindings()
        {
            return base.GenerateBindings()
                .Concat(FrameworkElementSettingTwoWayScopes
                .Where(BindingCondition)
                .Select(x => x.ToWithSource(DesignUnit.DesigningObject)));
        }
    }
}
