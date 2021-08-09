using System;

namespace Ao.ObjectDesign.Data
{
    public class DataChangedEventArgs<TKey,TData> : EventArgs
    {
        public DataChangedEventArgs(TKey name, TData old, TData @new,ChangeModes mode)
        {
            Name = name;
            Old = old;
            New = @new;
            Mode = mode;
        }

        public TKey Name { get; }

        public TData Old { get; }

        public TData New { get; }

        public ChangeModes Mode { get; }
    }
}
