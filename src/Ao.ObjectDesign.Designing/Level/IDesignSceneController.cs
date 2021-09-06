using System;
using System.Collections.Generic;

namespace Ao.ObjectDesign.Designing.Level
{
    public interface IDesignSceneController<TUI, TDesignObject>:IDisposable
    {
        TUI UI { get; }
        bool IsInitialized { get; }
        IObservableDeisgnScene<TDesignObject> Scene { get; }
        IDesignSceneController<TUI, TDesignObject> Parent { get; }
        IReadOnlyList<IDesignPair<TUI, TDesignObject>> DesignUnits { get; }

        void Initialize();
    }
}