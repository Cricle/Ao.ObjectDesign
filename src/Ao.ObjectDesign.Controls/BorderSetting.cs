using Ao.ObjectDesign.Designing.Annotations;
using Ao.ObjectDesign.Designing;
using System.Windows;
using System.Windows.Controls;

namespace Ao.ObjectDesign.Controls
{
    [MappingFor(typeof(Border))]
    public class BorderSetting : FrameworkElementSetting, IMiddlewareDesigner<Border>
    {
        private ThicknessDesigner borderThicknes;
        private ThicknessDesigner padding;
        private CornerRadiusDesigner cornerRadius;
        private BrushDesigner borderBrush;
        private BrushDesigner background;

        public virtual BrushDesigner Background
        {
            get => background;
            set
            {
                Set(ref background, value);
            }
        }

        public virtual BrushDesigner BorderBrush
        {
            get => borderBrush;
            set
            {
                Set(ref borderBrush, value);
            }
        }


        public virtual CornerRadiusDesigner CornerRadius
        {
            get => cornerRadius;
            set
            {
                Set(ref cornerRadius, value);
            }
        }

        public virtual ThicknessDesigner Padding
        {
            get => padding;
            set
            {
                Set(ref padding, value);
            }
        }

        public virtual ThicknessDesigner BorderThicknes
        {
            get => borderThicknes;
            set
            {
                Set(ref borderThicknes, value);
            }
        }

        public override void SetDefault()
        {
            base.SetDefault();
            Background = new BrushDesigner();
            BorderBrush = new BrushDesigner();
            CornerRadius = new CornerRadiusDesigner();
            Padding = new ThicknessDesigner();
            BorderThicknes = new ThicknessDesigner();
        }

        public void Apply(Border value)
        {
            if (value is null)
            {
                SetDefault();
            }
            else
            {
                if (background is null)
                {
                    Background = new BrushDesigner();
                }
                Background.SetBrush(value.Background);
                if (borderBrush is null)
                {
                    BorderBrush = new BrushDesigner();
                }
                borderBrush.SetBrush(value.BorderBrush);
                if (cornerRadius is null)
                {
                    CornerRadius = new CornerRadiusDesigner();
                }
                cornerRadius.SetCornerRadius(value.CornerRadius);
                if (padding is null)
                {
                    Padding = new ThicknessDesigner();
                }
                padding.SetThickness(value.Padding);
                if (borderThicknes is null)
                {
                    BorderThicknes = new ThicknessDesigner();
                }
                borderThicknes.SetThickness(value.BorderThickness);
            }
        }

        public void WriteTo(Border value)
        {
            if (value != null)
            {
                WriteTo((FrameworkElement)value);
                value.Background = background?.GetBrush();
                value.BorderThickness = borderThicknes?.GetThickness() ?? default;
                value.BorderBrush = borderBrush?.GetBrush();
                value.Padding = padding?.GetThickness() ?? default;
                value.CornerRadius = cornerRadius?.GetCornerRadius() ?? default;
            }
        }
    }
}
