using System.Windows.Threading;

namespace Ao.ObjectDesign.WpfDesign
{
    public static class DoEventsHelper
    {
        private static readonly DispatcherOperationCallback operationCallback = new DispatcherOperationCallback(ExitFrame);
        public static void DoEvents()
        {
            DispatcherFrame frame = new DispatcherFrame();
            Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background, operationCallback, frame);
            Dispatcher.PushFrame(frame);
        }
        private static object ExitFrame(object state)
        {
            ((DispatcherFrame)state).Continue = false;
            return null;
        }
        public static DoEventsClock Clock(int times)
        {
            return new DoEventsClock { DoEventTimes = times };
        }
    }
}
