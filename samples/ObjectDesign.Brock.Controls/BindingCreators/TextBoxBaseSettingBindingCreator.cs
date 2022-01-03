using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Wpf.Data;
using Ao.ObjectDesign.WpfDesign;
using Ao.ObjectDesign.Bindings;
using ObjectDesign.Brock.Components;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace ObjectDesign.Brock.Controls.BindingCreators
{
    
    public partial class TextBoxBaseSettingBindingCreator: ControlSettingBindingCreator
    {
        public TextBoxBaseSettingBindingCreator(IDesignPair<UIElement, UIElementSetting> designUnit, IBindingCreatorState state)
            : base(designUnit, state)
        {
        }

        protected override void SetToUI()
        {
            base.SetToUI();
            SetTextBoxBaseSettingToUI((TextBoxBaseSetting)DesignUnit.DesigningObject, DesignUnit.UI);
        }
        protected override IEnumerable<IWithSourceBindingScope> GenerateBindings()
        {
            return base.GenerateBindings().Concat(TextBoxBaseSettingTwoWayScopes
                        .Where(BindingCondition)
                        .Select(x => x.ToWithSource(DesignUnit.DesigningObject)));
        }
    }
}
