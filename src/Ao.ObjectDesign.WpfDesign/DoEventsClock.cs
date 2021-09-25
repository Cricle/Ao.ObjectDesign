using System.Runtime.CompilerServices;

namespace Ao.ObjectDesign.WpfDesign
{
    public struct DoEventsClock
    {
        private int currentTimes;

        public int CurrentTimes => currentTimes;

        public int DoEventTimes;

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
