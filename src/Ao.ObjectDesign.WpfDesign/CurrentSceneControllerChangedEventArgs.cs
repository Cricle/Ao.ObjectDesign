using System;

namespace Ao.ObjectDesign.WpfDesign
{
    public class CurrentSceneControllerChangedEventArgs<TDesignObject> : EventArgs
    {
        public CurrentSceneControllerChangedEventArgs(DesignSceneController<TDesignObject> old, DesignSceneController<TDesignObject> @new)
        {
            Old = old;
            New = @new;
        }

        public DesignSceneController<TDesignObject> Old { get; }

        public DesignSceneController<TDesignObject> New { get; }
    }
}
