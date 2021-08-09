using Ao.ObjectDesign.WpfDesign.Level;
using System;

namespace Ao.ObjectDesign.WpfDesign
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
