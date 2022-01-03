using Ao.ObjectDesign.Wpf.Data;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Shapes;

namespace ObjectDesign.Brock.Controls.BindingCreators
{
    public partial class RectangleSettingBindingCreator
    {
        public static readonly IReadOnlyList<IBindingScope> RectangleSettingTwoWayScopes =
            CreateBindingScope(BindingMode.TwoWay, UpdateSourceTrigger.Default).ToArray();
        public static readonly IReadOnlyList<IBindingScope> RectangleSettingOneWayScopes =
            CreateBindingScope(BindingMode.TwoWay, UpdateSourceTrigger.Default).ToArray();

        private static IEnumerable<IBindingScope> CreateBindingScope(BindingMode mode, UpdateSourceTrigger trigger)
        {
            yield return Rectangle.RadiusXProperty.Creator(nameof(RectangleSetting.RadiusX))
                .AddSetConfig(mode, trigger)
                .Build();
            yield return Rectangle.RadiusYProperty.Creator(nameof(RectangleSetting.RadiusY))
                .AddSetConfig(mode, trigger)
                .Build();
        }

        public static void SetRectangleSettingToUI(RectangleSetting obj, DependencyObject ui)
        {
            ui.SetValue(Rectangle.RadiusXProperty, obj.RadiusX);
            ui.SetValue(Rectangle.RadiusYProperty, obj.RadiusY);
        }
    }
}
