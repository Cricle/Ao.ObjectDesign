using System;

namespace Ao.ObjectDesign.Data
{
    public class NotifyUnSubscribedEventArgs<TKey,TValue> : EventArgs
    {
        public NotifyUnSubscribedEventArgs(TKey key, DataViewChannels<TKey, TValue> channels, IDataNotifyer<TKey, TValue> notifyer)
        {
            Key = key;
            Channels = channels;
            Notifyer = notifyer;
        }

        public TKey Key { get; }

        public DataViewChannels<TKey, TValue> Channels { get; }

        public IDataNotifyer<TKey, TValue> Notifyer { get; }
    }
}
