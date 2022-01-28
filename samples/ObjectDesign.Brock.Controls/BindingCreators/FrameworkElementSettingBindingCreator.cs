using Ao.ObjectDesign.Bindings;
using Ao.ObjectDesign.Designing.Level;
using Ao.ObjectDesign.Wpf.Data;
using ObjectDesign.Brock.Components;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace ObjectDesign.Brock.Controls.BindingCreators
{
    public partial class FrameworkElementSettingBindingCreator : BrockAutoBindingCreator<FrameworkElement, FrameworkElementSetting>
    {
        public FrameworkElementSettingBindingCreator(IDesignPair<UIElement, UIElementSetting> designUnit, IBindingCreatorState state)
            : base(designUnit, state)
        {
        }

        private static readonly IReadOnlyList<IBindingScope> PositionSizeScopes = new IBindingScope[]
        {
            Canvas.LeftProperty.Creator(nameof(FrameworkElementSetting.PositionSize)+"."+nameof(PositionSize.X)).AddSetConfig( BindingMode.TwoWay, UpdateSourceTrigger.PropertyChanged).Build(),
            Canvas.TopProperty.Creator(nameof(FrameworkElementSetting.PositionSize)+"."+nameof(PositionSize.Y)).AddSetConfig( BindingMode.TwoWay, UpdateSourceTrigger.PropertyChanged).Build(),
            FrameworkElement.WidthProperty.Creator(nameof(FrameworkElementSetting.PositionSize)+"."+nameof(PositionSize.Width)).AddSetConfig( BindingMode.TwoWay, UpdateSourceTrigger.PropertyChanged).Build(),
            FrameworkElement.HeightProperty.Creator(nameof(FrameworkElementSetting.PositionSize)+"."+nameof(PositionSize.Height)).AddSetConfig( BindingMode.TwoWay, UpdateSourceTrigger.PropertyChanged).Build(),
        };

        protected override IEnumerable<IWithSourceBindingScope> CreateBindingScopes()
        {
            foreach (var item in base.CreateBindingScopes())
            {
                yield return item;
            }
            if (CanGenerateBindings())
            {
                foreach (var item in PositionSizeScopes)
                {
                    yield return item.ToWithSource(DesignUnit.DesigningObject);
                }
            }
        }
    }
}
