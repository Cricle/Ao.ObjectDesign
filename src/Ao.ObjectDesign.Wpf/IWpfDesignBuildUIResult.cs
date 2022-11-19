using Ao.ObjectDesign.ForView;
using System.Collections.Generic;
using System.Windows;

namespace Ao.ObjectDesign
{
    public interface IWpfDesignBuildUIResult : IWpfDesignBuildResult
    {
        IForViewBuilder<FrameworkElement, WpfForViewBuildContext> Builder { get; }

        IEnumerable<IWpfUISpirit> Contexts { get; }
    }
}
