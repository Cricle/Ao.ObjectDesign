using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Wpf
{
    public static class ObservableExtensions
    {
        class SubscribeObject : IDisposable
        {
            private bool disposed;
            private readonly INotifyPropertyChanged notify;
            private readonly PropertyChangedEventHandler handler;

            public SubscribeObject(INotifyPropertyChanged notify, PropertyChangedEventHandler handler)
            {
                Debug.Assert(notify != null);
                Debug.Assert(handler != null);
                this.notify = notify;
                this.handler = handler;
            }

            ~SubscribeObject()
            {
                notify.PropertyChanged -= handler;
            }
            public void Dispose()
            {
                if (!disposed)
                {
                    disposed = true;
                    notify.PropertyChanged -= handler;
                    GC.SuppressFinalize(this);
                }
            }
        }
        public static IDisposable Subscribe<T>(this T notify, Expression<Func<T, object>> nameExp, Action<object> handler)
            where T : INotifyPropertyChanged
        {
            if (nameExp is null)
            {
                throw new ArgumentNullException(nameof(nameExp));
            }

            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            if (nameExp.Body is MemberExpression member)
            {
                return Subscribe(notify, member.Member.Name, handler);
            }
            throw new NotSupportedException(nameExp.ToString());
        }
        public static IDisposable Subscribe(this INotifyPropertyChanged notify, string name, Action<object> handler)
        {
            if (notify is null)
            {
                throw new ArgumentNullException(nameof(notify));
            }

            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException($"“{nameof(name)}”不能为 null 或空。", nameof(name));
            }

            if (handler is null)
            {
                throw new ArgumentNullException(nameof(handler));
            }

            PropertyChangedEventHandler hd = (o, e) =>
            {
                if (e.PropertyName == name)
                {
                    handler.Invoke(o);
                }
            };
            notify.PropertyChanged += hd;
            return new SubscribeObject(notify, hd);
        }
    }
}
