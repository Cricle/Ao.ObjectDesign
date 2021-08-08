using System;
using System.Collections.Generic;

namespace Ao.ObjectDesign.Designing
{
    public interface ICommandWays<T> : IReadOnlyCommandWays<T>
    {
        new int MaxSize { get; set; }

        event EventHandler<CommandWaysOperatorEventArgs<T>> WayChanged;

        void Clear(bool notify);
        T Pop(bool notify);
        T Peek();
        void Push(T item, bool notify);
        void PushRange(IEnumerable<T> items, bool notify);
    }
}