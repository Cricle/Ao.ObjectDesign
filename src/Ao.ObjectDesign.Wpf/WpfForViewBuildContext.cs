using Ao.ObjectDesign.ForView;
using System.Windows;

namespace Ao.ObjectDesign.Wpf
{
    public class WpfForViewBuildContext : WpfForViewBuildContextBase
    {
        public IForViewBuilder<FrameworkElement, WpfForViewBuildContext> ForViewBuilder { get; set; }
    }
}
