using System;

namespace Ao.ObjectDesign.Data
{
    public interface IDataNotifyer<TKey, TValue>
    {
        void OnDataChanged(object sender, DataChangedEventArgs<TKey, TValue> e);
    }
}
