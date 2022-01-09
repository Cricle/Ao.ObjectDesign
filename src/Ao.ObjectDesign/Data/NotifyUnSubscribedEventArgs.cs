using System;

namespace Ao.ObjectDesign.Data
{
    public class NotifyUnSubscribedEventArgs<TKey> : EventArgs
    {
        public NotifyUnSubscribedEventArgs(TKey key, DataViewChannels<TKey> channels, IDataNotifyer<TKey> notifyer)
        {
            Key = key;
            Channels = channels;
            Notifyer = notifyer;
        }

        public TKey Key { get; }

        public DataViewChannels<TKey> Channels { get; }

        public IDataNotifyer<TKey> Notifyer { get; }
    }
}
