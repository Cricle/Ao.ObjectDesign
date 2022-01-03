using Ao.ObjectDesign.Wpf.Data;
using ObjectDesign.Brock.Components;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ObjectDesign.Brock.Controls.BindingCreators
{
    public partial class FrameworkElementSettingBindingCreator
    {
        public static readonly IReadOnlyList<IBindingScope> FrameworkElementSettingTwoWayScopes =
               CreateBindingScope(BindingMode.TwoWay, UpdateSourceTrigger.Default).ToArray();
        public static readonly IReadOnlyList<IBindingScope> FrameworkElementSettingOneWayScopes =
               CreateBindingScope(BindingMode.OneWay, UpdateSourceTrigger.Default).ToArray();

        private static IEnumerable<IBindingScope> CreateBindingScope(BindingMode mode, UpdateSourceTrigger trigger)
        {
            yield return FrameworkElement.NameProperty.Creator(nameof(FrameworkElementSetting.Name))
                .AddSetConfig(mode, trigger)
                .Build();
            yield return Canvas.LeftProperty.Creator(nameof(FrameworkElementSetting.PositionSize) + "." + nameof(PositionSize.X))
                .AddSetConfig(mode, trigger)
                .Build();
            yield return Canvas.TopProperty.Creator(nameof(FrameworkElementSetting.PositionSize) + "." + nameof(PositionSize.Y))
                .AddSetConfig(mode, trigger)
                .Build();
            yield return FrameworkElement.WidthProperty.Creator(nameof(FrameworkElementSetting.PositionSize) + "." + nameof(PositionSize.Width))
                .AddSetConfig(mode, trigger)
                .Build();
            yield return FrameworkElement.HeightProperty.Creator(nameof(FrameworkElementSetting.PositionSize) + "." + nameof(PositionSize.Height))
                .AddSetConfig(mode, trigger)
                .Build();
            yield return FrameworkElement.VerticalAlignmentProperty.Creator(nameof(FrameworkElementSetting.VerticalAlignment))
                .AddSetConfig(mode, trigger)
                .Build();
            yield return FrameworkElement.HorizontalAlignmentProperty.Creator(nameof(FrameworkElementSetting.HorizontalAlignment))
                .AddSetConfig(mode, trigger)
                .Build();
            yield return FrameworkElement.UseLayoutRoundingProperty.Creator(nameof(FrameworkElementSetting.UseLayoutRounding))
                .AddSetConfig(mode, trigger)
                .Build();
        }
        private static void SetFrameworkElementSettingToUI(FrameworkElementSetting obj, DependencyObject ui)
        {
            if (!string.IsNullOrEmpty(obj.Name))
            {
                ui.SetValue(FrameworkElement.NameProperty, obj.Name);
            }
            var ps = obj.PositionSize;
            if (ps != null)
            {
                ui.SetValue(Canvas.LeftProperty, ps.X);
                ui.SetValue(Canvas.TopProperty, ps.Y);
                ui.SetValue(FrameworkElement.WidthProperty, ps.Width);
                ui.SetValue(FrameworkElement.HeightProperty, ps.Height);
            }
            ui.SetValue(FrameworkElement.HorizontalAlignmentProperty, obj.HorizontalAlignment);
            ui.SetValue(FrameworkElement.VerticalAlignmentProperty, obj.VerticalAlignment);
            ui.SetValue(FrameworkElement.UseLayoutRoundingProperty, obj.UseLayoutRounding);
        }
    }
}
