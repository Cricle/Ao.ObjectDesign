using Ao.ObjectDesign.Designing.Annotations;
using Ao.ObjectDesign.Designing;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Ao.ObjectDesign.Controls
{
    [MappingFor(typeof(Control))]
    public class ControlSetting : FrameworkElementSetting, IMiddlewareDesigner<Control>
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
        private FontStretchDesigner fontStretch;

        public virtual FontStretchDesigner FontStretch
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
        [DefaultValue(12d)]
        public virtual double FontSize
        {
            get => fontSize;
            set => Set(ref fontSize, value);
        }
        [DefaultValue(HorizontalAlignment.Left)]
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

        [DefaultValue(int.MaxValue)]
        public virtual int TabIndex
        {
            get => tabIndex;
            set => Set(ref tabIndex, value);
        }

        [DefaultValue(VerticalAlignment.Top)]
        public virtual VerticalAlignment VerticalContentAlignment
        {
            get => verticalContentAlignment;
            set => Set(ref verticalContentAlignment, value);
        }

        [DefaultValue(true)]
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
            FontStretch = new FontStretchDesigner();
            FontSize = SystemFonts.MessageFontSize;
            FontFamily = new FontFamilyDesigner();
            Foreground = new BrushDesigner();
            Background = new BrushDesigner();
            BorderThickness = new ThicknessDesigner();
            IsTabStop = true;
            VerticalContentAlignment = VerticalAlignment.Top;
            TabIndex = int.MaxValue;
            Padding = new ThicknessDesigner();
            FontWeight = new FontWeightDesigner();
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
                FontStyle = new FontStyleDesigner();
                FontStyle.SetFontStyle(value.FontStyle);
                FontSize = value.FontSize;
                FontStretch = new FontStretchDesigner();
                FontStretch.SetFontStretch(value.FontStretch);
                FontFamily = new FontFamilyDesigner();
                FontFamily.SetFontFamily(value.FontFamily);
                Foreground = new BrushDesigner();
                Foreground.SetBrush(value.Foreground);
                Background = new BrushDesigner();
                Background.SetBrush(value.Background);
                BorderThickness = new ThicknessDesigner();
                BorderThickness.SetThickness(value.BorderThickness);
                IsTabStop = value.IsTabStop;
                VerticalContentAlignment = value.VerticalAlignment;
                Padding = new ThicknessDesigner();
                Padding.SetThickness(value.Padding);
                FontWeight = new FontWeightDesigner();
                FontWeight.SetFontWeight(value.FontWeight);
                BorderBrush = new BrushDesigner();
                BorderBrush.SetBrush(value.BorderBrush);
                HorizontalContentAlignment = value.HorizontalAlignment;
            }
        }

        public void WriteTo(Control value)
        {
            if (value != null)
            {
                WriteTo((FrameworkElement)value);
                value.FontStyle = FontStyle?.GetFontStyle() ?? default;
                value.FontStretch = FontStretch?.GetFontStretch() ?? default;
                value.FontSize = FontSize;
                value.FontFamily = FontFamily?.GetFontFamily();
                value.Foreground = Foreground?.GetBrush();
                value.Background = Background?.GetBrush();
                value.BorderThickness = BorderThickness?.GetThickness() ?? default;
                value.IsTabStop = IsTabStop;
                value.VerticalContentAlignment = VerticalContentAlignment;
                value.TabIndex = TabIndex;
                value.Padding = Padding?.GetThickness() ?? default;
                value.FontWeight = FontWeight.GetFontWeight();
                value.BorderBrush = BorderBrush?.GetBrush();
                value.HorizontalContentAlignment = HorizontalContentAlignment;
            }
        }
    }
}
