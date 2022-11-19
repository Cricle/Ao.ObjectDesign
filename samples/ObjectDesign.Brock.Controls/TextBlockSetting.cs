using Ao.ObjectDesign.Designing.Annotations;
using Ao.ObjectDesign.Designing;
using ObjectDesign.Brock.Components;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ObjectDesign.Brock.Controls
{
    [MappingFor(typeof(TextBlock))]
    public class TextBlockSetting : FrameworkElementSetting
    {
        private string text;
        private BrushDesigner background;
        private BrushDesigner foreground;
        private TextTrimming textTrimming;
        private TextWrapping textWrapping;
        private double fontSize;

        public double FontSize
        {
            get => fontSize;
            set => Set(ref fontSize, value);
        }

        public TextWrapping TextWrapping
        {
            get => textWrapping;
            set => Set(ref textWrapping, value);
        }

        public TextTrimming TextTrimming
        {
            get => textTrimming;
            set => Set(ref textTrimming, value);
        }

        public string Text
        {
            get => text;
            set => Set(ref text, value);
        }

        public BrushDesigner Background
        {
            get => background;
            set => Set(ref background, value);
        }

        public BrushDesigner Foreground
        {
            get => foreground;
            set => Set(ref foreground, value);
        }
        public override void SetDefault()
        {
            base.SetDefault();
            Text = null;
            FontSize = 12;
            TextTrimming = TextTrimming.None;
            Background = new BrushDesigner();
            Background.SetBrush(Brushes.Transparent);
            Foreground = new BrushDesigner();
            Foreground.SetBrush(Brushes.Transparent);
        }
    }
}
