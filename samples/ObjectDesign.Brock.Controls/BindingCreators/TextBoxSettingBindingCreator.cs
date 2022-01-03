using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Wpf.Data;
using Ao.ObjectDesign.WpfDesign;
using ObjectDesign.Brock.Components;
using Ao.ObjectDesign.Bindings;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ObjectDesign.Brock.Controls.BindingCreators
{
    
    public partial class TextBoxSettingBindingCreator : TextBoxBaseSettingBindingCreator
    {
        public TextBoxSettingBindingCreator(IDesignPair<UIElement, UIElementSetting> designUnit, IBindingCreatorState state)
               : base(designUnit, state)
        {
        }

        protected override void SetToUI()
        {
            base.SetToUI();
            SetTextBoxSettingToUI((TextBoxSetting)DesignUnit.DesigningObject, DesignUnit.UI);
        }
        protected override IEnumerable<IWithSourceBindingScope> GenerateBindings()
        {
            return base.GenerateBindings().Concat(TextBoxSettingTwoWayScopes
                        .Where(BindingCondition)
                        .Select(x => x.ToWithSource(DesignUnit.DesigningObject)));
        }
    }
}
