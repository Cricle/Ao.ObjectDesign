using System.Windows.Input;

namespace Ao.ObjectDesign.WpfDesign.Input
{
    public interface IPreviewStylusInput
    {
        void OnPreviewStylusUp(object sender, StylusEventArgs e);

        void OnPreviewStylusSystemGesture(object sender, StylusSystemGestureEventArgs e);

        void OnPreviewStylusOutOfRange(object sender, StylusEventArgs e);

        void OnPreviewStylusMove(object sender, StylusEventArgs e);

        void OnPreviewStylusInRange(object sender, StylusEventArgs e);

        void OnPreviewStylusInAirMove(object sender, StylusEventArgs e);

        void OnPreviewStylusDown(object sender, StylusDownEventArgs e);

        void OnPreviewStylusButtonUp(object sender, StylusButtonEventArgs e);

        void OnPreviewStylusButtonDown(object sender, StylusButtonEventArgs e);
    }
}
