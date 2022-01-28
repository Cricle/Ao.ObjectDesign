using Ao.ObjectDesign.Designing.Annotations;
using System.Windows;
using System.Windows.Controls;

namespace ObjectDesign.Brock.Controls
{
    [MappingFor(typeof(TextBox))]
    public class TextBoxSetting : TextBoxBaseSetting
    {
        private string text;
        private TextAlignment textAlignment;
        private TextWrapping textWrapping;

        public TextWrapping TextWrapping
        {
            get => textWrapping;
            set => Set(ref textWrapping, value);
        }

        public TextAlignment TextAlignment
        {
            get => textAlignment;
            set => Set(ref textAlignment, value);
        }

        public string Text
        {
            get => text;
            set => Set(ref text, value);
        }
        public override void SetDefault()
        {
            base.SetDefault();
            Text = null;
            TextWrapping = TextWrapping.NoWrap;
            TextAlignment = TextAlignment.Left;
        }
    }
}
