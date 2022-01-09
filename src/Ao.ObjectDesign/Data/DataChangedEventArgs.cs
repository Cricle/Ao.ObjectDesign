using System;

namespace Ao.ObjectDesign.Data
{
    public class DataChangedEventArgs<TKey, TData> : EventArgs
    {
        public DataChangedEventArgs(TKey key, TData old, TData @new, ChangeModes mode)
        {
            Key = key;
            Old = old;
            New = @new;
            Mode = mode;
        }

        public TKey Key { get; }

        public TData Old { get; }

        public TData New { get; }

        public ChangeModes Mode { get; }
    }
}
