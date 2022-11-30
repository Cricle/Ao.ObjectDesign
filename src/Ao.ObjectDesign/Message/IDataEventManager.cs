using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Message
{
    public interface IDataEventManager<TId, TEventPipline, TMessage, TRequest> : IDisposable
#if NET6_0_OR_GREATER
        ,IAsyncDisposable
#endif
        where TRequest : IDataEventRequest<TId, TEventPipline>
    {
        IReadOnlyDictionary<TId, IDataEventBody<TId, TEventPipline, TMessage, TRequest>> Bodys { get; }

        Task<IDataEventBody<TId, TEventPipline, TMessage, TRequest>> CreateAsync(TRequest request);

        Task<IList<IDataEventBody<TId, TEventPipline, TMessage, TRequest>>> StopAsync(IEnumerable<TId> id);

        Task<IList<IDataEventBody<TId, TEventPipline, TMessage, TRequest>>> ClearAsync();
    }
}
