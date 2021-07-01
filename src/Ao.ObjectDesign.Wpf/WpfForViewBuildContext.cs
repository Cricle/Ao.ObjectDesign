using Ao.ObjectDesign.ForView;
using System.Windows;
using System.Windows.Data;

namespace Ao.ObjectDesign.Wpf
{
    public class WpfForViewBuildContext : WpfForViewBuildContextBase
    {
        public IForViewBuilder<FrameworkElement, WpfForViewBuildContext> ForViewBuilder { get; set; }
    }
}
