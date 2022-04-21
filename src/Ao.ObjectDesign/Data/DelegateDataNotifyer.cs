using System;

namespace Ao.ObjectDesign.Data
{
    public class DelegateDataNotifyer<TKey, TValue> : IDataNotifyer<TKey, TValue>
    {
        public DelegateDataNotifyer(Action<object, DataChangedEventArgs<TKey, TValue>> @delegate)
        {
            Delegate = @delegate ?? throw new ArgumentNullException(nameof(@delegate));
        }

        public Action<object, DataChangedEventArgs<TKey, TValue>> Delegate { get; }

        public void OnDataChanged(object sender, DataChangedEventArgs<TKey, TValue> e)
        {
            Delegate(sender, e);
        }
    }
}
