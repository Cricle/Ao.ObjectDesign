using Ao.ObjectDesign.Designing.Level;
using System.Collections;
using System.Collections.Generic;
using System.Windows;

namespace Ao.ObjectDesign.Session.Desiging
{
    public interface IPropertyContextCreator<TContext, TSetting> : IPropertyContextCreator<TSetting>
    {
        new IEnumerable<TContext> CreateContexts(IDesignPair<UIElement, TSetting> pair);
    }
    public interface IPropertyContextCreator<TSetting>
    {
        IEnumerable CreateContexts(IDesignPair<UIElement, TSetting> pair);
    }
}
