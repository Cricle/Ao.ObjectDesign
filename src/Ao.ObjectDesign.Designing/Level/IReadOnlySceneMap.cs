using System.Collections.Generic;

namespace Ao.ObjectDesign.Designing.Level
{
    public interface IReadOnlySceneMap<TUI, TDesignObject> : IReadOnlyDictionary<IDesignPair<TUI, TDesignObject>, IDesignSceneController<TUI, TDesignObject>>
    {

    }
}
