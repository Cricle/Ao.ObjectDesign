using System;

namespace Ao.ObjectDesign.Designing.Level
{
    public class CurrentSceneControllerChangedEventArgs<TUI, TDesignObject> : EventArgs
    {
        public CurrentSceneControllerChangedEventArgs(DesignSceneController<TUI, TDesignObject> old, DesignSceneController<TUI, TDesignObject> @new)
        {
            Old = old;
            New = @new;
        }

        public DesignSceneController<TUI, TDesignObject> Old { get; }

        public DesignSceneController<TUI, TDesignObject> New { get; }
    }
}
