﻿namespace Ao.ObjectDesign.Designing
{
    public interface IActionSequencer<TFallback> : ISequencer<TFallback>, INotifyObjectManager, IUndoRedo
        where TFallback : IFallbackable
    {
        bool CanUndo { get; }

        bool CanRedo { get; }
    }
}
