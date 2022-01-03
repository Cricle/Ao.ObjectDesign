using Ao.ObjectDesign.Wpf.Data;
using ObjectDesign.Brock.Components;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Shapes;

namespace ObjectDesign.Brock.Controls.BindingCreators
{
    public partial class LineSettingBindingCreator
    {
        public static readonly IReadOnlyList<IBindingScope> LineSettingTwoWayScopes =
            CreateBindingScope(BindingMode.TwoWay, UpdateSourceTrigger.Default).ToArray();
        public static readonly IReadOnlyList<IBindingScope> LineSettingOneWayScopes =
            CreateBindingScope(BindingMode.TwoWay, UpdateSourceTrigger.Default).ToArray();

        private static IEnumerable<IBindingScope> CreateBindingScope(BindingMode mode, UpdateSourceTrigger trigger)
        {
            //yield return Line.X1Property.Creator(nameof(LineSetting.X1))
            //    .AddSetConfig(mode, trigger)
            //    .Build();
            //yield return Line.Y1Property.Creator(nameof(LineSetting.Y1))
            //    .AddSetConfig(mode, trigger)
            //    .Build();
            yield return Line.X2Property.Creator(nameof(LineSetting.PositionSize) + "." + nameof(PositionSize.Width))
                .AddSetConfig(mode, trigger)
                .Build();
            yield return Line.Y2Property.Creator(nameof(LineSetting.PositionSize) + "." + nameof(PositionSize.Height))
                .AddSetConfig(mode, trigger)
                .Build();
        }

        public static void SetLineSettingToUI(LineSetting obj, DependencyObject ui)
        {
            //ui.SetValue(Line.X1Property, obj.X1);
            //ui.SetValue(Line.Y1Property, obj.Y1);
            //ui.SetValue(Line.X2Property, obj.X2);
            //ui.SetValue(Line.Y2Property, obj.Y2);

            ui.SetValue(Line.X1Property, 0d);
            ui.SetValue(Line.Y1Property, 0d);
            ui.SetValue(Line.X2Property, obj.PositionSize?.Width ?? 0d);
            ui.SetValue(Line.Y2Property, obj.PositionSize?.Height ?? 0d);

        }
    }
}
