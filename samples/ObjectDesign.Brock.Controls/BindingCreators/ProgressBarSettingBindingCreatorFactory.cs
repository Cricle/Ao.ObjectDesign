using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.WpfDesign;
using Ao.ObjectDesign.Bindings;
using Ao.ObjectDesign.Session.BindingCreators;
using ObjectDesign.Brock.Components;
using System.Collections.Generic;
using System.Windows;

namespace ObjectDesign.Brock.Controls.BindingCreators
{
    
    public class ProgressBarSettingBindingCreatorFactory : BindingCreatorFactory<UIElementSetting>
    {
        protected override IEnumerable<IWpfBindingCreator<UIElementSetting>> CreateWpfCreators(IDesignPair<UIElement, UIElementSetting> unit, IBindingCreatorState state)
        {
            yield return new ProgressBarSettingBindingCreator(unit, state);
        }

        public override bool IsAccept(IDesignPair<UIElement, UIElementSetting> unit, IBindingCreatorState state)
        {
            return unit.DesigningObject.GetType() == typeof(ProgressBarSetting);
        }
    }
}
