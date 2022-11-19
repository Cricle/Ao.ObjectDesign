using Ao.ObjectDesign.Designing.Annotations;
using Ao.ObjectDesign.Designing;
using System.Windows;
using System.Windows.Controls;

namespace Ao.ObjectDesign.Controls
{
    [MappingFor(typeof(Panel))]
    public class PanelSetting : FrameworkElementSetting, IMiddlewareDesigner<Panel>
    {
        private BrushDesigner background;

        public virtual BrushDesigner Background
        {
            get => background;
            set => Set(ref background, value);
        }
        public override void SetDefault()
        {
            base.SetDefault();
            Background = new BrushDesigner();
        }
        public void Apply(Panel value)
        {
            if (value is null)
            {
                SetDefault();
            }
            else
            {
                Apply((FrameworkElement)value);
                Background = new BrushDesigner();
                Background.SetBrush(value.Background);
            }
        }

        public void WriteTo(Panel value)
        {
            if (value != null)
            {
                WriteTo((FrameworkElement)value);
                value.Background = background?.GetBrush();
            }
        }
    }
}
