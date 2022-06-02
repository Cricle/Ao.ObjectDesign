using System;

namespace Ao.ObjectDesign.Data
{
    public class DataProviderDataChangedEventArgs<T>:EventArgs
    {
        public DataProviderDataChangedEventArgs(IDataProviderIdentity<T> provider, T old, T @new)
        {
            Provider = provider;
            Old = old;
            New = @new;
        }

        public IDataProviderIdentity<T> Provider { get; }

        public T Old { get; }

        public T New { get; }
    }
}
