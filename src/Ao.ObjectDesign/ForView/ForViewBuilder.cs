using System.Collections.Generic;

namespace Ao.ObjectDesign.ForView
{
    public class ForViewBuilder<TView, TContext> : List<IForViewCondition<TView, TContext>>, IForViewBuilder<TView, TContext>
        where TContext : IForViewBuildContext
    {
    }
}
