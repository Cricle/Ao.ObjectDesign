using System;

namespace Ao.ObjectDesign.Message
{
    public class DefaultDataMessage<TEventPipline, TMessage, TRequest> : DefaultDataMessage<long, TEventPipline, TMessage, TRequest>
             where TRequest : IDataEventRequest<long, TEventPipline>
    {
        public DefaultDataMessage(IDataEventBody<long, TEventPipline, TMessage, TRequest> body, TMessage message) : base(body, message)
        {
        }
    }
    public class DefaultDataMessage<TId, TEventPipline, TMessage, TRequest> : EventArgs, IDataMessage<TId, TEventPipline, TMessage, TRequest>
            where TRequest : IDataEventRequest<TId, TEventPipline>
    {
        protected DefaultDataMessage(IDataEventBody<TId, TEventPipline, TMessage, TRequest> body, TMessage message)
        {
            Body = body;
            Raw = message;
        }

        public IDataEventBody<TId, TEventPipline, TMessage, TRequest> Body { get; }

        public TMessage Raw { get; }
    }
}
