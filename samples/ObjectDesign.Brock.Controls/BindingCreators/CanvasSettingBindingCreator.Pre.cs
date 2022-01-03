using Ao.ObjectDesign.Wpf.Data;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace ObjectDesign.Brock.Controls.BindingCreators
{
    //NOTE: 只放在Canvas？
    public partial class CanvasSettingBindingCreator
    {
        public static readonly IReadOnlyList<IBindingScope> CanvasSettingTwoWayScopes =
            CreateBindingScope(BindingMode.TwoWay, UpdateSourceTrigger.Default).ToArray();
        public static readonly IReadOnlyList<IBindingScope> CanvasSettingOneWayScopes =
            CreateBindingScope(BindingMode.TwoWay, UpdateSourceTrigger.Default).ToArray();

        private static IEnumerable<IBindingScope> CreateBindingScope(BindingMode mode, UpdateSourceTrigger trigger)
        {
            yield return RenderOptions.BitmapScalingModeProperty.Creator(nameof(CanvasSetting.BitmapScalingMode))
                .AddSetConfig(mode, trigger)
                .Build();
            yield return RenderOptions.EdgeModeProperty.Creator(nameof(CanvasSetting.EdgeMode))
                .AddSetConfig(mode, trigger)
                .Build();
        }

        public static void SetCanvasSettingToUI(CanvasSetting obj, DependencyObject ui)
        {
            ui.SetValue(RenderOptions.BitmapScalingModeProperty, obj.BitmapScalingMode);
            ui.SetValue(RenderOptions.EdgeModeProperty, obj.EdgeMode);
        }
    }
}
