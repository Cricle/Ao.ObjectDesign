using Ao.ObjectDesign.Wpf.Data;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ObjectDesign.Brock.Controls.BindingCreators
{
    public partial class ProgressBarSettingBindingCreator
    {
        public static readonly IReadOnlyList<IBindingScope> ProgressBarSettingTwoWayScopes =
            CreateBindingScope(BindingMode.TwoWay, UpdateSourceTrigger.Default).ToArray();
        public static readonly IReadOnlyList<IBindingScope> ProgressBarSettingOneWayScopes =
            CreateBindingScope(BindingMode.TwoWay, UpdateSourceTrigger.Default).ToArray();

        private static IEnumerable<IBindingScope> CreateBindingScope(BindingMode mode, UpdateSourceTrigger trigger)
        {
            yield return ProgressBar.IsIndeterminateProperty.Creator(nameof(ProgressBarSetting.IsIndeterminate))
                .AddSetConfig(mode, trigger)
                .Build();
            yield return ProgressBar.OrientationProperty.Creator(nameof(ProgressBarSetting.Orientation))
                .AddSetConfig(mode, trigger)
                .Build();
        }

        public static void SetProgressBarSettingToUI(ProgressBarSetting obj, DependencyObject ui)
        {
            ui.SetValue(ProgressBar.IsIndeterminateProperty, obj.IsIndeterminate);
            ui.SetValue(ProgressBar.OrientationProperty, obj.Orientation);
        }
    }
}
