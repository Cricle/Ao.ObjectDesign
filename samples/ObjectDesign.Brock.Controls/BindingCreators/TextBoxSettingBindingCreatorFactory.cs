using Ao.ObjectDesign.Bindings;
using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Session.Wpf.BindingCreators;
using Ao.ObjectDesign.WpfDesign;
using ObjectDesign.Brock.Components;
using System.Collections.Generic;
using System.Windows;

namespace ObjectDesign.Brock.Controls.BindingCreators
{

    public class TextBoxSettingBindingCreatorFactory : BindingCreatorFactory<UIElementSetting>
    {
        protected override IEnumerable<IWpfBindingCreator<UIElementSetting>> CreateWpfCreators(IDesignPair<UIElement, UIElementSetting> unit, IBindingCreatorState state)
        {
            yield return new FrameworkElementSettingBindingCreator(unit, state);
            yield return new ControlSettingBindingCreator(unit, state);
            yield return new TextBoxBaseSettingBindingCreator(unit, state);
            yield return new TextBoxSettingBindingCreator(unit, state);
        }
        public override bool IsAccept(IDesignPair<UIElement, UIElementSetting> unit, IBindingCreatorState state)
        {
            return unit.DesigningObject.GetType() == typeof(TextBoxSetting);
        }
    }
}
