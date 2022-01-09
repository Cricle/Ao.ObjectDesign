using Ao.ObjectDesign.Wpf.Data;
using ObjectDesign.Brock.Controls.Designing;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ObjectDesign.Brock.Controls.BindingCreators
{
    public partial class ItemsControlSettingBindingCreator
    {
        public static readonly IReadOnlyList<IBindingScope> ItemsControlSettingTwoWayScopes =
            CreateBindingScope(BindingMode.TwoWay, UpdateSourceTrigger.Default).ToArray();
        public static readonly IReadOnlyList<IBindingScope> ItemsControlSettingOneWayScopes =
            CreateBindingScope(BindingMode.TwoWay, UpdateSourceTrigger.Default).ToArray();

        private static IEnumerable<IBindingScope> CreateBindingScope(BindingMode mode, UpdateSourceTrigger trigger)
        {
            yield return ItemsControl.IsTextSearchEnabledProperty.Creator(nameof(ItemsControlSetting.IsTextSearchEnabled))
                .AddSetConfig(mode, trigger)
                .Build();
            yield return ItemsControl.IsTextSearchCaseSensitiveProperty.Creator(nameof(ItemsControlSetting.IsTextSearchCaseSensitive))
                .AddSetConfig(mode, trigger)
                .Build();
            yield return ItemsControl.ItemsPanelProperty.Creator(nameof(ItemsControlSetting.ItemsPanel) + "." + nameof(ItemsPanelTemplateDesigning.ItemsPanelTemplate))
                .AddSetConfig(mode, trigger)
                .Build();
            yield return ItemsControl.ItemsSourceProperty.Creator(nameof(ItemsControlSetting.DesigningObjects))
                .AddSetConfig(BindingMode.OneWay, trigger)
                .Build();
        }

        public static void SetItemsControlSettingToUI(ItemsControlSetting obj, DependencyObject ui)
        {
            ui.SetValue(ItemsControl.IsTextSearchEnabledProperty, obj.IsTextSearchEnabled);
            ui.SetValue(ItemsControl.IsTextSearchCaseSensitiveProperty, obj.IsTextSearchCaseSensitive);
            ui.SetValue(ItemsControl.ItemsPanelProperty, obj.ItemsPanel?.ItemsPanelTemplate);
            ui.SetValue(ItemsControl.ItemsSourceProperty, obj.DesigningObjects);

        }
    }
}
