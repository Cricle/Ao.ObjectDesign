using System.Windows.Input;

namespace Ao.ObjectDesign.WpfDesign.Input
{
    public abstract class PreviewKeyboardInputBase : IPreviewKeyboardInput
    {
        public virtual void OnPreviewGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
        }

        public virtual void OnPreviewKeyDown(object sender, KeyEventArgs e)
        {
        }

        public virtual void OnPreviewKeyUp(object sender, KeyEventArgs e)
        {
        }

        public virtual void OnPreviewLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
        }
    }
}
