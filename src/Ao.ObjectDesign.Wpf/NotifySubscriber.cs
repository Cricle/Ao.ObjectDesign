using Ao.ObjectDesign.ForView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Ao.ObjectDesign.Wpf
{
    public class CreateTemplateContextsResult
    {
        public CreateTemplateContextsResult(IReadOnlyList<WpfForViewBuildContextBase> contexts, IDisposable subscriber)
        {
            Contexts = contexts;
            Subscriber = subscriber;
        }

        public IReadOnlyList<WpfForViewBuildContextBase> Contexts { get; }

        public IDisposable Subscriber { get; }
    }
    public class NotifySubscriber
    {
        private static Action EmptyAction => () => { };

        public static IEnumerable<IPropertyProxy> Lookup(IObjectProxy proxy)
        {
            return ObjectLookup.Lookup(proxy,
                x => x.Type.GetInterface(typeof(INotifyPropertyChangeTo).FullName) != null,
                (x, _) => x,false);
        }
        public static IDisposable Subscribe(IEnumerable<WpfForViewBuildContextBase> ctxs,INotifyObjectManager mgr)
        {
            var cb = new SubscribeCallback { Disposed = EmptyAction };
            foreach (var item in ctxs)
            {
                EventHandler handler = (o, e) =>
                {
                    if (o is WpfForViewBuildContextBase ctx)
                    {
                        if (ctx.PropertyVisitor is INotifyPropertyChangeTo changeTo)
                        {
                            mgr.Attack(changeTo);
                        }
                        if (ctx.PropertyProxy.DeclaringInstance is INotifyPropertyChangeTo decChangeTo)
                        {
                            mgr.Attack(decChangeTo);
                        }
                        if (ctx.PropertyVisitor.Value is INotifyPropertyChangeTo valChangeTo)
                        {
                            mgr.Attack(valChangeTo);
                        }
                    }
                };
                item.PropertyVisitorCreated += handler;
                cb.Disposed += () =>
                {
                    item.PropertyVisitorCreated -= handler;
                };
            }
            return cb;
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
