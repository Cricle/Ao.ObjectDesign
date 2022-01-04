using Ao.ObjectDesign.Designing.Level;
using System.Collections.Generic;

namespace Ao.ObjectDesign.Session.Desiging
{
    public interface IPropertyContextsDecorater<TContext, TScene, TSetting>
        where TScene : IDesignScene<TSetting>
    {
        IEnumerable<TContext> Decorate(IPropertyPanel<TScene, TSetting> panel, IEnumerable<TContext> contexts);
    }
}
