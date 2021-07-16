using System.Collections.Generic;

namespace Ao.ObjectDesign.Wpf
{
    public interface IUIGenerator<TView, TContext>
    {
        IEnumerable<IUISpirit<TView, TContext>> Generate(IEnumerable<IPropertyProxy> propertyProxies);
    }
}
