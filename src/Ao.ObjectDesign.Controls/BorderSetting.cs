using Ao.ObjectDesign.Wpf.Annotations;
using Ao.ObjectDesign.Wpf.Designing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            set => Set(ref background, value);
        }

        public virtual BrushDesigner BorderBrush
        {
            get => borderBrush;
            set => Set(ref borderBrush, value);
        }


        public virtual CornerRadiusDesigner CornerRadius
        {
            get => cornerRadius;
            set => Set(ref cornerRadius, value);
        }

        public virtual ThicknessDesigner Padding
        {
            get => padding;
            set => Set(ref padding, value);
        }


        public virtual ThicknessDesigner BorderThicknes
        {
            get => borderThicknes;
            set => Set(ref borderThicknes, value);
        }

        public override void SetDefault()
        {
            base.SetDefault();
            Background = null;
            BorderBrush = null;
            CornerRadius = null;
            Padding = null;
            BorderThicknes = null;
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
                    Background = new BrushDesigner { Brush = value.Background };
                }
                else
                {
                    background.Brush = value.Background;
                }
                if (borderBrush is null)
                {
                    BorderBrush = new BrushDesigner { Brush = value.BorderBrush };
                }
                else
                {
                    borderBrush.Brush = value.BorderBrush;
                }
                if (cornerRadius is null)
                {
                    CornerRadius = new CornerRadiusDesigner { CornerRadius = value.CornerRadius };
                }
                else
                {
                    cornerRadius.CornerRadius = value.CornerRadius;
                }
                if (padding is null)
                {
                    Padding = new ThicknessDesigner { Thickness = value.Padding };
                }
                else
                {
                    padding.Thickness = value.Padding;
                }
                if (borderThicknes is null)
                {
                    BorderThicknes = new ThicknessDesigner { Thickness = value.BorderThickness };
                }
                else
                {
                    borderThicknes.Thickness = value.BorderThickness;
                }
            }
        }

        public void WriteTo(Border value)
        {
            if (value != null)
            {
                WriteTo((FrameworkElement)value);
                value.Background = background?.Brush;
                value.BorderThickness = borderThicknes?.Thickness ?? default;
                value.BorderBrush = borderBrush?.Brush;
                value.Padding = padding?.Thickness ?? default;
                value.CornerRadius = cornerRadius?.CornerRadius ?? default;
            }
        }
    }
}
