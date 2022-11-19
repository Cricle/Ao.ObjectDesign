using System.Windows.Input;

namespace Ao.ObjectDesign.Wpf.Input
{
    public abstract class MouseInputBase : IMouseInput
    {
        public virtual void OnGotMouseCapture(object sender, MouseEventArgs e)
        {
        }

        public virtual void OnLostMouseCapture(object sender, MouseEventArgs e)
        {
        }

        public virtual void OnMouseEnter(object sender, MouseEventArgs e)
        {
        }

        public virtual void OnMouseLeave(object sender, MouseEventArgs e)
        {
        }

        public virtual void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
        }

        public virtual void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
        }

        public virtual void OnMouseMove(object sender, MouseEventArgs e)
        {
        }

        public virtual void OnMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
        }

        public virtual void OnMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
        }

        public virtual void OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
        }
    }
}
