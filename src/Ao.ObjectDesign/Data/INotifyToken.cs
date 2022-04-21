using System;

namespace Ao.ObjectDesign.Data
{
    public interface INotifyToken<TKey, TValue> : IDisposable
    {
        bool IsSubscribed { get; }

        TKey Key { get; }

        IDataNotifyer<TKey, TValue> Notifyer { get; }

        DataViewChannels<TKey, TValue> Channel { get; }

        event EventHandler<NotifyUnSubscribedEventArgs<TKey, TValue>> UnSubscribed;
    }
}
