using Ao.ObjectDesign.Wpf.Data;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Shapes;

namespace ObjectDesign.Brock.Controls.BindingCreators
{
    public partial class ShapeSettingBindingCreator
    {
        public static readonly IReadOnlyList<IBindingScope> ShapeSettingTwoWayScopes =
            CreateBindingScope(BindingMode.TwoWay, UpdateSourceTrigger.Default).ToArray();
        public static readonly IReadOnlyList<IBindingScope> ShapeSettingOneWayScopes =
            CreateBindingScope(BindingMode.TwoWay, UpdateSourceTrigger.Default).ToArray();

        private static IEnumerable<IBindingScope> CreateBindingScope(BindingMode mode, UpdateSourceTrigger trigger)
        {
            yield return Shape.StrokeDashArrayProperty.Creator(nameof(ShapeSetting.StrokeDashArray) + ".DoubleCollection")
                .AddSetConfig(mode, trigger)
                .Build();
            yield return Shape.StrokeThicknessProperty.Creator(nameof(ShapeSetting.StrokeThickness))
                .AddSetConfig(mode, trigger)
                .Build();
            yield return Shape.StrokeProperty.Creator(nameof(ShapeSetting.Stroke) + ".Brush")
                .AddSetConfig(mode, trigger)
                .Build();
            yield return Shape.FillProperty.Creator(nameof(ShapeSetting.Fill) + ".Brush")
                .AddSetConfig(mode, trigger)
                .Build();
        }

        public static void SetShapeSettingToUI(ShapeSetting obj, DependencyObject ui)
        {
            ui.SetValue(Shape.StrokeDashArrayProperty, obj.StrokeDashArray?.GetDoubleCollection());
            ui.SetValue(Shape.StrokeThicknessProperty, obj.StrokeThickness);
            ui.SetValue(Shape.StrokeProperty, obj.Stroke?.GetBrush());
            ui.SetValue(Shape.FillProperty, obj.Fill?.GetBrush());
        }
    }
}
