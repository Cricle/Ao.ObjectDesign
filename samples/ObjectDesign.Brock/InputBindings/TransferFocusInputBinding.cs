using Ao.ObjectDesign.WpfDesign.Input;
using System.Windows;
using System.Windows.Input;

namespace ObjectDesign.Brock.InputBindings
{
    internal class TransferFocusInputBinding : PreviewMouseInputBase
    {
        public override void OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Keyboard.Focus(sender as UIElement);
        }
    }
}
