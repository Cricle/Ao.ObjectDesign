using Ao.ObjectDesign.Wpf.Annotations;
using Ao.ObjectDesign.Wpf.Designing;
using System.Windows;
using System.Windows.Controls;

namespace Ao.ObjectDesign.Controls
{
    [MappingFor(typeof(TextBlock))]
    public class TextBlockSetting : FrameworkElementSetting, IMiddlewareDesigner<TextBlock>
    {
        private double fontSize;
        private FontWeightDesigner fontWeight;
        private FontStyleDesigner fontStyle;
        private FontFamilyDesigner fontFamily;
        private string text;
        private double baselineOffset;
        private TextWrapping textWrapping;
        private BrushDesigner background;
        private double lineHeight;
        private LineStackingStrategy lineStackingStrategy;
        private ThicknessDesigner padding;
        private TextAlignment textAlignment;
        private TextTrimming textTrimming;
        private bool isHyphenationEnabled;
        private BrushDesigner foreground;
        private FontStretchDesigner fontStretch;
        private TextDecorationCollectionDesigner textDecorations;

        public virtual TextDecorationCollectionDesigner TextDecorations
        {
            get => textDecorations;
            set => Set(ref textDecorations, value);
        }

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

        public virtual double FontSize
        {
            get => fontSize;
            set => Set(ref fontSize, value);
        }

        public virtual BrushDesigner Foreground
        {
            get => foreground;
            set => Set(ref foreground, value);
        }

        public virtual bool IsHyphenationEnabled
        {
            get => isHyphenationEnabled;
            set => Set(ref isHyphenationEnabled, value);
        }

        public virtual TextTrimming TextTrimming
        {
            get => textTrimming;
            set => Set(ref textTrimming, value);
        }

        public virtual TextAlignment TextAlignment
        {
            get => textAlignment;
            set => Set(ref textAlignment, value);
        }

        public virtual ThicknessDesigner Padding
        {
            get => padding;
            set => Set(ref padding, value);
        }

        public virtual LineStackingStrategy LineStackingStrategy
        {
            get => lineStackingStrategy;
            set => Set(ref lineStackingStrategy, value);
        }

        public virtual double LineHeight
        {
            get => lineHeight;
            set => Set(ref lineHeight, value);
        }

        public virtual BrushDesigner Background
        {
            get => background;
            set => Set(ref background, value);
        }

        public virtual TextWrapping TextWrapping
        {
            get => textWrapping;
            set => Set(ref textWrapping, value);
        }

        public virtual double BaselineOffset
        {
            get => baselineOffset;
            set => Set(ref baselineOffset, value);
        }

        public virtual string Text
        {
            get => text;
            set => Set(ref text, value);
        }

        public override void SetDefault()
        {
            base.SetDefault();
            FontWeight = new FontWeightDesigner();
            FontStyle = new FontStyleDesigner();
            FontFamily = new FontFamilyDesigner();
            Text = null;
            FontStretch = new FontStretchDesigner();
            BaselineOffset = double.NaN;
            FontSize = SystemFonts.MessageFontSize;
            TextWrapping = TextWrapping.NoWrap;
            Background = new BrushDesigner();
            TextDecorations = new TextDecorationCollectionDesigner();
            LineHeight = double.NaN;
            LineStackingStrategy = LineStackingStrategy.MaxHeight;
            Padding = new ThicknessDesigner();
            TextAlignment = TextAlignment.Left;
            TextTrimming = TextTrimming.None;
            IsHyphenationEnabled = false;
            Foreground = new BrushDesigner();


        }

        public void Apply(TextBlock value)
        {
            if (value is null)
            {
                SetDefault();
            }
            else
            {
                Apply((FrameworkElement)value);
                FontWeight = new FontWeightDesigner { FontWeight = value.FontWeight };
                FontStyle = new FontStyleDesigner { FontStyle = value.FontStyle };
                FontFamily = new FontFamilyDesigner { FontFamily = value.FontFamily };
                Text = value.Text;
                FontStretch = new FontStretchDesigner { FontStretch = value.FontStretch };
                BaselineOffset = value.BaselineOffset;
                FontSize = value.FontSize;
                TextWrapping = value.TextWrapping;
                Background = new BrushDesigner { Brush = value.Background };
                TextDecorations = new TextDecorationCollectionDesigner { TextDecorationCollection = value.TextDecorations };
                LineHeight = value.LineHeight;
                LineStackingStrategy = value.LineStackingStrategy;
                Padding = new ThicknessDesigner { Thickness = value.Padding };
                TextAlignment = value.TextAlignment;
                TextTrimming = value.TextTrimming;
                IsHyphenationEnabled = value.IsHyphenationEnabled;
                Foreground = new BrushDesigner { Brush = value.Foreground };
            }
        }

        public void WriteTo(TextBlock value)
        {
            if (value != null)
            {
                WriteTo((FrameworkElement)value);
                value.FontWeight = fontWeight?.FontWeight ?? default;
                value.FontStyle = fontStyle?.FontStyle ?? default;
                value.FontFamily = fontFamily?.FontFamily;
                value.Text = text; ;
                value.FontStretch = fontStretch?.FontStretch ?? default;
                value.BaselineOffset = baselineOffset;
                value.FontSize = fontSize;
                value.TextWrapping = textWrapping;
                value.Background = background?.Brush;
                value.TextDecorations = textDecorations?.TextDecorationCollection;
                value.LineHeight = lineHeight;
                value.LineStackingStrategy = lineStackingStrategy;
                value.Padding = padding?.Thickness ?? default;
                value.TextAlignment = textAlignment;
                value.TextTrimming = textTrimming;
                value.IsHyphenationEnabled = isHyphenationEnabled;
                value.Foreground = foreground?.Brush;
            }
        }
    }
}
