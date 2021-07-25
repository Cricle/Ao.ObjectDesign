using System;
using System.Collections.Generic;

namespace Ao.ObjectDesign.Designing
{
    public interface ICommandWays<T>
    {
        int Count { get; }
        T First { get; }
        T Last { get; }
        int MaxSize { get; set; }

        event EventHandler<CommandWaysOperatorEventArgs<T>> WayChanged;

        void Clear(bool notify);
        bool Contains(T item);
        T Pop(bool notify);
        T Peek();
        void Push(T item, bool notify);
        void PushRange(IEnumerable<T> items, bool notify);
    }
}