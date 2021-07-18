using Ao.ObjectDesign.ForView;
using System.Collections.Generic;
using System.Windows;

namespace Ao.ObjectDesign.Wpf
{
    public interface IWpfDesignDataTemplateBuildResult : IWpfDesignBuildResult
    {
        IForViewBuilder<DataTemplate, WpfTemplateForViewBuildContext> Builder { get; }

        IReadOnlyList<WpfTemplateForViewBuildContext> Contexts { get; }

        IEnumerable<WpfTemplateForViewBuildContext> GetEqualsInstanceContexts(object instance);
    }
}
