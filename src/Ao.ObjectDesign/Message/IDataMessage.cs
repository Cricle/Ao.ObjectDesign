namespace Ao.ObjectDesign.Message
{
    public interface IDataMessage<TId, TEventPipline, TMessage, TRequest>
           where TRequest : IDataEventRequest<TId, TEventPipline>
    {
        IDataEventBody<TId, TEventPipline, TMessage, TRequest> Body { get; }

        TMessage Raw { get; }
    }
}
