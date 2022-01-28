using Ao.ObjectDesign.Bindings;
using Ao.ObjectDesign.Designing.Level;
using ObjectDesign.Brock.Components;
using System.Windows;

namespace ObjectDesign.Brock.Controls.BindingCreators
{
    public partial class UIElementSettingBindingCreator : BrockAutoBindingCreator<UIElement,UIElementSetting>
    {
        public UIElementSettingBindingCreator(IDesignPair<UIElement, UIElementSetting> designUnit, IBindingCreatorState state)
            : base(designUnit, state)
        {
        }
    }
}
