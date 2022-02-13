using Ao.ObjectDesign.Bindings;
using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Wpf;
using Ao.ObjectDesign.Wpf.Data;
using ObjectDesign.Brock.Components;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Shapes;

namespace ObjectDesign.Brock.Controls.BindingCreators
{
    public partial class LineSettingBindingCreator : BrockAutoBindingCreator<Line, LineSetting>
    {
        public LineSettingBindingCreator(IDesignPair<UIElement, UIElementSetting> designUnit, IBindingCreatorState state)
            : base(designUnit, state)
        {
        }
        private static readonly IBindingScope LineX2Scope = Line.X2Property.Creator(nameof(LineSetting.PositionSize) + "." + nameof(PositionSize.Width)).Build();
        private static readonly IBindingScope LineY2Scope = Line.Y2Property.Creator(nameof(LineSetting.PositionSize) + "." + nameof(PositionSize.Height)).Build();
        protected override IWithSourceBindingScope MakeScope(IBindingScope scope)
        {
            if (scope.DependencyProperty == Line.X2Property)
            {
                return LineX2Scope.ToWithSource(DesignUnit.DesigningObject);
            }
            else if (scope.DependencyProperty == Line.Y2Property)
            {
                return LineY2Scope.ToWithSource(DesignUnit.DesigningObject);

            }
            return base.MakeScope(scope);
        }
        protected override void SetValue(UIElementSetting obj, DependencyObject ui, BindingPair<IBindingMaker> pair)
        {
            var line = (LineSetting)obj;
            var prop = GetProperty(ui.GetType(),pair.Box.Name);
            if (prop != null && (prop == Line.X2Property || prop == Line.Y2Property))
            {
                if (prop == Line.X2Property)
                {
                    ui.SetValue(Line.X2Property, line.PositionSize.Width);
                }
                else if (prop == Line.Y2Property)
                {
                    ui.SetValue(Line.Y2Property, line.PositionSize.Height);
                }
                else
                {
                    base.SetValue(obj, ui, pair);
                }
            }
            else
            {
                base.SetValue(obj, ui, pair);
            }
        }
    }
}
