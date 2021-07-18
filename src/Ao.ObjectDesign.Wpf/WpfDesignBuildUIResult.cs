using Ao.ObjectDesign.ForView;
using System.Collections.Generic;
using System.Windows;

namespace Ao.ObjectDesign.Wpf
{
    internal class WpfDesignBuildUIResult : WpfDesignBuildResult, IWpfDesignBuildUIResult
    {
        public IForViewBuilder<FrameworkElement, WpfForViewBuildContext> Builder { get; set; }

        public IEnumerable<IWpfUISpirit> Contexts { get; set; }
    }
}
