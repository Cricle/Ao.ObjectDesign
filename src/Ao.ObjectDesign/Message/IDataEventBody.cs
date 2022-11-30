using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ao.ObjectDesign.Message
{
    public interface IDataEventBody<TId, TEventPipline, TMessage, TRequest> : IDisposable
        where TRequest : IDataEventRequest<TId, TEventPipline>
    {
        TRequest Request { get; }

        TEventPipline EventPipline { get; }

        CancellationToken CancellationToken { get; }

        bool IsStrated { get; }

        event EventHandler<IDataMessage<TId, TEventPipline, TMessage, TRequest>> MessageReceived;

        Task<bool> StartAsync();

        Task<bool> StopAsync();
    }
}
