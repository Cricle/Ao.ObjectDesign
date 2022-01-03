using Ao.ObjectDesign.Wpf.Data;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace ObjectDesign.Brock.Controls.BindingCreators
{
    public partial class TextBoxBaseSettingBindingCreator
    {
        public static readonly IReadOnlyList<IBindingScope> TextBoxBaseSettingTwoWayScopes =
            CreateBindingScope(BindingMode.TwoWay, UpdateSourceTrigger.Default).ToArray();
        public static readonly IReadOnlyList<IBindingScope> TextBoxBaseSettingOneWayScopes =
            CreateBindingScope(BindingMode.TwoWay, UpdateSourceTrigger.Default).ToArray();

        private static IEnumerable<IBindingScope> CreateBindingScope(BindingMode mode, UpdateSourceTrigger trigger)
        {
            yield return TextBoxBase.AcceptsReturnProperty.Creator(nameof(TextBoxBaseSetting.AcceptsReturn))
                .AddSetConfig(mode, trigger)
                .Build();
        }

        public static void SetTextBoxBaseSettingToUI(TextBoxBaseSetting obj, DependencyObject ui)
        {
            ui.SetValue(TextBoxBase.AcceptsReturnProperty, obj.AcceptsReturn);
        }
    }
}
