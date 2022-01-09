using Ao.ObjectDesign.Wpf.Data;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace ObjectDesign.Brock.Controls.BindingCreators
{
    public partial class ControlSettingBindingCreator
    {
        public static readonly IReadOnlyList<IBindingScope> ControlSettingTwoWayScopes =
            CreateBindingScope(BindingMode.TwoWay, UpdateSourceTrigger.Default).ToArray();
        public static readonly IReadOnlyList<IBindingScope> ControlSettingOneWayScopes =
            CreateBindingScope(BindingMode.TwoWay, UpdateSourceTrigger.Default).ToArray();

        private static IEnumerable<IBindingScope> CreateBindingScope(BindingMode mode, UpdateSourceTrigger trigger)
        {
            yield return Control.VerticalContentAlignmentProperty.Creator(nameof(ControlSetting.VerticalContentAlignment))
                .AddSetConfig(mode, trigger)
                .Build();
            yield return Control.HorizontalContentAlignmentProperty.Creator(nameof(ControlSetting.HorizontalContentAlignment))
                .AddSetConfig(mode, trigger)
                .Build();
            yield return Control.BackgroundProperty.Creator(nameof(ControlSetting.Background) + ".Brush")
                .AddSetConfig(mode, trigger)
                .Build();
            yield return Control.ForegroundProperty.Creator(nameof(ControlSetting.Foreground) + ".Brush")
                .AddSetConfig(mode, trigger)
                .Build();
            yield return Control.FontSizeProperty.Creator(nameof(ControlSetting.FontSize))
                .AddSetConfig(mode, trigger)
                .Build();
            yield return Control.FontFamilyProperty.Creator(nameof(ControlSetting.FontFamily) + ".FontFamily")
                .AddSetConfig(mode, trigger)
                .Build();
            yield return Control.BorderBrushProperty.Creator(nameof(ControlSetting.BorderBrush) + ".Brush")
                .AddSetConfig(mode, trigger)
                .Build();
            yield return Control.PaddingProperty.Creator(nameof(ControlSetting.Padding) + ".Thickness")
                .AddSetConfig(mode, trigger)
                .Build();
            yield return Control.BorderThicknessProperty.Creator(nameof(ControlSetting.BorderThickness) + ".Thickness")
                .AddSetConfig(mode, trigger)
                .Build();
            yield return Control.BorderThicknessProperty.Creator(nameof(ControlSetting.BorderThickness) + ".Thickness")
                .AddSetConfig(mode, trigger)
                .Build();
            yield return Control.FontStyleProperty.Creator(nameof(ControlSetting.FontStyle) + ".FontStyle")
                .AddSetConfig(mode, trigger)
                .Build();
            yield return Control.FontStretchProperty.Creator(nameof(ControlSetting.FontStretch) + ".FontStretch")
                .AddSetConfig(mode, trigger)
                .Build();
            yield return Control.FontWeightProperty.Creator(nameof(ControlSetting.FontWeight) + ".FontWeight")
                .AddSetConfig(mode, trigger)
                .Build();
        }

        public static void SetControlSettingToUI(ControlSetting obj, DependencyObject ui)
        {
            ui.SetValue(Control.HorizontalContentAlignmentProperty, obj.HorizontalContentAlignment);
            ui.SetValue(Control.VerticalContentAlignmentProperty, obj.VerticalContentAlignment);
            ui.SetValue(Control.BackgroundProperty, obj.Background?.GetBrush());
            ui.SetValue(Control.ForegroundProperty, obj.Foreground?.GetBrush());
            ui.SetValue(Control.FontSizeProperty, obj.FontSize);
            ui.SetValue(Control.FontFamilyProperty, obj.FontFamily?.GetFontFamily() ?? new FontFamily());
            ui.SetValue(Control.BorderBrushProperty, obj.BorderBrush?.GetBrush());
            ui.SetValue(Control.PaddingProperty, obj.Padding?.GetThickness() ?? default(Thickness));
            ui.SetValue(Control.BorderThicknessProperty, obj.BorderThickness?.GetThickness() ?? default(Thickness));
            ui.SetValue(Control.FontStyleProperty, obj.FontStyle?.GetFontStyle() ?? new FontStyle());
            ui.SetValue(Control.FontStretchProperty, obj.FontStretch?.GetFontStretch() ?? new FontStretch());
            ui.SetValue(Control.FontWeightProperty, obj.FontWeight?.GetFontWeight() ?? new FontWeight());
        }
    }
}
