using System.Windows.Input;

namespace Ao.ObjectDesign.WpfDesign.Input
{
    public abstract class PreviewMouseInputBase : IPreviewMouseInput
    {
        public virtual void OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
        }

        public virtual void OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
        }

        public virtual void OnPreviewMouseMove(object sender, MouseEventArgs e)
        {
        }

        public virtual void OnPreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
        }

        public virtual void OnPreviewMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
        }

        public virtual void OnPreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
        }
    }
}
