using System.Windows.Input;

namespace Ao.ObjectDesign.Wpf.Input
{
    public abstract class PreviewStylusInput : IPreviewStylusInput
    {
        public virtual void OnPreviewStylusButtonDown(object sender, StylusButtonEventArgs e)
        {

        }

        public virtual void OnPreviewStylusButtonUp(object sender, StylusButtonEventArgs e)
        {

        }

        public virtual void OnPreviewStylusDown(object sender, StylusDownEventArgs e)
        {

        }

        public virtual void OnPreviewStylusInAirMove(object sender, StylusEventArgs e)
        {

        }

        public virtual void OnPreviewStylusInRange(object sender, StylusEventArgs e)
        {

        }

        public virtual void OnPreviewStylusMove(object sender, StylusEventArgs e)
        {

        }

        public virtual void OnPreviewStylusOutOfRange(object sender, StylusEventArgs e)
        {

        }

        public virtual void OnPreviewStylusSystemGesture(object sender, StylusSystemGestureEventArgs e)
        {

        }

        public virtual void OnPreviewStylusUp(object sender, StylusEventArgs e)
        {

        }
    }
}
