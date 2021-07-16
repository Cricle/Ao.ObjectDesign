using System;
using System.Text;

namespace Ao.ObjectDesign.ForView
{
    public interface IForViewCondition<out TView,TContext>
        where TContext:IForViewBuildContext
    {
        int Order { get; }

        TView Create(TContext context);

        bool CanBuild(TContext context);
    }
}
