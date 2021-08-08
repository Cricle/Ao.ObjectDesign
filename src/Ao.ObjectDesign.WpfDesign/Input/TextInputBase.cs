using System.Windows.Input;

namespace Ao.ObjectDesign.WpfDesign.Input
{
    public abstract class TextInputBase : ITextInput
    {
        public virtual void OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
        }

        public virtual void OnTextInput(object sender, TextCompositionEventArgs e)
        {
        }
    }
}
