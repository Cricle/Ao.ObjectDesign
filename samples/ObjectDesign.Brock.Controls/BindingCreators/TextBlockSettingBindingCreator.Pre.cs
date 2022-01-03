using Ao.ObjectDesign.Wpf.Data;
using Ao.ObjectDesign.Wpf.Designing;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ObjectDesign.Brock.Controls.BindingCreators
{
    public partial class TextBlockSettingBindingCreator
    {
        public static readonly IReadOnlyList<IBindingScope> TextBlockSettingTwoWayScopes =
            CreateBindingScope(BindingMode.TwoWay, UpdateSourceTrigger.Default).ToArray();
        public static readonly IReadOnlyList<IBindingScope> TextBlockSettingOneWayScopes =
            CreateBindingScope(BindingMode.TwoWay, UpdateSourceTrigger.Default).ToArray();

        private static IEnumerable<IBindingScope> CreateBindingScope(BindingMode mode, UpdateSourceTrigger trigger)
        {
            yield return TextBlock.TextProperty.Creator(nameof(TextBlockSetting.Text))
                .AddSetConfig(mode, trigger)
                .Build();
            yield return TextBlock.BackgroundProperty.Creator(nameof(TextBlockSetting.Background) + ".Brush")
                .AddSetConfig(mode, trigger)
                .Build();
            yield return TextBlock.ForegroundProperty.Creator(nameof(TextBlockSetting.Foreground) + ".Brush")
                .AddSetConfig(mode, trigger)
                .Build();
            yield return TextBlock.TextTrimmingProperty.Creator(nameof(TextBlockSetting.TextTrimming))
                .AddSetConfig(mode, trigger)
                .Build();
            yield return TextBlock.TextWrappingProperty.Creator(nameof(TextBlockSetting.TextWrapping))
                .AddSetConfig(mode, trigger)
                .Build();
            yield return TextBlock.FontSizeProperty.Creator(nameof(TextBlockSetting.FontSize))
                .AddSetConfig(mode, trigger)
                .Build();
        }

        public static void SetTextBlockSettingToUI(TextBlockSetting obj, DependencyObject ui)
        {
            ui.SetValue(TextBlock.TextProperty, obj.Text);
            ui.SetValue(TextBlock.BackgroundProperty, obj.Background?.GetBrush());
            ui.SetValue(TextBlock.ForegroundProperty, obj.Foreground?.GetBrush());
            ui.SetValue(TextBlock.TextTrimmingProperty, obj.TextTrimming);
            ui.SetValue(TextBlock.TextWrappingProperty, obj.TextWrapping);
            ui.SetValue(TextBlock.FontSizeProperty, obj.FontSize);
        }
    }
}
