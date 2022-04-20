using System;

namespace Ao.ObjectDesign.Data
{
    public class DelegateDataNotifyer<TKey> : IDataNotifyer<TKey>
    {
        public DelegateDataNotifyer(Action<object, DataChangedEventArgs<TKey, IVarValue>> @delegate)
        {
            Delegate = @delegate ?? throw new ArgumentNullException(nameof(@delegate));
        }

        public Action<object, DataChangedEventArgs<TKey, IVarValue>> Delegate { get; }

        public void OnDataChanged(object sender, DataChangedEventArgs<TKey, IVarValue> e)
        {
            Delegate(sender, e);
        }
    }
}
