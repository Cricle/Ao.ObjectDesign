using Ao.ObjectDesign.Wpf.Data;
using Ao.ObjectDesign.Wpf.Designing;
using ObjectDesign.Brock.Components;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace ObjectDesign.Brock.Controls.BindingCreators
{
    public partial class UIElementSettingBindingCreator
    {
        public static readonly IReadOnlyList<IBindingScope> UIElementSettingTwoWayScopes =
            CreateBindingScope(BindingMode.TwoWay, UpdateSourceTrigger.Default).ToArray();
        public static readonly IReadOnlyList<IBindingScope> UIElementSettingOneWayScopes =
            CreateBindingScope(BindingMode.TwoWay, UpdateSourceTrigger.Default).ToArray();

        private static IEnumerable<IBindingScope> CreateBindingScope(BindingMode mode, UpdateSourceTrigger trigger)
        {
            yield return UIElement.IsEnabledProperty.Creator(nameof(UIElementSetting.IsEnabled))
                .AddSetConfig(mode, trigger)
                .Build();
            yield return UIElement.ClipToBoundsProperty.Creator(nameof(UIElementSetting.ClipToBounds))
                .AddSetConfig(mode, trigger)
                .Build();
            yield return UIElement.VisibilityProperty.Creator(nameof(UIElementSetting.Visibility))
                .AddSetConfig(mode, trigger)
                .Build();
            yield return UIElement.OpacityProperty.Creator(nameof(UIElementSetting.Opacity))
                .AddSetConfig(mode, trigger)
                .Build();
            yield return UIElement.RenderTransformOriginProperty.Creator(nameof(UIElementSetting.RenderTransformOrigin) + ".Point")
                .AddSetConfig(mode, trigger)
                .Build();
            yield return UIElement.RenderTransformProperty.Creator(nameof(UIElementSetting.RotateTransform) + ".RotateTransform")
                .AddSetConfig(mode, trigger)
                .Build();
        }

        public static void SetUIElementSettingToUI(UIElementSetting obj, DependencyObject ui)
        {
            ui.SetValue(UIElement.IsEnabledProperty, obj.IsEnabled);
            ui.SetValue(UIElement.ClipToBoundsProperty, obj.ClipToBounds);
            ui.SetValue(UIElement.VisibilityProperty, obj.Visibility);
            ui.SetValue(UIElement.OpacityProperty, obj.Opacity);
            ui.SetValue(UIElement.RenderTransformOriginProperty, obj.RenderTransformOrigin?.GetPoint() ?? default);
            if (obj.RotateTransform != null && obj.RotateTransform.Angle != 0)
            {
                ui.SetValue(UIElement.RenderTransformProperty, obj.RotateTransform.GetRotateTransform());
            }
        }
    }
}
