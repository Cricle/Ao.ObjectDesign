using System;

namespace Ao.ObjectDesign.Data
{
    public abstract class DataProviderIdentityBase<T>: IDataProviderIdentity<T>
    {
        public string Name { get; set; }

        public virtual bool SupportRaiseDataChanged => true;

        public event EventHandler<DataProviderDataChangedEventArgs<T>> DataChanged;

        protected void RaiseDataChanged(in T old, in T @new)
        {
            RaiseDataChanged(new DataProviderDataChangedEventArgs<T>(this, old, @new));
        }
        protected void RaiseDataChanged(DataProviderDataChangedEventArgs<T> args)
        {
            DataChanged?.Invoke(this, args);
        }
    }
}
