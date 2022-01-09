using System.Collections.Generic;

namespace Ao.ObjectDesign.Designing
{
    public interface IUIGenerator<TView, TContext>
    {
        IEnumerable<IUISpirit<TView, TContext>> Generate(IEnumerable<IPropertyProxy> propertyProxies);
    }
}
