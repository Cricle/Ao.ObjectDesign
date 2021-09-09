using System;
using System.Collections.Generic;

namespace Ao.ObjectDesign.Designing.Level
{
    public interface IDesignSceneController<TUI, TDesignObject> : IDisposable
    {
        bool IsInitialized { get; }

        TUI UI { get; }
        IObservableDesignScene<TDesignObject> Scene { get; }
        IDesignSceneController<TUI, TDesignObject> Parent { get; }

        IReadOnlyList<IDesignPair<TUI, TDesignObject>> DesignUnits { get; }
        IReadOnlyDictionary<TUI, IDesignPair<TUI, TDesignObject>> DesignUnitMap { get; }
        IReadOnlyDictionary<TDesignObject, IDesignPair<TUI, TDesignObject>> DesignObjectUnitMap { get; }

        IReadOnlySceneMap<TUI, TDesignObject> Nexts { get; }
        IReadOnlyDictionary<TUI, IDesignSceneController<TUI, TDesignObject>> DesignUnitNextMap { get; }
        IReadOnlyDictionary<TDesignObject, IDesignSceneController<TUI, TDesignObject>> DesignObjectUnitNextMap { get; }

        void Initialize();
    }
}