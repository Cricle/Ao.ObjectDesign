using System;
using System.Collections.Generic;

namespace Ao.ObjectDesign.Designing.Level
{
    public interface IDesignSceneController<TUI, TDesignObject>:IDisposable
    {
        bool IsInitialized { get; }
        IObservableDeisgnScene<TDesignObject> Scene { get; }
        IReadOnlyList<IDesignPair<TUI, TDesignObject>> DesignUnits { get; }

        void Initialize();
    }
}