using System;

namespace Ao.ObjectDesign.Data
{
    public interface INotifyToken<TKey> : IDisposable
    {
        bool IsSubscribed { get; }

        TKey Key { get; }

        IDataNotifyer<TKey> Notifyer { get; }

        DataViewChannels<TKey> Channel { get; }

        event EventHandler<NotifyUnSubscribedEventArgs<TKey>> UnSubscribed;
    }
}
