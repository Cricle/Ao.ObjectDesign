using Ao.ObjectDesign.Bindings;
using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Data;
using ObjectDesign.Brock.Components;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace ObjectDesign.Brock.Controls.BindingCreators
{

    public partial class TextBoxBaseSettingBindingCreator : BrockAutoBindingCreator<TextBoxBase,TextBoxBaseSetting>
    {
        public TextBoxBaseSettingBindingCreator(IDesignPair<UIElement, UIElementSetting> designUnit, IBindingCreatorState state)
            : base(designUnit, state)
        {
        }

    }
}
