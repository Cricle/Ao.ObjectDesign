using System.Windows.Input;

namespace Ao.ObjectDesign.Wpf.Input
{
    public abstract class KeyboardInputBase : IKeyboardInput
    {
        public virtual void OnGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
        }

        public virtual void OnKeyDown(object sender, KeyEventArgs e)
        {
        }

        public virtual void OnKeyUp(object sender, KeyEventArgs e)
        {
        }

        public virtual void OnLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
        }
    }
}
