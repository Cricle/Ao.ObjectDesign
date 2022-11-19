using Ao.ObjectDesign.Designing.Level;

namespace Ao.ObjectDesign.Session.Wpf.Desiging
{
    public interface IPropertyPanel<TScene, TSetting> : IPropertyContextCreator<TSetting>
        where TScene : IDesignScene<TSetting>
    {
        IDesignSession<TScene, TSetting> Session { get; }
    }
}
