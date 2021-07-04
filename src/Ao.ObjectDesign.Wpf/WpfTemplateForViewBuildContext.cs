using Ao.ObjectDesign.ForView;
using System.Windows;

namespace Ao.ObjectDesign.Wpf
{
    public class WpfTemplateForViewBuildContext : WpfForViewBuildContextBase
    {
        public IForViewBuilder<DataTemplate, WpfTemplateForViewBuildContext> ForViewBuilder { get; set; }
    }
}
