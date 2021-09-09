using System;

namespace Ao.ObjectDesign.Designing.Level
{
    public class CurrentSceneChangedEventArgs<TDesignObject> : EventArgs
    {
        public CurrentSceneChangedEventArgs(IDesignScene<TDesignObject> old, IDesignScene<TDesignObject> @new)
        {
            Old = old;
            New = @new;
        }

        public IDesignScene<TDesignObject> Old { get; }

        public IDesignScene<TDesignObject> New { get; }
    }
}
