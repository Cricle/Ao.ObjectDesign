using Ao.ObjectDesign.Wpf.Annotations;
using Ao.ObjectDesign.Wpf.Designing;
using System.Windows;
using System.Windows.Controls;

namespace Ao.ObjectDesign.Controls
{
    [MappingFor(typeof(TextBox))]
    public class TextBoxSetting : TextBoxBaseSetting, IMiddlewareDesigner<TextBox>
    {
        private int minLines;
        private int maxLines;
        private string text;
        private CharacterCasing characterCasing;
        private int maxLength;
        private TextAlignment textAlignment;
        private int caretIndex;
        private int selectionLength;
        private int selectionStart;
        private TextDecorationCollectionDesigner textDecorations;
        private TextWrapping textWrapping;
        private string selectedText;

        public virtual string SelectedText
        {
            get => selectedText;
            set => Set(ref selectedText, value);
        }

        public virtual TextWrapping TextWrapping
        {
            get => textWrapping;
            set => Set(ref textWrapping, value);
        }

        public virtual TextDecorationCollectionDesigner TextDecorations
        {
            get => textDecorations;
            set => Set(ref textDecorations, value);
        }

        public virtual int SelectionStart
        {
            get => selectionStart;
            set => Set(ref selectionStart, value);
        }

        public virtual int SelectionLength
        {
            get => selectionLength;
            set => Set(ref selectionLength, value);
        }


        public virtual int CaretIndex
        {
            get => caretIndex;
            set => Set(ref caretIndex, value);
        }

        public virtual TextAlignment TextAlignment
        {
            get => textAlignment;
            set => Set(ref textAlignment, value);
        }

        public virtual int MaxLength
        {
            get => maxLength;
            set => Set(ref maxLength, value);
        }


        public virtual CharacterCasing CharacterCasing
        {
            get => characterCasing;
            set => Set(ref characterCasing, value);
        }

        public virtual string Text
        {
            get => text;
            set => Set(ref text, value);
        }
        public virtual int MaxLines
        {
            get => maxLines;
            set => Set(ref maxLines, value);
        }

        public virtual int MinLines
        {
            get => minLines;
            set => Set(ref minLines, value);
        }


        public override void SetDefault()
        {
            base.SetDefault();

            MinLines = 1;
            MaxLines = int.MaxValue;
            Text = null;
            CharacterCasing = CharacterCasing.Normal;
            MaxLength = 0;
            TextAlignment = TextAlignment.Left;
            CaretIndex = 0;
            SelectionLength = 0;
            SelectionStart = 0;
            TextDecorations = new TextDecorationCollectionDesigner();
            SelectedText = null;
            TextWrapping = TextWrapping.NoWrap;
        }

        public void Apply(TextBox value)
        {
            if (value is null)
            {
                SetDefault();
            }
            else
            {
                Apply((FrameworkElement)value);
                MinLines = value.MinLines;
                MaxLines = value.MaxLength;
                Text = value.Text;
                CharacterCasing = value.CharacterCasing;
                MaxLength = value.MaxLength;
                TextAlignment = value.TextAlignment;
                CaretIndex = value.CaretIndex;
                SelectionLength = value.SelectionLength;
                SelectionStart = value.SelectionStart;
                TextDecorations = new TextDecorationCollectionDesigner { TextDecorationCollection = value.TextDecorations };
                SelectedText = value.SelectedText;
                TextWrapping = value.TextWrapping;
            }
        }

        public void WriteTo(TextBox value)
        {
            if (value != null)
            {
                WriteTo((FrameworkElement)value);
                value.MinLines = minLines;
                value.MaxLines = maxLength;
                value.Text = text;
                value.CharacterCasing = characterCasing;
                value.MaxLength = maxLength;
                value.TextAlignment = textAlignment;
                value.CaretIndex = caretIndex;
                value.SelectionLength = selectionLength;
                value.SelectionStart = selectionStart;
                value.TextDecorations = textDecorations?.TextDecorationCollection;
                value.SelectedText = selectedText;
                value.TextWrapping = textWrapping;
            }
        }
    }
}
