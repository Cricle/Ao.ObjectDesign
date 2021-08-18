using System;

namespace Ao.ObjectDesign.Designing.Level
{
    public class CurrentSceneChangedEventArgs<TDesignObject> : EventArgs
    {
        public CurrentSceneChangedEventArgs(IDeisgnScene<TDesignObject> old, IDeisgnScene<TDesignObject> @new)
        {
            Old = old;
            New = @new;
        }

        public IDeisgnScene<TDesignObject> Old { get; }

        public IDeisgnScene<TDesignObject> New { get; }
    }
}
