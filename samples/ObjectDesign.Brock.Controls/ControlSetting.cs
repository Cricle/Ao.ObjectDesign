using Ao.ObjectDesign.Designing.Annotations;
using Ao.ObjectDesign.Wpf.Designing;
using ObjectDesign.Brock.Components;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ObjectDesign.Brock.Controls
{
    [MappingFor(typeof(Control))]
    public class ControlSetting : FrameworkElementSetting
    {
        private VerticalAlignment verticalContentAlignment;
        private HorizontalAlignment horizontalContentAlignment;
        private BrushDesigner background;
        private BrushDesigner foreground;
        private double fontSize;
        private FontFamilyDesigner fontFamily;
        private BrushDesigner borderBrush;
        private ThicknessDesigner padding;
        private ThicknessDesigner borderThickness;
        private FontStyleDesigner fontStyle;
        private FontStretchDesigner fontStretch;
        private FontWeightDesigner fontWeight;

        public FontWeightDesigner FontWeight
        {
            get => fontWeight;
            set => Set(ref fontWeight, value);
        }
        public FontStretchDesigner FontStretch
        {
            get => fontStretch;
            set => Set(ref fontStretch, value);
        }
        public FontStyleDesigner FontStyle
        {
            get => fontStyle;
            set => Set(ref fontStyle, value);
        }

        public ThicknessDesigner BorderThickness
        {
            get => borderThickness;
            set => Set(ref borderThickness, value);
        }

        public ThicknessDesigner Padding
        {
            get => padding;
            set => Set(ref padding, value);
        }

        public BrushDesigner BorderBrush
        {
            get => borderBrush;
            set => Set(ref borderBrush, value);
        }

        public FontFamilyDesigner FontFamily
        {
            get => fontFamily;
            set => Set(ref fontFamily, value);
        }

        public double FontSize
        {
            get => fontSize;
            set => Set(ref fontSize, value);
        }

        public BrushDesigner Foreground
        {
            get => foreground;
            set => Set(ref foreground, value);
        }
        public BrushDesigner Background
        {
            get => background;
            set => Set(ref background, value);
        }

        public HorizontalAlignment HorizontalContentAlignment
        {
            get => horizontalContentAlignment;
            set => Set(ref horizontalContentAlignment, value);
        }

        public VerticalAlignment VerticalContentAlignment
        {
            get => verticalContentAlignment;
            set => Set(ref verticalContentAlignment, value);
        }

        public override void SetDefault()
        {
            base.SetDefault();
            HorizontalContentAlignment = HorizontalAlignment.Stretch;
            VerticalContentAlignment = VerticalAlignment.Stretch;
            Background = new BrushDesigner();
            Background.SetBrush(Brushes.Transparent);
            BorderBrush = new BrushDesigner();
            BorderBrush.SetBrush(Brushes.Black);
            Foreground = new BrushDesigner();
            Foreground.SetBrush(Brushes.Black);
            FontSize = 12;
            FontFamily = new FontFamilyDesigner();
            FontFamily.SetFontFamily(new FontFamily());
            Padding = new ThicknessDesigner();
            BorderThickness = new ThicknessDesigner();
            FontStretch = new FontStretchDesigner();
            FontFamily = new FontFamilyDesigner();
            FontStyle = new FontStyleDesigner();
            FontWeight = new FontWeightDesigner();
        }
    }
}
