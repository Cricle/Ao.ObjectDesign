using Ao.ObjectDesign;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Ao.ObjectDesign.Data
{
    public class DataViewChannels<TKey>
    {
        public DataViewChannels(DataView<TKey> dataView)
        {
            DataView = dataView ?? throw new ArgumentNullException(nameof(dataView));
            notifyerMap = new Dictionary<TKey, ChannelEntity>();
        }

        private readonly Dictionary<TKey, ChannelEntity> notifyerMap;

        public DataView<TKey> DataView { get; }

        public IEnumerable<TKey> SubscribeNames => notifyerMap.Keys;

        public IReadOnlyDictionary<TKey, IReadOnlyHashSet<IDataNotifyer<TKey>>> NotifyerMap =>
            notifyerMap.ToDictionary(x => x.Key, x => x.Value.ReadOnlySubscribers);

        public INotifyToken<TKey> Regist(TKey name, IDataNotifyer<TKey> notifyer)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (notifyer is null)
            {
                throw new ArgumentNullException(nameof(notifyer));
            }

            if (!notifyerMap.TryGetValue(name,out var lst))
            {
                lst = new ChannelEntity(DataView, name);
                notifyerMap.Add(name, lst);
            }
            lst.Subscribers.Add(notifyer);
            return new NotifyToken(this, name, notifyer);
        }

        public bool UnRegist(TKey key, IDataNotifyer<TKey> notifyer)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (notifyer is null)
            {
                throw new ArgumentNullException(nameof(notifyer));
            }

            if (notifyerMap.TryGetValue(key, out var notifyers))
            {
                var ok = notifyers.Subscribers.Remove(notifyer);
                if (notifyers.Subscribers.Count == 0)
                {
                    notifyers.Dispose();
                    notifyerMap.Remove(key);
                }
                return ok;
            }
            return false;
        }
        public int? GetSubscribeCount(TKey name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (notifyerMap.TryGetValue(name, out var notifyers))
            {
                return notifyers.Subscribers.Count;
            }
            return null;
        }
        public bool IsSubscribedName(TKey key)
        {
            return notifyerMap.ContainsKey(key);
        }
        public bool IsSubscribedNotifyer(TKey key,IDataNotifyer<TKey> notifyer)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            if (notifyer is null)
            {
                throw new ArgumentNullException(nameof(notifyer));
            }

            return notifyerMap.TryGetValue(key, out var notifyers) &&
                notifyers.Subscribers.Contains(notifyer);
        }
        class ChannelEntity : IDisposable
        {
            public readonly HashSet<IDataNotifyer<TKey>> Subscribers;

            public IReadOnlyHashSet<IDataNotifyer<TKey>> ReadOnlySubscribers => new ReadOnlyHashSet<IDataNotifyer<TKey>>(Subscribers);

            public readonly DataView<TKey> DataView;

            public readonly TKey Name;

            public ChannelEntity(DataView<TKey> dataView, TKey key)
            {
                Debug.Assert(dataView != null);
                Debug.Assert(key != null);
                Subscribers = new HashSet<IDataNotifyer<TKey>>();
                DataView = dataView;
                Name = key;
                DataView.DataChanged += OnDataViewDataChanged;
            }

            private void OnDataViewDataChanged(object sender, DataChangedEventArgs<TKey, VarValue> e)
            {
                if (e.Key!=null &&e.Key.Equals(Name))
                {
                    foreach (var item in Subscribers)
                    {
                        item.OnDataChanged(sender, e);
                    }
                }
            }

            public void Dispose()
            {
                DataView.DataChanged -= OnDataViewDataChanged;
            }
        }

        class NotifyToken : INotifyToken<TKey>
        {
            public NotifyToken(DataViewChannels<TKey> channels, TKey key, IDataNotifyer<TKey> notifyer)
            {
                Debug.Assert(channels != null);
                Debug.Assert(key!=null);
                Debug.Assert(notifyer != null);
                Channel = channels;
                Notifyer = notifyer;
                Key = key;
            }


            public DataViewChannels<TKey> Channel { get; }

            public IDataNotifyer<TKey> Notifyer { get; }

            public TKey Key { get; }

            public bool IsSubscribed => Channel.IsSubscribedNotifyer(Key, Notifyer);

            public event EventHandler<NotifyUnSubscribedEventArgs<TKey>> UnSubscribed;

            public void Dispose()
            {
                if (IsSubscribed)
                {

                    Debug.Assert(Channel != null);
                    Debug.Assert(Key != null);
                    Debug.Assert(Notifyer != null);
                    Channel.UnRegist(Key, Notifyer);
                    if (UnSubscribed!=null)
                    {
                        var e = new NotifyUnSubscribedEventArgs<TKey>(Key, Channel, Notifyer);
                        UnSubscribed?.Invoke(this, e);
                    }
                }
            }
        }
    }
}
