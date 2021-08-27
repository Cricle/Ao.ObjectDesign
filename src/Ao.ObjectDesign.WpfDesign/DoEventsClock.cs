using System.Runtime.CompilerServices;

namespace Ao.ObjectDesign.WpfDesign
{
    public class DoEventsClock
    {
        private int currentTimes;

        public int CurrentTimes => currentTimes;

        public int DoEventTimes { get; set; }

        public bool Do()
        {
            currentTimes++;
            if (currentTimes >= DoEventTimes)
            {
                DoEventsHelper.DoEvents();
                Clear();
                return true;
            }
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Clear()
        {
            currentTimes = 0;
        }
    }
}
