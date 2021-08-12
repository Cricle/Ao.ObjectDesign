using Ao.ObjectDesign.WpfDesign.Level;
using System;

namespace Ao.ObjectDesign.WpfDesign
{
    public class StoredSceneEventArgs<TScene, TDesignObject> : EventArgs
        where TScene : IDeisgnScene<TDesignObject> 
    {
        public StoredSceneEventArgs(TScene scene)
        {
            Scene = scene;
        }

        public TScene Scene { get; }
    }
}
