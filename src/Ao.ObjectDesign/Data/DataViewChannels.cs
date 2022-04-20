using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Data
{
    public class DataViewChannels<TKey>
    {
        public DataViewChannels(DataView<TKey> dataView)
        {
            DataView = dataView ?? throw new ArgumentNullException(nameof(dataView));
            notifyerMap = new ConcurrentDictionary<TKey, ChannelEntity>();
        }

        private readonly ConcurrentDictionary<TKey, ChannelEntity> notifyerMap;

        public DataView<TKey> DataView { get; }

        public IEnumerable<TKey> SubscribeNames => notifyerMap.Keys;

        public bool AsyncRaise { get; set; }

        public IReadOnlyDictionary<TKey, IReadOnlyList<IDataNotifyer<TKey>>> NotifyerMap =>
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

            if (!notifyerMap.TryGetValue(name, out var lst))
            {
                lst = new ChannelEntity(DataView,this, name);
                notifyerMap.AddOrUpdate(name, lst, (_, __) => lst);
            }
            lock (lst.SyncRoot)
            {
                lst.Subscribers.Add(notifyer);
            }
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
                bool ok;
                lock (notifyers.SyncRoot)
                {
                    ok = notifyers.Subscribers.Remove(notifyer);
                }
                if (notifyers.Subscribers.Count == 0)
                {
                    notifyers.Dispose();
                    notifyerMap.TryRemove(key, out _);
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
        public bool IsSubscribedNotifyer(TKey key, IDataNotifyer<TKey> notifyer)
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
                lock (notifyers.SyncRoot)
                {
                    return notifyers.Subscribers.Contains(notifyer);
                }
            }
            return false;
        }
        class ChannelEntity : IDisposable
        {
            public readonly object SyncRoot=new object();

            public readonly List<IDataNotifyer<TKey>> Subscribers;

            public IReadOnlyList<IDataNotifyer<TKey>> ReadOnlySubscribers => Subscribers;

            public readonly DataView<TKey> DataView;

            public readonly TKey Name;

            public readonly DataViewChannels<TKey> Channels;

            public ChannelEntity(DataView<TKey> dataView, DataViewChannels<TKey> channels, TKey key)
            {
                Debug.Assert(dataView != null);
                Debug.Assert(key != null);
                Debug.Assert(channels != null);
                Subscribers = new List<IDataNotifyer<TKey>>();
                DataView = dataView;
                Name = key;
                Channels = channels;
                DataView.DataChanged += OnDataViewDataChanged;
            }

            private void OnDataViewDataChanged(object sender, DataChangedEventArgs<TKey, IVarValue> e)
            {
                if (e.Key != null && e.Key.Equals(Name))
                {
                    if (Channels.AsyncRaise)
                    {
                        Task.Factory.StartNew((state) =>
                        {
                            var s = (List<IDataNotifyer<TKey>>)state;
                            RunNotify(s, sender, e);
                        }, Subscribers);
                    }
                    else
                    {
                        RunNotify(Subscribers, sender, e);
                    }
                }
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private void RunNotify(List<IDataNotifyer<TKey>> s,object sender,DataChangedEventArgs<TKey, IVarValue> e)
            {
                for (int i = 0; i < s.Count; i++)
                {
                    s[i].OnDataChanged(sender, e);
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
                Debug.Assert(key != null);
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
                    if (UnSubscribed != null)
                    {
                        var e = new NotifyUnSubscribedEventArgs<TKey>(Key, Channel, Notifyer);
                        UnSubscribed?.Invoke(this, e);
                    }
                }
            }
        }
    }
}
