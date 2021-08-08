using System.Windows.Input;

namespace Ao.ObjectDesign.WpfDesign.Input
{
    public interface ITextInput
    {
        void OnPreviewTextInput(object sender, TextCompositionEventArgs e);

        void OnTextInput(object sender, TextCompositionEventArgs e);
    }
}
