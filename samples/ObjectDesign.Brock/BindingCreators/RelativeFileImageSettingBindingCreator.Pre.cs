using Ao.ObjectDesign.Wpf.Data;
using ObjectDesign.Brock.Components;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ObjectDesign.Brock.BindingCreators
{
    public partial class RelativeFileImageSettingBindingCreator
    {
        public static readonly IReadOnlyList<IBindingScope> RelativeFileImageSettingTwoWayScopes =
            CreateBindingScope(BindingMode.TwoWay, UpdateSourceTrigger.Default).ToArray();
        public static readonly IReadOnlyList<IBindingScope> RelativeFileImageSettingOneWayScopes =
            CreateBindingScope(BindingMode.TwoWay, UpdateSourceTrigger.Default).ToArray();

        private static IEnumerable<IBindingScope> CreateBindingScope(BindingMode mode, UpdateSourceTrigger trigger)
        {
            yield return Image.StretchProperty.Creator(nameof(RelativeFileImageSetting.Stretch))
                .AddSetConfig(mode, trigger)
                .Build();
            yield return Image.StretchDirectionProperty.Creator(nameof(RelativeFileImageSetting.StretchDirection))
                .AddSetConfig(mode, trigger)
                .Build();
        }

        public static void SetRelativeFileImageSettingToUI(RelativeFileImageSetting obj, DependencyObject ui)
        {
            ui.SetValue(Image.StretchProperty, obj.Stretch);
            ui.SetValue(Image.StretchDirectionProperty, obj.StretchDirection);
        }
    }
}
