using System.Windows.Input;

namespace Ao.ObjectDesign.Wpf.Input
{
    public interface IMouseInput
    {
        void OnMouseRightButtonUp(object sender, MouseButtonEventArgs e);

        void OnMouseRightButtonDown(object sender, MouseButtonEventArgs e);

        void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e);

        void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e);

        void OnMouseLeave(object sender, MouseEventArgs e);

        void OnMouseWheel(object sender, MouseWheelEventArgs e);

        void OnMouseMove(object sender, MouseEventArgs e);

        void OnMouseEnter(object sender, MouseEventArgs e);

        void OnLostMouseCapture(object sender, MouseEventArgs e);

        void OnGotMouseCapture(object sender, MouseEventArgs e);
    }
}
