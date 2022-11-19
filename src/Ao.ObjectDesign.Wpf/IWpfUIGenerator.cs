using Ao.ObjectDesign.Designing;
using System.Collections.Generic;
using System.Windows;

namespace Ao.ObjectDesign
{
    public interface IWpfUIGenerator : IUIGenerator<FrameworkElement, WpfForViewBuildContext>
    {
        new IEnumerable<IWpfUISpirit> Generate(IEnumerable<IPropertyProxy> propertyProxies);
    }
}
