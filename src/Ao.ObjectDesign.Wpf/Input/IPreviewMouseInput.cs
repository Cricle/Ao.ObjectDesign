using System.Windows.Input;

namespace Ao.ObjectDesign.Wpf.Input
{
    public interface IPreviewMouseInput
    {
        void OnPreviewMouseWheel(object sender, MouseWheelEventArgs e);

        void OnPreviewMouseRightButtonUp(object sender, MouseButtonEventArgs e);

        void OnPreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e);

        void OnPreviewMouseMove(object sender, MouseEventArgs e);

        void OnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e);

        void OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e);
    }
}
