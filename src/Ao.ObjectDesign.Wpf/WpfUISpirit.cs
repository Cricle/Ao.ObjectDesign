using Ao.ObjectDesign.Designing;
using System.Windows;

namespace Ao.ObjectDesign
{
    public class WpfUISpirit : UISpirit<FrameworkElement, WpfForViewBuildContext>, IWpfUISpirit
    {
        public WpfUISpirit(WpfForViewBuildContext context) : base(context)
        {
        }

        public WpfUISpirit(FrameworkElement view, WpfForViewBuildContext context) : base(view, context)
        {
        }
    }
}
