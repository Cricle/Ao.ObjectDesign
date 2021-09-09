using System.Collections.Generic;

namespace Ao.ObjectDesign.Designing.Level
{
    public class SceneMap<TUI, TDesignObject> : Dictionary<IDesignPair<TUI, TDesignObject>, IDesignSceneController<TUI, TDesignObject>>, ISceneMap<TUI, TDesignObject>, IReadOnlySceneMap<TUI, TDesignObject>
    {

    }
}
