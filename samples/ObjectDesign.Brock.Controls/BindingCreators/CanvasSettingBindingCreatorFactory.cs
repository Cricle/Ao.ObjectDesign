using Ao.ObjectDesign.Bindings;
using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Session.BindingCreators;
using Ao.ObjectDesign.WpfDesign;
using ObjectDesign.Brock.Components;
using System.Collections.Generic;
using System.Windows;

namespace ObjectDesign.Brock.Controls.BindingCreators
{

    public class CanvasSettingBindingCreatorFactory : BindingCreatorFactory<UIElementSetting>
    {
        protected override IEnumerable<IWpfBindingCreator<UIElementSetting>> CreateWpfCreators(IDesignPair<UIElement, UIElementSetting> unit, IBindingCreatorState state)
        {
            yield return new CanvasSettingBindingCreator(unit, state);
        }

        public override bool IsAccept(IDesignPair<UIElement, UIElementSetting> unit, IBindingCreatorState state)
        {
            return unit.DesigningObject.GetType() == typeof(CanvasSetting);
        }
    }
}
