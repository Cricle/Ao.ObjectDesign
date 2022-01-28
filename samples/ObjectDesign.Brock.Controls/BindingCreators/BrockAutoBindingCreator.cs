using Ao.ObjectDesign.AutoBind;
using Ao.ObjectDesign.Bindings;
using Ao.ObjectDesign.Designing.Level;
using ObjectDesign.Brock.Components;
using System.Windows;

namespace ObjectDesign.Brock.Controls.BindingCreators
{
    public class BrockAutoBindingCreator<TUI, TDesignObject> : AutoBindingCreator<TUI, UIElementSetting, TDesignObject>
        where TUI : DependencyObject
    {
        public BrockAutoBindingCreator(IDesignPair<UIElement, UIElementSetting> designUnit, IBindingCreatorState state) : base(designUnit, state)
        {
        }
    }
}
