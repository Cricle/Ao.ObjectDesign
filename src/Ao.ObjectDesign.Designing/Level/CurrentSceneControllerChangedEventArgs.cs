using System;

namespace Ao.ObjectDesign.Designing.Level
{
    public class CurrentSceneControllerChangedEventArgs<TUI, TDesignObject> : EventArgs
    {
        public CurrentSceneControllerChangedEventArgs(IDesignSceneController<TUI, TDesignObject> old, IDesignSceneController<TUI, TDesignObject> @new)
        {
            Old = old;
            New = @new;
        }

        public IDesignSceneController<TUI, TDesignObject> Old { get; }

        public IDesignSceneController<TUI, TDesignObject> New { get; }
    }
}
