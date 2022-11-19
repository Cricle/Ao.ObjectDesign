using System.Windows.Input;

namespace Ao.ObjectDesign.Wpf.Input
{
    public interface IPreviewKeyboardInput
    {
        void OnPreviewKeyUp(object sender, KeyEventArgs e);

        void OnPreviewKeyDown(object sender, KeyEventArgs e);

        void OnPreviewGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e);

        void OnPreviewLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e);
    }
}
