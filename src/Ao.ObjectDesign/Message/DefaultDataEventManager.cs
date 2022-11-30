using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Message
{
    public abstract class DefaultDataEventManager<TEventPipline, TMessage, TRequest> : DefaultDataEventManager<long, TEventPipline, TMessage, TRequest>
           where TRequest : IDataEventRequest<long, TEventPipline>
    {

    }
    public abstract class DefaultDataEventManager<TId, TEventPipline, TMessage, TRequest> : IDataEventManager<TId, TEventPipline, TMessage, TRequest>
         where TRequest : IDataEventRequest<TId, TEventPipline>
    {
        protected readonly ConcurrentDictionary<TId, IDataEventBody<TId, TEventPipline, TMessage, TRequest>> bodys = new ConcurrentDictionary<TId, IDataEventBody<TId, TEventPipline, TMessage, TRequest>>();
#if NET452
        public IReadOnlyDictionary<TId, IDataEventBody<TId, TEventPipline, TMessage, TRequest>> Bodys => bodys.ToDictionary(x => x.Key, x => x.Value);
#else
        public IReadOnlyDictionary<TId, IDataEventBody<TId, TEventPipline, TMessage, TRequest>> Bodys => bodys;
#endif

        public virtual async Task<IList<IDataEventBody<TId, TEventPipline, TMessage, TRequest>>> ClearAsync()
        {
            var values = bodys.Values.ToList();
            foreach (var item in values)
            {
                await CloseEventPiplineAsync(item);
            }
            return values;
        }

        public abstract Task<IDataEventBody<TId, TEventPipline, TMessage, TRequest>> CreateAsync(TRequest request);

        public void Dispose()
        {
            ClearAsync().GetAwaiter().GetResult();
            OnDisposed();
            GC.SuppressFinalize(this);
        }

        protected void OnDisposed()
        {

        }
        protected virtual async Task CloseEventPiplineAsync(IDataEventBody<TId, TEventPipline, TMessage, TRequest> body)
        {
            await body.StopAsync();
            body.Dispose();
        }
        public virtual async Task<IList<IDataEventBody<TId, TEventPipline, TMessage, TRequest>>> StopAsync(IEnumerable<TId> ids)
        {
            var values = new List<IDataEventBody<TId, TEventPipline, TMessage, TRequest>>();
            foreach (var item in ids)
            {
                if (bodys.TryGetValue(item, out var body))
                {
                    await CloseEventPiplineAsync(body);
                    values.Add(body);
                }
            }
            return values;
        }
#if NET6_0_OR_GREATER
        public async ValueTask DisposeAsync()
        {
            await ClearAsync();
            await OnDisposedAsync();
            GC.SuppressFinalize(this);
        }
        protected virtual ValueTask OnDisposedAsync()
        {
            return new ValueTask();
        }
#endif
    }
}
