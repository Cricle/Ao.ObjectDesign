using Ao.ObjectDesign.Designing;
using Ao.ObjectDesign.Designing.Level;

namespace Ao.ObjectDesign.Session.Desiging
{
    public interface IDesignSession<TScene, TSetting> : IDesignSessionInfo<TScene, TSetting>, IDesignSessionActions, IInitableObject
        where TScene:IDesignScene<TSetting>
    {

    }
}
