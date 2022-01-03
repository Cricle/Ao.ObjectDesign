using Ao.ObjectDesign.Wpf.Data;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ObjectDesign.Brock.Controls.BindingCreators
{
    public partial class TextBoxSettingBindingCreator
    {
        public static readonly IReadOnlyList<IBindingScope> TextBoxSettingTwoWayScopes =
            CreateBindingScope(BindingMode.TwoWay, UpdateSourceTrigger.Default).ToArray();
        public static readonly IReadOnlyList<IBindingScope> TextBoxSettingOneWayScopes =
            CreateBindingScope(BindingMode.TwoWay, UpdateSourceTrigger.Default).ToArray();

        private static IEnumerable<IBindingScope> CreateBindingScope(BindingMode mode, UpdateSourceTrigger trigger)
        {
            yield return TextBox.TextProperty.Creator(nameof(TextBoxSetting.Text))
                .AddSetConfig(mode, trigger)
                .Build();
            yield return TextBox.TextAlignmentProperty.Creator(nameof(TextBoxSetting.TextAlignment))
                .AddSetConfig(mode, trigger)
                .Build();
            yield return TextBox.TextWrappingProperty.Creator(nameof(TextBoxSetting.TextWrapping))
                .AddSetConfig(mode, trigger)
                .Build();
        }

        public static void SetTextBoxSettingToUI(TextBoxSetting obj, DependencyObject ui)
        {
            ui.SetValue(TextBox.TextProperty, obj.Text);
            ui.SetValue(TextBox.TextAlignmentProperty, obj.TextAlignment);
            ui.SetValue(TextBox.TextWrappingProperty, obj.TextWrapping);
        }
    }
}
