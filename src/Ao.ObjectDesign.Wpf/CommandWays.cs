using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Ao.ObjectDesign.Wpf
{
    [DebuggerDisplay("Count={Count}")]
    public class CommandWays<T> : IEnumerable<T>
    {
        private readonly LinkedList<T> ways = new LinkedList<T>();

        private int maxSize = 20;

        public int MaxSize
        {
            get => maxSize;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException($"Max undo size must more or equal zero");
                }
                var origin = maxSize;
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
                var f = ways.First;
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
                var f = ways.Last;
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
            var removed = new List<T>();
            while (ways.Count > size)
            {
                var f = ways.First;
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

        public void Push(T item,bool notify=true)
        {
            var size = ways.Count;
            ways.AddLast(item);
            if (notify)
            {
                WayChanged?.Invoke(this, new CommandWaysOperatorEventArgs<T>(new T[] { item }, CommandWaysOperatorTypes.Add));
            }
            if (size >= maxSize)
            {
                Pop();
            }
        }

        public void PushRange(IEnumerable<T> items, bool notify = true)
        {
            var arr = items.ToList();
            foreach (var item in arr)
            {
                ways.AddLast(item);
            }
            if (notify)
            {
                WayChanged?.Invoke(this, new CommandWaysOperatorEventArgs<T>(arr, CommandWaysOperatorTypes.Add));
            }
            var popCount = ways.Count + arr.Count - MaxSize;
            if (popCount>0)
            {
                var sub = new T[popCount];
                for (int i = 0; i < popCount; i++)
                {
                    sub[i] = ways.First.Value;
                    ways.RemoveFirst();
                }
                if (notify)
                {
                    WayChanged?.Invoke(this, new CommandWaysOperatorEventArgs<T>(sub, CommandWaysOperatorTypes.Remove));
                }
            }
        }


        public void Clear(bool notify = true)
        {
            var coll = ways.ToList();
            ways.Clear();
            if (notify)
            {
                WayChanged?.Invoke(this, new CommandWaysOperatorEventArgs<T>(coll, CommandWaysOperatorTypes.Clear));
            }
        }

        public bool Contains(T item)
        {
            return ways.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            ways.CopyTo(array, arrayIndex);
        }

        public T Pop(bool notify=true)
        {
            var last = ways.First;
            if (last != null)
            {
                ways.RemoveFirst();
                if (notify)
                {
                    WayChanged?.Invoke(this, new CommandWaysOperatorEventArgs<T>(new T[] { last.Value }, CommandWaysOperatorTypes.Remove));
                }
                return last.Value;
            }
            return default;
        }
    }
}
