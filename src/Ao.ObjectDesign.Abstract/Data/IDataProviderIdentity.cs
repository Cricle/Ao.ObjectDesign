using System;

namespace Ao.ObjectDesign.Data
{
    public interface IDataProviderIdentity<T>
    {
        string Name { get; set; }

        event EventHandler<DataProviderDataChangedEventArgs<T>> DataChanged;

        bool SupportRaiseDataChanged { get; }
    }
}
