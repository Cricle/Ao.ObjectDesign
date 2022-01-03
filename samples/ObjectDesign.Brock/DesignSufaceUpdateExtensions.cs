using Ao.ObjectDesign.WpfDesign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace ObjectDesign.Brock
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
