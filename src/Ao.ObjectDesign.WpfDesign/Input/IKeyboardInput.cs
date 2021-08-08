using System.Windows.Input;

namespace Ao.ObjectDesign.WpfDesign.Input
{
    public interface IKeyboardInput
    {
        void OnKeyUp(object sender, KeyEventArgs e);

        void OnKeyDown(object sender, KeyEventArgs e);

        void OnGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e);

        void OnLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e);
    }
}
