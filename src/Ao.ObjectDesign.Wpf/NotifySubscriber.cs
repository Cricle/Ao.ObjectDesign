using Ao.ObjectDesign.Designing;
using System;
using System.Collections.Generic;

namespace Ao.ObjectDesign
{
    public static class NotifySubscriber
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
        public static IDisposable Subscribe(IEnumerable<WpfForViewBuildContextBase> ctxs,
            INotifyObjectManager mgr,
            AttackModes attackMode)
        {
            SubscribeCallback cb = new SubscribeCallback { Disposed = EmptyAction };
            foreach (WpfForViewBuildContextBase item in ctxs)
            {
                if ((attackMode & AttackModes.PropertyVisitor) != 0)
                {
                    item.PropertyVisitorCreated += handler;
                    cb.Disposed += () =>
                    {
                        item.PropertyVisitorCreated -= handler;
                    };
                }
                if ((attackMode & AttackModes.DeclareObject) != 0 &&
                    item.PropertyProxy.DeclaringInstance is INotifyPropertyChangeTo decChangeTo)
                {
                    mgr.Attack(decChangeTo);
                }
                if ((attackMode & AttackModes.NativeProperty) != 0 &&
                    item.PropertyVisitor.Value is INotifyPropertyChangeTo propChangeTo)
                {
                    mgr.Attack(propChangeTo);
                }
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
            public Action Disposed;

            public void Dispose()
            {
                Disposed?.Invoke();
            }
        }
    }
}
