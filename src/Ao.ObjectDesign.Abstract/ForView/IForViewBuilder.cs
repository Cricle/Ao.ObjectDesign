using System.Collections.Generic;

namespace Ao.ObjectDesign.ForView
{
    public interface IForViewBuilder<TView, TContext> : ICollection<IForViewCondition<TView, TContext>>
        where TContext : IForViewBuildContext
    {
    }
}
