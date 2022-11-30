using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Message
{
    public abstract class DefaultDataEventBody<TEventPipline, TMessage, TRequest> : DefaultDataEventBody<long, TEventPipline, TMessage, TRequest>
          where TRequest : IDataEventRequest<long, TEventPipline>
    {
        protected DefaultDataEventBody(TRequest request, TEventPipline eventPipline) : base(request, eventPipline)
        {
        }
    }
    public abstract class DefaultDataEventBody<TId, TEventPipline, TMessage, TRequest> : IDataEventBody<TId, TEventPipline, TMessage, TRequest>
         where TRequest : IDataEventRequest<TId, TEventPipline>
    {
        protected DefaultDataEventBody(TRequest request, TEventPipline eventPipline)
        {
            Request = request;
            EventPipline = eventPipline;
        }

        private CancellationTokenSource cancellationTokenSource;
        private int isStarted;

        public event EventHandler<IDataMessage<TId, TEventPipline, TMessage, TRequest>> MessageReceived;

        public TRequest Request { get; }

        public CancellationTokenSource CancellationTokenSource => cancellationTokenSource;

        public TEventPipline EventPipline { get; }


        protected void RaiseMessageReceived(IDataMessage<TId, TEventPipline, TMessage, TRequest> args)
        {
            MessageReceived?.Invoke(this, args);
        }

        protected virtual CancellationTokenSource GetCancellationTokenSource()
        {
            return new CancellationTokenSource();
        }

        protected virtual void CleanCancellationTokenSource(CancellationTokenSource cancellationTokenSource)
        {
            cancellationTokenSource?.Dispose();
        }

        public CancellationToken CancellationToken => cancellationTokenSource == null ? CancellationToken.None : cancellationTokenSource.Token;

        public bool IsStrated => Volatile.Read(ref isStarted) != 0;


        public void Dispose()
        {
            OnDisposed();
            GC.SuppressFinalize(this);
        }

        protected virtual void OnDisposed()
        {

        }

        public Task<bool> StartAsync()
        {
            if (Interlocked.CompareExchange(ref isStarted, 1, 0) == 0)
            {
                CleanCancellationTokenSource(cancellationTokenSource);
                cancellationTokenSource = GetCancellationTokenSource();
                return OnStartAsync();
            }
            return TaskResultCaches.falseTask;
        }

        protected abstract Task<bool> OnStartAsync();

        public Task<bool> StopAsync()
        {
            if (Interlocked.CompareExchange(ref isStarted, 0, 1) == 1)
            {
                return OnStopAsync();
            }
            return TaskResultCaches.falseTask;
        }

        protected abstract Task<bool> OnStopAsync();
    }
}
