using System;
using System.Collections.Generic;

namespace Ao.ObjectDesign.Wpf
{
    public class NotifySubscriber
    {
        private static readonly string INotifyPropertyChangeToTypeName = typeof(INotifyPropertyChangeTo).FullName;
        private static Action EmptyAction => () => { };

        public static IEnumerable<IPropertyProxy> Lookup(IObjectProxy proxy)
        {
            return ObjectLookup.Lookup(proxy,
                PropertyProxyChangeToCanStepIn,
                PropertyProxyValueFetch,
                false);
        }
        private static IPropertyProxy PropertyProxyValueFetch(IPropertyProxy proxy, IPropertyVisitor visitor)
        {
            return proxy;
        }
        private static bool PropertyProxyChangeToCanStepIn(IPropertyProxy proxy)
        {
            return proxy.Type.GetInterface(INotifyPropertyChangeToTypeName) != null;
        }
        public static IDisposable Subscribe(IEnumerable<WpfForViewBuildContextBase> ctxs, INotifyObjectManager mgr)
        {
            SubscribeCallback cb = new SubscribeCallback { Disposed = EmptyAction };
            foreach (WpfForViewBuildContextBase item in ctxs)
            {
                item.PropertyVisitorCreated += handler;
                cb.Disposed += () =>
                {
                    item.PropertyVisitorCreated -= handler;
                };
            }
            return cb;


            void handler(object o, EventArgs e)
            {
                if (o is WpfForViewBuildContextBase ctx &&
                    ctx.PropertyVisitor is INotifyPropertyChangeTo changeTo)
                {
                    mgr.Attack(changeTo);
                }
            }
        }
        class SubscribeCallback : IDisposable
        {
            public Action Disposed { get; set; }

            public void Dispose()
            {
                Disposed?.Invoke();
            }
        }
    }
}
