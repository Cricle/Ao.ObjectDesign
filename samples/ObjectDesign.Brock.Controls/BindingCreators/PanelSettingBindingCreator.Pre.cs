using Ao.ObjectDesign.Wpf.Data;
using Ao.ObjectDesign.Wpf.Designing;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ObjectDesign.Brock.Controls.BindingCreators
{
    public partial class PanelSettingBindingCreator
    {
        public static readonly IReadOnlyList<IBindingScope> PanelSettingTwoWayScopes =
            CreateBindingScope(BindingMode.TwoWay, UpdateSourceTrigger.Default).ToArray();
        public static readonly IReadOnlyList<IBindingScope> PanelSettingOneWayScopes =
            CreateBindingScope(BindingMode.TwoWay, UpdateSourceTrigger.Default).ToArray();

        private static IEnumerable<IBindingScope> CreateBindingScope(BindingMode mode, UpdateSourceTrigger trigger)
        {
            yield return Panel.BackgroundProperty.Creator(nameof(PanelSetting.Background) + ".Brush")
                .AddSetConfig(mode, trigger)
                .Build();
        }

        public static void SetPanelSettingToUI(PanelSetting obj, DependencyObject ui)
        {
            if (obj.Background != null)
            {
                ui.SetValue(Panel.BackgroundProperty, obj.Background.GetBrush());
            }
        }
    }
}
