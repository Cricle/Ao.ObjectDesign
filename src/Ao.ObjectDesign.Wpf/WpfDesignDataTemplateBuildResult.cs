using Ao.ObjectDesign.ForView;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Ao.ObjectDesign
{
    internal class WpfDesignDataTemplateBuildResult : WpfDesignBuildResult, IWpfDesignDataTemplateBuildResult
    {
        public IReadOnlyList<WpfTemplateForViewBuildContext> Contexts { get; set; }

        public IForViewBuilder<DataTemplate, WpfTemplateForViewBuildContext> Builder { get; set; }

        public IEnumerable<WpfTemplateForViewBuildContext> GetEqualsInstanceContexts(object instance)
        {
            return Contexts.Where(x => x.PropertyProxy.DeclaringInstance == instance);
        }
    }
}
