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
    [MappingFor(typeof(Control))]
    public class ControlSetting : FrameworkElementSetting,IMiddlewareDesigner<Control>
    {
        private BrushDesigner foreground;
        private BrushDesigner background;
        private BrushDesigner borderBrush;
        private ThicknessDesigner borderThickness;
        private bool isTabStop;
        private VerticalAlignment verticalContentAlignment;
        private HorizontalAlignment horizontalContentAlignment;
        private int tabIndex;
        private ThicknessDesigner padding;
        private double fontSize = 12;
        private FontStyleDesigner fontStyle;
        private FontWeightDesigner fontWeight;
        private FontFamilyDesigner fontFamily;
        private FontStretchSetting fontStretch;

        public virtual FontStretchSetting FontStretch
        {
            get => fontStretch;
            set => Set(ref fontStretch, value);
        }

        public virtual FontFamilyDesigner FontFamily
        {
            get => fontFamily;
            set => Set(ref fontFamily, value);
        }
        public virtual FontStyleDesigner FontStyle
        {
            get => fontStyle;
            set => Set(ref fontStyle, value);
        }
        public virtual FontWeightDesigner FontWeight
        {
            get => fontWeight;
            set => Set(ref fontWeight, value);
        }

        public virtual double FontSize
        {
            get => fontSize;
            set => Set(ref fontSize, value);
        }

        public virtual HorizontalAlignment HorizontalContentAlignment
        {
            get => horizontalContentAlignment;
            set => Set(ref horizontalContentAlignment, value);
        }

        public virtual BrushDesigner BorderBrush
        {
            get => borderBrush;
            set => Set(ref borderBrush, value);
        }

        public virtual ThicknessDesigner Padding
        {
            get => padding;
            set => Set(ref padding, value);
        }

        public virtual int TabIndex
        {
            get => tabIndex;
            set => Set(ref tabIndex, value);
        }

        public virtual VerticalAlignment VerticalContentAlignment
        {
            get => verticalContentAlignment;
            set => Set(ref verticalContentAlignment, value);
        }

        public virtual bool IsTabStop
        {
            get => isTabStop;
            set => Set(ref isTabStop, value);
        }

        public virtual ThicknessDesigner BorderThickness
        {
            get => borderThickness;
            set => Set(ref borderThickness, value);
        }

        public virtual BrushDesigner Background
        {
            get => background;
            set => Set(ref background, value);
        }
        public virtual BrushDesigner Foreground
        {
            get => foreground;
            set => Set(ref foreground, value);
        }

        public override void SetDefault()
        {
            base.SetDefault();
            FontStyle = new FontStyleDesigner();
            FontStretch = new FontStretchSetting();
            FontSize = SystemFonts.MessageFontSize;
            FontFamily = new FontFamilyDesigner();
            Foreground = new BrushDesigner();
            Background = new BrushDesigner();
            BorderThickness = new ThicknessDesigner();
            IsTabStop = true;
            VerticalContentAlignment = VerticalAlignment.Top;
            TabIndex = int.MaxValue;
            Padding = new ThicknessDesigner();
            FontWeight =new FontWeightDesigner();
            BorderBrush = new BrushDesigner();
            HorizontalContentAlignment = HorizontalAlignment.Left;
        }
        public void Apply(Control value)
        {
            if (value is null)
            {
                SetDefault();
            }
            else
            {
                Apply((FrameworkElement)value);
                FontStyle = new FontStyleDesigner { FontStyle = value.FontStyle};
                FontSize = value.FontSize;
                FontStretch =new FontStretchSetting { FontStretch = value.FontStretch };
                FontFamily = new FontFamilyDesigner { FontFamily= value.FontFamily };
                Foreground = new BrushDesigner { Brush= value.Foreground};
                Background =new BrushDesigner { Brush = value.Background};
                BorderThickness = new ThicknessDesigner { Thickness = value.BorderThickness };
                IsTabStop = value.IsTabStop;
                VerticalContentAlignment = value.VerticalAlignment;
                Padding = new ThicknessDesigner { Thickness = value.Padding };
                FontWeight = new FontWeightDesigner { FontWeight = value.FontWeight };
                BorderBrush = new BrushDesigner { Brush = value.BorderBrush };
                HorizontalContentAlignment = value.HorizontalAlignment;
            }
        }

        public void WriteTo(Control value)
        {
            if (value!=null)
            {
                WriteTo((FrameworkElement)value);
                value.FontStyle = FontStyle?.FontStyle??default;
                value.FontStretch = FontStretch?.FontStretch ?? default;
                value.FontSize = FontSize;
                value.FontFamily = FontFamily?.FontFamily;
                value.Foreground = Foreground?.Brush;
                value.Background = Background?.Brush;
                value.BorderThickness = BorderThickness?.Thickness ?? default;
                value.IsTabStop = IsTabStop;
                value.VerticalContentAlignment = VerticalContentAlignment;
                value.TabIndex = TabIndex;
                value.Padding = Padding?.Thickness ?? default;
                value.FontWeight = FontWeight.FontWeight;
                value.BorderBrush = BorderBrush?.Brush;
                value.HorizontalContentAlignment = HorizontalContentAlignment;
            }
        }
    }
}
