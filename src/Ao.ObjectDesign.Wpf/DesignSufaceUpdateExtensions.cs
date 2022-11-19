using System;
using System.Windows.Threading;

namespace Ao.ObjectDesign.Wpf
{
    public static class DesignSufaceUpdateExtensions
    {
        public static void UpdateInRender(this DesignSuface suface)
        {
            UpdateInThread(suface, DispatcherPriority.Render);
        }
        public static void UpdateInThread(this DesignSuface suface, DispatcherPriority priority)
        {
            if (suface is null)
            {
                throw new ArgumentNullException(nameof(suface));
            }

            suface.Dispatcher.Invoke(suface.Update, priority);
        }
    }
}
