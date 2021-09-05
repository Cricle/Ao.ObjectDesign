using System.Collections.Generic;

namespace Ao.ObjectDesign.Designing.Level
{
    public interface ISceneMap<TUI, TDesignObject> : IDictionary<IDesignPair<TUI, TDesignObject>, DesignSceneController<TUI, TDesignObject>>
    {

    }
}
