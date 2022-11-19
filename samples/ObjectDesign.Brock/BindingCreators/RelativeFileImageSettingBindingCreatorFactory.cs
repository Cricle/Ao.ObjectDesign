using Ao.ObjectDesign.Bindings;
using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Session.Wpf.BindingCreators;
using Ao.ObjectDesign.WpfDesign;
using ObjectDesign.Brock.Components;
using ObjectDesign.Brock.Controls.BindingCreators;
using System.Collections.Generic;
using System.Windows;

namespace ObjectDesign.Brock.BindingCreators
{
    public class RelativeFileImageSettingBindingCreatorFactory : BindingCreatorFactory<UIElementSetting>
    {
        protected override IEnumerable<IWpfBindingCreator<UIElementSetting>> CreateWpfCreators(IDesignPair<UIElement, UIElementSetting> unit, IBindingCreatorState state)
        {
            yield return new FrameworkElementSettingBindingCreator(unit, state);
            yield return new RelativeFileImageSettingBindingCreator(unit, state);
        }

        public override bool IsAccept(IDesignPair<UIElement, UIElementSetting> unit, IBindingCreatorState state)
        {
            return unit.DesigningObject.GetType() == typeof(RelativeFileImageSetting);
        }
    }
}
