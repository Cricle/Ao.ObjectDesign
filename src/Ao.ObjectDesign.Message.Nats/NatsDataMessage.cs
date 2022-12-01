using NATS.Client;

namespace Ao.ObjectDesign.Message.Nats
{
    public class NatsDataMessage : DefaultDataMessage<string, NatsEventPipeline, Msg, NatsDataEventRequest>
    {
        public NatsDataMessage(IDataEventBody<string, NatsEventPipeline, Msg, NatsDataEventRequest> body, Msg message) : base(body, message)
        {
        }
    }
}
