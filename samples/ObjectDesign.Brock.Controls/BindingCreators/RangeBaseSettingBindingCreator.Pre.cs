using Ao.ObjectDesign.Wpf.Data;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace ObjectDesign.Brock.Controls.BindingCreators
{
    public partial class RangeBaseSettingBindingCreator
    {
        public static readonly IReadOnlyList<IBindingScope> RangeBaseSettingTwoWayScopes =
            CreateBindingScope(BindingMode.TwoWay, UpdateSourceTrigger.Default).ToArray();
        public static readonly IReadOnlyList<IBindingScope> RangeBaseSettingOneWayScopes =
            CreateBindingScope(BindingMode.TwoWay, UpdateSourceTrigger.Default).ToArray();

        private static IEnumerable<IBindingScope> CreateBindingScope(BindingMode mode, UpdateSourceTrigger trigger)
        {
            yield return RangeBase.SmallChangeProperty.Creator(nameof(RangeBaseSetting.SmallChange))
                .AddSetConfig(mode, trigger)
                .Build();
            yield return RangeBase.LargeChangeProperty.Creator(nameof(RangeBaseSetting.LargeChange))
                .AddSetConfig(mode, trigger)
                .Build();
            yield return RangeBase.MinimumProperty.Creator(nameof(RangeBaseSetting.Minimum))
                .AddSetConfig(mode, trigger)
                .Build();
            yield return RangeBase.MaximumProperty.Creator(nameof(RangeBaseSetting.Maximum))
                .AddSetConfig(mode, trigger)
                .Build();
            yield return RangeBase.ValueProperty.Creator(nameof(RangeBaseSetting.Value))
                .AddSetConfig(mode, trigger)
                .Build();
        }

        public static void SetRangeBaseSettingToUI(RangeBaseSetting obj, DependencyObject ui)
        {
            ui.SetValue(RangeBase.SmallChangeProperty, obj.SmallChange);
        }
    }
}
