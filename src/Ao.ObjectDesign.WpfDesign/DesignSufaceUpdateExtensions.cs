using System;
using System.Windows.Threading;

namespace Ao.ObjectDesign.WpfDesign
{
    public static class DesignSufaceUpdateExtensions
    {
        public static void UpdateInRender(this DesignSuface suface)
        {
            if (suface is null)
            {
                throw new ArgumentNullException(nameof(suface));
            }

            suface.Dispatcher.Invoke(suface.Update, DispatcherPriority.Render);
        }
    }
}
