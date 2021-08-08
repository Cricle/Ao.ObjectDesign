using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ao.ObjectDesign.WpfDesign.Input
{
    public interface IStylusInput
    {
        void OnLostStylusCapture(object sender, StylusEventArgs e);

        void OnGotStylusCapture(object sender, StylusEventArgs e);

        void OnStylusUp(object sender, StylusEventArgs e);

        void OnStylusSystemGesture(object sender, StylusSystemGestureEventArgs e);

        void OnStylusOutOfRange(object sender, StylusEventArgs e);

        void OnStylusMove(object sender, StylusEventArgs e);

        void OnStylusLeave(object sender, StylusEventArgs e);

        void OnStylusInRange(object sender, StylusEventArgs e);

        void OnStylusInAirMove(object sender, StylusEventArgs e);

        void OnStylusEnter(object sender, StylusEventArgs e);

        void OnStylusDown(object sender, StylusDownEventArgs e);

        void OnStylusButtonUp(object sender, StylusButtonEventArgs e);

        void OnStylusButtonDown(object sender, StylusButtonEventArgs e);
    }
}
