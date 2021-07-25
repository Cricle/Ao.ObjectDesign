using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Ao.ObjectDesign.Designing
{
    [DebuggerDisplay("Count={Count}")]
    public class CommandWays<T> : IEnumerable<T>, ICommandWays<T>
    {
        public const int DefaultMaxSize = 100;

        private readonly LinkedList<T> ways = new LinkedList<T>();

        private int maxSize = DefaultMaxSize;

        public int MaxSize
        {
            get => maxSize;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException($"Max undo size must more or equal zero");
                }
                int origin = maxSize;
                maxSize = value;
                if (origin > value)
                {
                    Resize(value);
                }
            }
        }

        public int Count => ways.Count;
        
        public T First
        {
            get
            {
                LinkedListNode<T> f = ways.First;
                if (f is null)
                {
                    return default;
                }
                return f.Value;
            }
        }

        public T Last
        {
            get
            {
                LinkedListNode<T> f = ways.Last;
                if (f is null)
                {
                    return default;
                }
                return f.Value;
            }
        }

        public event EventHandler<CommandWaysOperatorEventArgs<T>> WayChanged;

        private void Resize(int size)
        {
            List<T> removed = new List<T>();
            while (ways.Count > size)
            {
                LinkedListNode<T> f = ways.First;
                removed.Add(f.Value);
                ways.RemoveFirst();
            }
            WayChanged?.Invoke(this, new CommandWaysOperatorEventArgs<T>(removed, CommandWaysOperatorTypes.Remove));
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ways.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public virtual void Push(T item)
        {
            Push(item, true);
        }
        public virtual void Push(T item, bool notify)
        {
            if (maxSize == 0)
            {
                return;
            }
            if (ways.Count >= maxSize)
            {
                T val = ways.Last.Value;
                ways.RemoveLast();
                if (notify)
                {
                    WayChanged?.Invoke(this, new CommandWaysOperatorEventArgs<T>(new T[] { val }, CommandWaysOperatorTypes.Remove));
                }
            }
            ways.AddFirst(item);
            if (notify)
            {
                WayChanged?.Invoke(this, new CommandWaysOperatorEventArgs<T>(new T[] { item }, CommandWaysOperatorTypes.Add));
            }
        }
        public virtual void PushRange(IEnumerable<T> items)
        {
            PushRange(items, true);
        }
        public virtual void PushRange(IEnumerable<T> items, bool notify)
        {
            if (maxSize == 0)
            {
                return;
            }
            List<T> arr = items.ToList();
            int popCount = ways.Count + arr.Count - MaxSize;
            if (popCount > 0)
            {
                T[] sub = new T[popCount];
                for (int i = 0; i < popCount; i++)
                {
                    sub[i] = ways.Last.Value;
                    ways.RemoveLast();
                }
                if (notify)
                {
                    WayChanged?.Invoke(this, new CommandWaysOperatorEventArgs<T>(sub, CommandWaysOperatorTypes.Remove));
                }
            }
            foreach (T item in arr)
            {
                ways.AddFirst(item);
            }
            if (notify)
            {
                WayChanged?.Invoke(this, new CommandWaysOperatorEventArgs<T>(arr, CommandWaysOperatorTypes.Add));
            }
        }

        public virtual void Clear()
        {
            Clear(true);
        }
        public virtual void Clear(bool notify)
        {
            if (ways.Count != 0)
            {
                List<T> coll = ways.ToList();
                ways.Clear();
                if (notify)
                {
                    WayChanged?.Invoke(this, new CommandWaysOperatorEventArgs<T>(coll, CommandWaysOperatorTypes.Clear));
                }
            }
        }

        public virtual bool Contains(T item)
        {
            return ways.Contains(item);
        }

        public virtual void CopyTo(T[] array, int arrayIndex)
        {
            ways.CopyTo(array, arrayIndex);
        }
        public virtual T Pop()
        {
            return Pop(true);
        }
        public virtual T Peek()
        {
            return First;
        }
        public virtual T Pop(bool notify)
        {
            if (ways.Count != 0)
            {
                LinkedListNode<T> last = ways.First;
                if (last != null)
                {
                    ways.RemoveFirst();
                    if (notify)
                    {
                        WayChanged?.Invoke(this, new CommandWaysOperatorEventArgs<T>(new T[] { last.Value }, CommandWaysOperatorTypes.Remove));
                    }
                    return last.Value;
                }
            }
            return default;
        }
    }
}
